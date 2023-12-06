using FaceItStats.Api.Persistence;
using FaceItStats.Api.Persistence.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Components.Queries
{
    public class GetBetStateRequestHandler(FaceitDbContext faceItDbContext) : IRequestHandler<GetBetStateRequest, bool>
    {
        public async Task<bool> Handle(GetBetStateRequest request, CancellationToken cancellationToken)
        {
            var betSettings = faceItDbContext.BetsSettings.FirstOrDefault();

            if (betSettings == null)
            {
                betSettings = new BetsSettings { IsEnabled = false };

                faceItDbContext.BetsSettings.Add(betSettings);
                await faceItDbContext.SaveChangesAsync(cancellationToken);
            }

            return betSettings.IsEnabled;
        }
    }
}
