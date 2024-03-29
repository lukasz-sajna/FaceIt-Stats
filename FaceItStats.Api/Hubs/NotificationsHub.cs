﻿using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Hubs
{
    public class NotificationsHub : Hub, INotificationsHub
    {
        public Task SendNotificationAsync(string method, object data, CancellationToken cancellationToken)
        {
            return Clients.All.SendAsync(method, data, cancellationToken);
        }
    }
}
