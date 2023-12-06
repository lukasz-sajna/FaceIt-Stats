namespace FaceItStats.Api.Components.Commands
{
    using Hubs;
    using Persistence;
    using MediatR;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateChallengeDataRequestHandler(FaceitDbContext dbContext, IHubContext<NotificationsHub> hubContext)
        : IRequestHandler<UpdateChallengeDataRequest>
    {
        public async Task Handle(UpdateChallengeDataRequest request, CancellationToken cancellationToken)
        {
            var statsRow = await dbContext.ChallengeStats.FirstOrDefaultAsync(cancellationToken) ?? new Persistence.Models.ChallengeStats();

            statsRow.Rank = request.Challenge.Rank;
            statsRow.Wins = request.Challenge.Wins;
            statsRow.Draws = request.Challenge.Draws;
            statsRow.Loses = request.Challenge.Loses;

            dbContext.ChallengeStats.Update(statsRow);
            await dbContext.SaveChangesAsync(cancellationToken);

            await hubContext.Clients.All.SendAsync("challengeStats", request.Challenge, cancellationToken);

        }
    }
}
