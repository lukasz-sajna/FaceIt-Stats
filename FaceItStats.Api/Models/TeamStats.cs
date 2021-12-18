namespace FaceItStats.Api.Models
{
    using FaceItStats.Api.Helpers;
    using Newtonsoft.Json;

    public class TeamStats
    {
        [JsonProperty("First Half Score")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FirstHalfScore { get; set; }

        [JsonProperty("Second Half Score")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SecondHalfScore { get; set; }

        [JsonProperty("Team")]
        public string Team { get; set; }

        [JsonProperty("Team Win")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TeamWin { get; set; }

        [JsonProperty("Final Score")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FinalScore { get; set; }

        [JsonProperty("Team Headshots")]
        public string TeamHeadshots { get; set; }

        [JsonProperty("Overtime score")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long OvertimeScore { get; set; }
    }
}
