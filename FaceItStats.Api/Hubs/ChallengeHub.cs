namespace FaceItStats.Api.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using System.Threading;
    using System.Threading.Tasks;

    public class ChallengeHub : Hub, IChallengeHub
    {

        public Task SendNotificationAsync(string method, object data, CancellationToken cancellationToken)
        {
            return Clients.All.SendAsync(method, data, cancellationToken);
        }
    }
}
