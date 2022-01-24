using Newtonsoft.Json;

namespace FaceItStats.Api.Client.Models
{
    public class LatestMatchesTrend
    {
        [JsonProperty("simple")]
        public string Simple { get; set; }

        [JsonProperty("extended")]
        public string Extended { get; set; }

        [JsonProperty("score")]
        public Score Score { get; set; }
    }
}
