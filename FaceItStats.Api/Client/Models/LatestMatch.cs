using Newtonsoft.Json;
using System;

namespace FaceItStats.Api.Client.Models
{
    public class LatestMatch
    {
        [JsonProperty("team")]
        public string Team { get; set; }

        [JsonProperty("teamScore")]
        public string TeamScore { get; set; }

        [JsonProperty("map")]
        public string Map { get; set; }

        [JsonProperty("kd")]
        public string Kd { get; set; }

        [JsonProperty("hs")]
        public Hs Hs { get; set; }

        [JsonProperty("eloDiff")]
        public string EloDiff { get; set; }

        [JsonProperty("kills")]
        public string Kills { get; set; }

        [JsonProperty("death")]
        public string Death { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
