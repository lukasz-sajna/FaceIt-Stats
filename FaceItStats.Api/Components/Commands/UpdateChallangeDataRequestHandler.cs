namespace FaceItStats.Api.Components.Commands
{
    using FaceItStats.Api.Hubs;
    using FaceItStats.Api.Persistence;
    using MediatR;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateChallangeDataRequestHandler : IRequestHandler<UpdateChallangeDataRequest>
    {
        private readonly FaceitDbContext dbContext;
        private readonly IHubContext<ChallangeHub> hubContext;

        public UpdateChallangeDataRequestHandler(FaceitDbContext dbContext, IHubContext<ChallangeHub> hubContext)
        {
            this.dbContext = dbContext;
            this.hubContext = hubContext;
        }

        public async Task<Unit> Handle(UpdateChallangeDataRequest request, CancellationToken cancellationToken)
        {
            var statsRow = await dbContext.ChallangeStats.FirstOrDefaultAsync(cancellationToken);

            if(statsRow == null)
            {
                statsRow = new Persistence.Models.ChallangeStats();
            }

            statsRow.Rank = request.challange.Rank;
            statsRow.Wins = request.challange.Wins;
            statsRow.Draws = request.challange.Draws;
            statsRow.Loses = request.challange.Loses;

            dbContext.ChallangeStats.Update(statsRow);
            await dbContext.SaveChangesAsync(cancellationToken);

            await this.hubContext.Clients.All.SendAsync("challangeStats", request.challange, cancellationToken);

            return Unit.Value;

        }
    }
}
