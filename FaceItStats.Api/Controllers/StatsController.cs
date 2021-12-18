using FaceItStats.Api.Client;
using FaceItStats.Api.Client.Models;
using FaceItStats.Api.Hubs;
using FaceItStats.Api.Models;
using FaceItStats.Api.Persistence;
using FaceItStats.Api.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
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

        public StatsController(FaceitDbContext faceItDbContext, IHubContext<FaceItStatsHub> hubContext)
        {
            _faceItClient = new FaceItStatsClient();
            _faceItDbContext = faceItDbContext;
            _hubContext = hubContext;
        }

        [HttpGet("GetStats")]
        public async Task<IActionResult> GetFaceItStats([FromQuery]string nickname, CancellationToken cancellationToken)
        {
            var stats = await _faceItClient.GetStatsForNickname(nickname, cancellationToken);
            var userInfo = await _faceItClient.GetUserInfoForNickname(nickname, cancellationToken);

            return Ok(stats.ToFaceItStatsResponse(userInfo.PlayerId));
        }

        [HttpPost("FaceItWebhook")]
        public async Task<IActionResult> FaceItWebhook([FromBody]FaceitWebhookModel body, CancellationToken cancellationToken)
        {
            var bodyString = JsonConvert.SerializeObject(body);

            await _hubContext.Clients.All.SendAsync(body.Event, body.ThirdPartyId.ToString(), cancellationToken);


            if (body.Event.Equals("match_object_created"))
            {
                _faceItDbContext.Add(new MatchResult(body.Payload.Id));
            }


            if (body.Event.Equals("match_status_ready"))
            {
                var matchResult = _faceItDbContext.MatchResult.FirstOrDefault(x => x.MatchId.Equals(body.Payload.Id));
                if(matchResult != null)
                {
                    matchResult.MarkAsStarted();
                }
            }


            if (body.Event.Equals("match_status_cancelled"))
            {
                var matchResult = _faceItDbContext.MatchResult.FirstOrDefault(x => x.MatchId.Equals(body.Payload.Id));
                if (matchResult != null)
                {
                    matchResult.MarkAsCancelled();
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

                    matchResult.AddResult(isWin, (int)myScore.Kills, myScore.KDRatio);
                    matchResult.MarkAsFinished();
                }
            }

            _faceItDbContext.Add(new FaceitWebhookData(bodyString));
            await _faceItDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
