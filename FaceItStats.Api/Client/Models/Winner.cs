using Newtonsoft.Json;

namespace FaceItStats.Api.Client.Models
{
    public class Winner
    {
        public Winner(string winnerId)
        {
            WinnerId = winnerId;
        }

        [JsonProperty("winnerId")]
        public string WinnerId { get; }
    }
}
