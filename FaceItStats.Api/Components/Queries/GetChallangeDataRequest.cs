namespace FaceItStats.Api.Components.Queries
{
    using Models;
    using Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetChallengeDataRequest : IRequest<ChallengeData>
    {
    }

    public class GetChallengeDataHandler(FaceitDbContext context)
        : IRequestHandler<GetChallengeDataRequest, ChallengeData>
    {
        public async Task<ChallengeData> Handle(GetChallengeDataRequest request, CancellationToken cancellationToken)
        {
            var statsRow = await context.ChallengeStats.FirstOrDefaultAsync(cancellationToken);

            if (statsRow is null)
            {
                statsRow = new Persistence.Models.ChallengeStats
                {
                    Rank = 0,
                    Wins = 0,
                    Draws = 0,
                    Loses = 0
                };

                context.ChallengeStats.Add(statsRow);
                await context.SaveChangesAsync(cancellationToken);
            }

            return new ChallengeData(statsRow.Rank, statsRow.Wins, statsRow.Draws, statsRow.Loses);
        }
    }
}
