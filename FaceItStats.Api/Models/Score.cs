namespace FaceItStats.Api.Models
{
    using Newtonsoft.Json;

    public class Score
    {
        [JsonProperty("faction1")]
        public long Faction1 { get; set; }

        [JsonProperty("faction2")]
        public long Faction2 { get; set; }
    }
}
