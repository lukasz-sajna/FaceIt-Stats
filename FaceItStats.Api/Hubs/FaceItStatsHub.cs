using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Hubs
{
    public class FaceItStatsHub : Hub, IFaceItStatsHub
    {
        public Task NotifyFaceItStatsChangedAsync(string method, object data, CancellationToken cancellationToken)
        {
            return Clients.All.SendAsync(method, data, cancellationToken);
        }
    }
}
