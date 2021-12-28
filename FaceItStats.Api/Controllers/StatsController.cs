using FaceItStats.Api.Client;
using FaceItStats.Api.Client.Models;
using FaceItStats.Api.Configs;
using FaceItStats.Api.Hubs;
using FaceItStats.Api.Models;
using FaceItStats.Api.Persistence;
using FaceItStats.Api.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly FaceItStatsClient _faceItClient;
        private readonly FaceitDbContext _faceItDbContext;
        private readonly IHubContext<FaceItStatsHub> _hubContext;
        private readonly SeClient _seClient;
        private readonly string _token;

        public StatsController(FaceitDbContext faceItDbContext, IHubContext<FaceItStatsHub> hubContext, IOptions<Auth> authSettings)
        {
            _faceItClient = new FaceItStatsClient();
            _seClient = new SeClient(authSettings.Value.SeToken);
            _faceItDbContext = faceItDbContext;
            _hubContext = hubContext;
            _token = authSettings.Value.SeToken;
        }

        [HttpGet("GetStats")]
        public async Task<IActionResult> GetFaceItStats([FromQuery]string nickname, CancellationToken cancellationToken)
        {
            var stats = await _faceItClient.GetStatsForNickname(nickname, cancellationToken);
            var userInfo = await _faceItClient.GetUserInfoForNickname(nickname, cancellationToken);

            return Ok(stats.ToFaceItStatsResponse(userInfo.PlayerId));
        }

        [HttpGet("GetToken")]
        public IActionResult GetToken()
        {
            return Ok(_token);
        }

        [HttpPost("FaceItWebhook")]
        public async Task<IActionResult> FaceItWebhook([FromBody]FaceitWebhookModel body, CancellationToken cancellationToken)
        {
            var bodyString = JsonConvert.SerializeObject(body);
            var betSettings = await _faceItDbContext.BetsSettings.FirstOrDefaultAsync(cancellationToken);
            var isBetsEnabled = betSettings != null && betSettings.IsEnabled;

            await _hubContext.Clients.All.SendAsync(body.Event, body.ThirdPartyId.ToString(), cancellationToken);

            if (body.Event.Equals("match_object_created"))
            {
                var matchResult = new MatchResult(body.Payload.Id);

                _faceItDbContext.Add(matchResult);

                if (isBetsEnabled)
                {
                    var contest = await _seClient.CreateBet();
                    await _seClient.StartBet(contest.Id);

                    matchResult.SetContest(contest.Id, contest.Options.First().Id, contest.Options.Last().Id);
                }
            }


            if (body.Event.Equals("match_status_ready"))
            {
                var matchResult = _faceItDbContext.MatchResult.FirstOrDefault(x => x.MatchId.Equals(body.Payload.Id));
                
                if(matchResult == null)
                {
                    matchResult = new MatchResult(body.Payload.Id);
                }

                matchResult.MarkAsStarted();
            }


            if (body.Event.Equals("match_status_cancelled") || body.Event.Equals("match_status_aborted"))
            {
                var matchResult = _faceItDbContext.MatchResult.FirstOrDefault(x => x.MatchId.Equals(body.Payload.Id));

                if (matchResult == null)
                {
                    matchResult = new MatchResult(body.Payload.Id);
                }

                matchResult.MarkAsCancelled();
                
                if (isBetsEnabled || matchResult.ContestId != null)
                {
                    await _seClient.RefundBet(matchResult.ContestId);

                    var contest = await _seClient.GetBet(matchResult.ContestId);

                    if (!contest.State.Equals(ContestState.Closed))
                    {
                        await _seClient.CancelBet(matchResult.ContestId);
                    }
                }

            }


            if (body.Event.Equals("match_status_finished"))
            {
                var matchResult = _faceItDbContext.MatchResult.FirstOrDefault(x => x.MatchId.Equals(body.Payload.Id));
                if (matchResult != null)
                {
                    var user = await _faceItClient.GetUserInfoForNickname("luciusxsein", cancellationToken);
                    var userId = user.PlayerId;

                    var matchDetails = await _faceItClient.GetMatchDetails(matchResult.MatchId, cancellationToken);
                    var myFaction = matchDetails.Teams.Faction1.Roster.Any(x => x.PlayerId.ToString().Equals(userId)) ? "faction1" : "faction2";
                    var isWin = matchDetails.Results.Winner.Equals(myFaction);

                    var matchStats = await _faceItClient.GetStatisticOfMatch(matchResult.MatchId, cancellationToken);
                    var myScore = matchStats.Rounds.FirstOrDefault()
                        .Teams.FirstOrDefault(x => x.Players.Any(x => x.PlayerId.ToString().Equals(userId)))
                        .Players.FirstOrDefault(x => x.PlayerId.ToString().Equals(userId))
                        .PlayerStats;

                    matchResult.AddResult(isWin, (int)myScore.Kills, decimal.Parse(myScore.KDRatio, CultureInfo.InvariantCulture));
                    matchResult.MarkAsFinished();

                    if (isBetsEnabled || matchResult.ContestId != null)
                    {
                        var winnerId = await GetWinnerOptionAsync(matchResult);

                        await _seClient.PickWinner(matchResult.ContestId, winnerId);
                    }
                }
            }

            _faceItDbContext.Add(new FaceitWebhookData(bodyString));
            await _faceItDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpPost("BetState")]
        public async Task<IActionResult> ChangeBetState([FromQuery] bool isEnabled, CancellationToken cancellationToken)
        {
            var betSettings =  _faceItDbContext.BetsSettings.First();

            betSettings.IsEnabled = isEnabled;

            await _faceItDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpGet("BetState")]
        public async Task<IActionResult> GetBetState(CancellationToken cancellationToken)
        {
            var betSettings = _faceItDbContext.BetsSettings.FirstOrDefault();

            if(betSettings == null)
            {
                _faceItDbContext.BetsSettings.Add(new BetsSettings { IsEnabled = false });
                await _faceItDbContext.SaveChangesAsync(cancellationToken);
                return Ok(false);
            } 

            return Ok(betSettings.IsEnabled);
        }

        private async Task<string> GetWinnerOptionAsync(MatchResult result)
        {
            var contest = await _seClient.GetBet(result.ContestId);

            if (contest.Title == ContestType.WinLose)
            {
                return result.IsWin ? result.FirstOptionId : result.SecondOptionId;
            }
            else if(contest.Title == ContestType.Kd)
            {
                return (double)result.KdRatio > 0.995 ? result.FirstOptionId : result.SecondOptionId;
            }
            else if (contest.Title == ContestType.Kills)
            {
                return result.Kills > 19.5 ? result.FirstOptionId : result.SecondOptionId;
            }

            return string.Empty;
        }
    }
}
