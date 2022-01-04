using FaceItStats.Api.Persistence;
using FaceItStats.Api.Persistence.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Components.Commands
{
    public class MatchReadyRequestHandler : IRequestHandler<MatchReadyRequest>
    {
        private readonly FaceitDbContext _faceItDbContext;

        public MatchReadyRequestHandler(FaceitDbContext faceItDbContext)
        {
            _faceItDbContext = faceItDbContext;
        }

        public async Task<Unit> Handle(MatchReadyRequest request, CancellationToken cancellationToken)
        {
            var matchResult = _faceItDbContext.MatchResult.FirstOrDefault(x => x.MatchId.Equals(request.MatchId));

            if (matchResult == null)
            {
                matchResult = new MatchResult(request.MatchId);
            }

            matchResult.MarkAsStarted();

            await _faceItDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
