using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Hubs
{
    interface INotificationsHub
    {
        Task SendNotificationAsync(string method, object data, CancellationToken cancellationToken);
    }
}
