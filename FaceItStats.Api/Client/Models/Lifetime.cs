using FaceItStats.Api.Helpers;
using Newtonsoft.Json;

namespace FaceItStats.Api.Client.Models
{
    public class Lifetime
    {
        [JsonProperty("Current Win Streak")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CurrentWinStreak { get; set; }

        [JsonProperty("Matches")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Matches { get; set; }

        [JsonProperty("Wins")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Wins { get; set; }

        [JsonProperty("Total Headshots %")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TotalHeadshots { get; set; }

        [JsonProperty("Average Headshots %")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AverageHeadshots { get; set; }

        [JsonProperty("K/D Ratio")]
        public string KDRatio { get; set; }

        [JsonProperty("Win Rate %")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long WinRate { get; set; }

        [JsonProperty("Longest Win Streak")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long LongestWinStreak { get; set; }

        [JsonProperty("Average K/D Ratio")]
        public string AverageKDRatio { get; set; }
    }
}
