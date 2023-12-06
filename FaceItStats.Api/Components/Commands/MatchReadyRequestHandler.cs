using FaceItStats.Api.Persistence;
using FaceItStats.Api.Persistence.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Components.Commands
{
    public class MatchReadyRequestHandler(FaceitDbContext faceItDbContext) : IRequestHandler<MatchReadyRequest>
    {
        public async Task Handle(MatchReadyRequest request, CancellationToken cancellationToken)
        {
            var matchResult = faceItDbContext.MatchResult.FirstOrDefault(x => x.MatchId.Equals(request.MatchId)) ??
                              new MatchResult(request.MatchId);

            matchResult.MarkAsStarted();

            await faceItDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
