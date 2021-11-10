using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FaceItStats.Api.Hubs
{
    public class FaceItStatsHub : Hub, IFaceItStatsHub
    {
        public async Task NotifyFaceItStatsChanged(string method)
        {
            await Clients.All.SendAsync(method);
        }
    }
}
