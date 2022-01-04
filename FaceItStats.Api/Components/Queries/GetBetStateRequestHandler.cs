using FaceItStats.Api.Persistence;
using FaceItStats.Api.Persistence.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Components.Queries
{
    public class GetBetStateRequestHandler : IRequestHandler<GetBetStateRequest, bool>
    {
        private readonly FaceitDbContext _faceItDbContext;

        public GetBetStateRequestHandler(FaceitDbContext faceItDbContext)
        {
            _faceItDbContext = faceItDbContext;
        }

        public async Task<bool> Handle(GetBetStateRequest request, CancellationToken cancellationToken)
        {
            var betSettings = _faceItDbContext.BetsSettings.FirstOrDefault();

            if (betSettings == null)
            {
                betSettings = new BetsSettings { IsEnabled = false };

                _faceItDbContext.BetsSettings.Add(betSettings);
                await _faceItDbContext.SaveChangesAsync(cancellationToken);
            }

            return betSettings.IsEnabled;
        }
    }
}
