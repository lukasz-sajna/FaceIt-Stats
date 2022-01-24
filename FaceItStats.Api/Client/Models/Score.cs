using Newtonsoft.Json;

namespace FaceItStats.Api.Client.Models
{
    public class Score
    {
        [JsonProperty("wins")]
        public long Wins { get; set; }

        [JsonProperty("loses")]
        public long Loses { get; set; }
    }
}
