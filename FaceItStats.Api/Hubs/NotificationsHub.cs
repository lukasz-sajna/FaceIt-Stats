using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Hubs
{
    public class NotificationsHub : Hub, INotificationsHub
    {
        public async Task SendNotificationAsync(string method, object data, CancellationToken cancellationToken)
        {
            await Clients.All.SendAsync(method, data, cancellationToken);
        }
    }
}
