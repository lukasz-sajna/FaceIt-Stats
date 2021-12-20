using FaceItStats.Api.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Components.Commands
{
    public class ResetStatsCommandHandler : IRequestHandler<ResetStatsCommand>
    {
        private readonly IHubContext<FaceItStatsHub> _hubContext;

        public ResetStatsCommandHandler(IHubContext<FaceItStatsHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task<Unit> Handle(ResetStatsCommand request, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("reset_stats", string.Empty, cancellationToken);

            return Unit.Value;
        }
    }
}
