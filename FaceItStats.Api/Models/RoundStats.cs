namespace FaceItStats.Api.Models
{
    using System;
    using FaceItStats.Api.Helpers;
    using Newtonsoft.Json;

    public class RoundStats
    {
        [JsonProperty("Rounds")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Rounds { get; set; }

        [JsonProperty("Winner")]
        public Guid Winner { get; set; }

        [JsonProperty("Region")]
        public string Region { get; set; }

        [JsonProperty("Map")]
        public string Map { get; set; }

        [JsonProperty("Score")]
        public string Score { get; set; }
    }
}
