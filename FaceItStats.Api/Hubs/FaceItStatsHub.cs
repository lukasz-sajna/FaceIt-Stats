using FaceItStats.Api.Client;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FaceItStats.Api.Hubs
{
    public class FaceItStatsHub : Hub, IFaceItStatsHub
    {
        private readonly FaceItStatsClient _faceItClient;
        public FaceItStatsHub()
        {
            _faceItClient = new FaceItStatsClient();
        }

        public async Task NotifyFaceItStatsChanged()
        {
            var stats = await _faceItClient.GetStatsForNickname("luciusxsein");

            await Clients.All.SendAsync("StatsRefreshed", stats);
        }
    }
}
