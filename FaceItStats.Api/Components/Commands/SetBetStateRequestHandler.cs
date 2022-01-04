using FaceItStats.Api.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Components.Commands
{
    public class SetBetStateRequestHandler : IRequestHandler<SetBetStateRequest>
    {
        private readonly FaceitDbContext _faceItDbContext;

        public SetBetStateRequestHandler(FaceitDbContext faceItDbContext)
        {
            _faceItDbContext = faceItDbContext;
        }

        public async Task<Unit> Handle(SetBetStateRequest request, CancellationToken cancellationToken)
        {
            var betSettings = _faceItDbContext.BetsSettings.FirstOrDefault();

            if(betSettings == null)
            {
                betSettings = new Persistence.Models.BetsSettings { IsEnabled = request.IsEnabled };
                _faceItDbContext.Add(betSettings);
            }

            betSettings.IsEnabled = request.IsEnabled;

            await _faceItDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
