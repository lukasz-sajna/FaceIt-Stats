namespace FaceItStats.Api.Client.Models
{
    using Helpers;
    using Newtonsoft.Json;

    public class PlayerMatchEloHistory
    {

        [JsonProperty("matchId")]
        public string MatchId { get; set; }

        [JsonProperty("elo")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Elo { get; set; }
    }
}
