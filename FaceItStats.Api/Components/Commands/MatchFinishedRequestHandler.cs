using FaceItStats.Api.Client;
using FaceItStats.Api.Client.Models;
using FaceItStats.Api.Configs;
using FaceItStats.Api.Hubs;
using FaceItStats.Api.Persistence;
using FaceItStats.Api.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Components.Commands
{
    public class MatchFinishedRequestHandler(
        FaceitDbContext faceItDbContext,
        IOptions<Auth> authSettings,
        IHubContext<NotificationsHub> hubContext,
        IOptions<ThirdPartyApis> thirdPartyApisOptions)
        : IRequestHandler<MatchFinishedRequest>
    {
        private readonly SeClient _seClient = SeClient.CreateInstance(authSettings.Value.SeToken, hubContext);
        private readonly FaceItStatsClient _faceItClient = new(thirdPartyApisOptions.Value);

        public async Task Handle(MatchFinishedRequest request, CancellationToken cancellationToken)
        {
            var betSettings = await faceItDbContext.BetsSettings.FirstOrDefaultAsync(cancellationToken);
            var isBetsEnabled = betSettings != null && betSettings.IsEnabled;

            var matchResult = faceItDbContext.MatchResult.FirstOrDefault(x => x.MatchId.Equals(request.MatchId));
            if (matchResult != null)
            {
                var user = await _faceItClient.GetUserInfoForNickname("luciusxsein", cancellationToken);
                var userId = user.PlayerId;

                var matchDetails = await _faceItClient.GetMatchDetails(matchResult.MatchId, cancellationToken);
                var myFaction = matchDetails.Teams.Faction1.Roster.Any(x => x.PlayerId.ToString().Equals(userId)) ? "faction1" : "faction2";
                var isWin = matchDetails.Results.Winner.Equals(myFaction);

                var matchStats = await _faceItClient.GetStatisticOfMatch(matchResult.MatchId, cancellationToken);
                var myScore = matchStats.Rounds.FirstOrDefault()!
                    .Teams.FirstOrDefault(x => x.Players.Any(x => x.PlayerId.ToString().Equals(userId)))
                    .Players.FirstOrDefault(x => x.PlayerId.ToString().Equals(userId))
                    .PlayerStats;

                matchResult.AddResult(isWin, (int)myScore.Kills, decimal.Parse(myScore.KdRatio, CultureInfo.InvariantCulture));
                matchResult.MarkAsFinished();

                if (isBetsEnabled || matchResult.ContestId != null)
                {
                    var winnerId = await GetWinnerOptionAsync(matchResult);

                    await _seClient.PickWinner(matchResult.ContestId, winnerId);
                }
            }

            await faceItDbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<string> GetWinnerOptionAsync(MatchResult result)
        {
            var contest = await _seClient.GetBet(result.ContestId);

            if (contest.Title == ContestType.WinLose)
            {
                return result.IsWin ? result.FirstOptionId : result.SecondOptionId;
            }
            else if (contest.Title == ContestType.Kd)
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
