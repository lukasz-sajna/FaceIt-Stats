using Newtonsoft.Json;

namespace FaceItStats.Api.Client.Models
{
    public class FaceItUserResponse
    {
        [JsonProperty("player_id")]
        public string PlayerId { get; set; }
    }
}
