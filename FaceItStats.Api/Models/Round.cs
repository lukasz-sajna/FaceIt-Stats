namespace FaceItStats.Api.Models
{
    using System.Collections.Generic;
    using FaceItStats.Api.Helpers;
    using Newtonsoft.Json;

    public class Round
    {
        [JsonProperty("best_of")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long BestOf { get; set; }

        [JsonProperty("competition_id")]
        public object CompetitionId { get; set; }

        [JsonProperty("game_id")]
        public string GameId { get; set; }

        [JsonProperty("game_mode")]
        public string GameMode { get; set; }

        [JsonProperty("match_id")]
        public string MatchId { get; set; }

        [JsonProperty("match_round")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MatchRound { get; set; }

        [JsonProperty("played")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Played { get; set; }

        [JsonProperty("round_stats")]
        public RoundStats RoundStats { get; set; }

        [JsonProperty("teams")]
        public List<Team> Teams { get; set; }
    }
}
