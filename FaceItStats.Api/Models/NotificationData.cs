using System;

namespace FaceItStats.Api.Models
{
    public class NotificationData(string message, string type)
    {
        public string Message { get; private set; } = message;
        public string Type { get; private set; } = type;
        public DateTime Date { get; private set; } = DateTime.Now;
    }
}