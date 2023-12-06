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

        [JsonProperty("currentMatch")]
        public CurrentMatch CurrentMatch { get; set; }

        [JsonProperty("latestMatchesTrend")]
        public LatestMatchesTrend LatestMatchesTrend { get; set; }

        [JsonProperty("latestMatches")]
        public List<LatestMatch> LatestMatches { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }

        public string Ladder { get; set; }

        public string Report { get; set; }

        public string Trend { get; set; }

        [JsonProperty("last_match")]
        public string LastMatch { get; set; }
    }
}
