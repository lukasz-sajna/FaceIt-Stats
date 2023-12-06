using FaceItStats.Api.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Components.Commands
{
    public class SetBetStateRequestHandler(FaceitDbContext faceItDbContext) : IRequestHandler<SetBetStateRequest>
    {
        public async Task Handle(SetBetStateRequest request, CancellationToken cancellationToken)
        {
            var betSettings = faceItDbContext.BetsSettings.FirstOrDefault();

            if(betSettings is null)
            {
                betSettings = new Persistence.Models.BetsSettings { IsEnabled = request.IsEnabled };
                faceItDbContext.Add(betSettings);
            }

            betSettings.IsEnabled = request.IsEnabled;

            await faceItDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
