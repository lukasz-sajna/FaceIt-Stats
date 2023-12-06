using Newtonsoft.Json;

namespace FaceItStats.Api.Client.Models
{
    public class OptionResponse
    {
        [JsonProperty("totalAmount")]
        public long TotalAmount { get; set; }

        [JsonProperty("totalUsers")]
        public long TotalUsers { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("command")]
        public string Command { get; set; }
    }
}
