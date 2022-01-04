using System;

namespace FaceItStats.Api.Models
{
    public class NotificationData
    {
        public string Message { get; private set; }
        public string Type { get; private set; }
        public DateTime Date { get; private set; }

        public NotificationData(string message, string type)
        {
            Message = message;
            Type = type;
            Date = DateTime.Now;

        }
    }
}