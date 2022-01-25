namespace FaceItStats.Api.Client.Models
{
    using FaceItStats.Api.Helpers;
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
