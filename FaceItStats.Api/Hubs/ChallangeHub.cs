namespace FaceItStats.Api.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using System.Threading;
    using System.Threading.Tasks;

    public class ChallangeHub : Hub, IChallangeHub
    {

        public async Task SendNotificationAsync(string method, object data, CancellationToken cancellationToken)
        {
            await Clients.All.SendAsync(method, data, cancellationToken);
        }
    }
}
