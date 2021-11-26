using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Hubs
{
    public interface IFaceItStatsHub
    {
        Task NotifyFaceItStatsChangedAsync(string method, object data, CancellationToken cancellationToken);
    }
}