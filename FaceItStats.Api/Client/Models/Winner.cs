using Newtonsoft.Json;

namespace FaceItStats.Api.Client.Models
{
    public class Winner(string winnerId)
    {
        [JsonProperty("winnerId")]
        public string WinnerId { get; } = winnerId;
    }
}
