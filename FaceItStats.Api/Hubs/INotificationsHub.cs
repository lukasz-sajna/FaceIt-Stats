using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Hubs
{
    internal interface INotificationsHub
    {
        Task SendNotificationAsync(string method, object data, CancellationToken cancellationToken);
    }
}
