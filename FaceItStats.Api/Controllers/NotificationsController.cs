using FaceItStats.Api.Hubs;
using FaceItStats.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Controllers
{
    public class NotificationsController(IHubContext<NotificationsHub> hubContext) : ControllerBase
    {
        [HttpPost("Notification/{description}")]
        public async Task<IActionResult> PassNotification(string description, CancellationToken cancellationToken)
        {
            await hubContext.Clients.All.SendAsync("notification", new NotificationData(description, NotificationType.Success), cancellationToken);

            return Ok();
        }
    }
}
