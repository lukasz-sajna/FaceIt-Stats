namespace FaceItStats.Api.Components.Queries
{
    using FaceItStats.Api.Models;
    using FaceItStats.Api.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetChallangeDataRequest : IRequest<ChallangeData>
    {
    }

    public class GetChallangeDataHandler : IRequestHandler<GetChallangeDataRequest, ChallangeData>
    {
        private readonly FaceitDbContext dbContext;

        public GetChallangeDataHandler(FaceitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ChallangeData> Handle(GetChallangeDataRequest request, CancellationToken cancellationToken)
        {
            var statsRow = await this.dbContext.ChallangeStats.FirstOrDefaultAsync(cancellationToken);

            if (statsRow == null)
            {
                statsRow = new Persistence.Models.ChallangeStats
                {
                    Rank = 0,
                    Wins = 0,
                    Draws = 0,
                    Loses = 0
                };

                dbContext.ChallangeStats.Add(statsRow);
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            return new ChallangeData { Rank = statsRow.Rank, Wins = statsRow.Wins, Draws = statsRow.Draws, Loses = statsRow.Loses };
        }
    }
}
