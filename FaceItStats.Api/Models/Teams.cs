namespace FaceItStats.Api.Models
{
    using Newtonsoft.Json;

    public class Teams
    {
        [JsonProperty("faction2")]
        public Faction Faction2 { get; set; }

        [JsonProperty("faction1")]
        public Faction Faction1 { get; set; }
    }
}
