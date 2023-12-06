namespace FaceItStats.Api.Models
{
    using Helpers;
    using Newtonsoft.Json;

    public class PlayerStats
    {
        [JsonProperty("Quadro Kills")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long QuadroKills { get; set; }

        [JsonConverter(typeof(ParseStringConverter))]
        public long Headshots { get; set; }

        [JsonProperty("Headshots %")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PlayerStatsHeadshots { get; set; }

        [JsonProperty("Triple Kills")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TripleKills { get; set; }

        [JsonConverter(typeof(ParseStringConverter))]
        public long Kills { get; set; }

        [JsonConverter(typeof(ParseStringConverter))]
        public long Assists { get; set; }

        [JsonProperty("Penta Kills")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PentaKills { get; set; }

        [JsonProperty("MVPs")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MvPs { get; set; }

        [JsonProperty("K/R Ratio")]
        public string KrRatio { get; set; }

        [JsonConverter(typeof(ParseStringConverter))]
        public long Result { get; set; }

        [JsonConverter(typeof(ParseStringConverter))]
        public long Deaths { get; set; }

        [JsonProperty("K/D Ratio")]
        public string KdRatio { get; set; }
    }
}
