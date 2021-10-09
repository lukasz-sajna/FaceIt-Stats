using Newtonsoft.Json;

namespace FaceItStats.Api.Client.Models
{
    public class Lifetime
    {
        [JsonProperty("Average Headshots %")]
        public string AverageHeadshots { get; set; }

        [JsonProperty("Total Headshots %")]
        public string TotalHeadshots { get; set; }

        [JsonProperty("Longest Win Streak")]
        public string LongestWinStreak { get; set; }
        public string Wins { get; set; }

        [JsonProperty("Average K/D Ratio")]
        public string AverageKDRatio { get; set; }
        public string Matches { get; set; }

        [JsonProperty("Current Win Streak")]
        public string CurrentWinStreak { get; set; }

        [JsonProperty("Win Rate %")]
        public string WinRate { get; set; }

        [JsonProperty("K/D Ratio")]
        public string KDRatio { get; set; }
    }
}
