using FaceItStats.Api.Hubs;
using FaceItStats.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Controllers
{
    public class NotificationsController : ControllerBase
    {
        private readonly IHubContext<NotificationsHub> _hubContext;

        public NotificationsController(IHubContext<NotificationsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("Notification/{description}")]
        public async Task<IActionResult> PassNotification(string description, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("notification", new NotificationData(description, NotificationType.Success), cancellationToken);

            return Ok();
        }
    }
}
