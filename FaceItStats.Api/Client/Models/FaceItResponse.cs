using Newtonsoft.Json;
using System.Collections.Generic;

namespace FaceItStats.Api.Client.Models
{

    public class FaceItResponse
    {
        [JsonProperty("elo")]
        public long Elo { get; set; }

        [JsonProperty("lvl")]
        public long Lvl { get; set; }

        [JsonProperty("todayEloDiff")]
        public string TodayEloDiff { get; set; }

        [JsonProperty("latestMatchesTrend")]
        public LatestMatchesTrend LatestMatchesTrend { get; set; }

        [JsonProperty("latestMatches")]
        public List<LatestMatch> LatestMatches { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }
        public string ladder { get; set; }
        public string report { get; set; }
        public string trend { get; set; }
        public string last_match { get; set; }
    }
}
