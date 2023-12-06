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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Components.Commands
{
    public class MatchCancelledRequestHandler(
        FaceitDbContext faceItDbContext,
        IOptions<Auth> authSettings,
        IHubContext<NotificationsHub> hubContext)
        : IRequestHandler<MatchCancelledRequest>
    {
        private readonly SeClient _seClient = SeClient.CreateInstance(authSettings.Value.SeToken, hubContext);

        public async Task Handle(MatchCancelledRequest request, CancellationToken cancellationToken)
        {
            var betSettings = await faceItDbContext.BetsSettings.FirstOrDefaultAsync(cancellationToken);
            var isBetsEnabled = betSettings is { IsEnabled: true };

            var matchResult = faceItDbContext.MatchResult.FirstOrDefault(x => x.MatchId.Equals(request.MatchId));

            if (matchResult == null)
            {
                matchResult = new MatchResult(request.MatchId);
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

            await faceItDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
