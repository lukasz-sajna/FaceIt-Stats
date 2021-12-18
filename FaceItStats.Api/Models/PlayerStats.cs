namespace FaceItStats.Api.Models
{
    using FaceItStats.Api.Helpers;
    using Newtonsoft.Json;

    public class PlayerStats
    {
        [JsonProperty("Quadro Kills")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long QuadroKills { get; set; }

        [JsonProperty("Headshots")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Headshots { get; set; }

        [JsonProperty("Headshots %")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PlayerStatsHeadshots { get; set; }

        [JsonProperty("Triple Kills")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TripleKills { get; set; }

        [JsonProperty("Kills")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Kills { get; set; }

        [JsonProperty("Assists")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Assists { get; set; }

        [JsonProperty("Penta Kills")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PentaKills { get; set; }

        [JsonProperty("MVPs")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MvPs { get; set; }

        [JsonProperty("K/R Ratio")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal KRRatio { get; set; }

        [JsonProperty("Result")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Result { get; set; }

        [JsonProperty("Deaths")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Deaths { get; set; }

        [JsonProperty("K/D Ratio")]
        [JsonConverter(typeof(DecimalConverter))]
        public decimal KDRatio { get; set; }
    }
}
