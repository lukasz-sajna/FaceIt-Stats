namespace FaceItStats.Api.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class PlayerMatchHistory
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("end")]
        public long End { get; set; }

        [JsonProperty("from")]
        public long From { get; set; }

        [JsonProperty("to")]
        public long To { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("match_id")]
        public string MatchId { get; set; }

        [JsonProperty("game_id")]
        public string GameId { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("match_type")]
        public string MatchType { get; set; }

        [JsonProperty("game_mode")]
        public string GameMode { get; set; }

        [JsonProperty("max_players")]
        public long MaxPlayers { get; set; }

        [JsonProperty("teams_size")]
        public long TeamsSize { get; set; }

        [JsonProperty("teams")]
        public Teams Teams { get; set; }

        [JsonProperty("playing_players")]
        public List<Guid> PlayingPlayers { get; set; }

        [JsonProperty("competition_id")]
        public Guid CompetitionId { get; set; }

        [JsonProperty("competition_name")]
        public string CompetitionName { get; set; }

        [JsonProperty("competition_type")]
        public string CompetitionType { get; set; }

        [JsonProperty("organizer_id")]
        public string OrganizerId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("started_at")]
        public long StartedAt { get; set; }

        [JsonProperty("finished_at")]
        public long FinishedAt { get; set; }

        [JsonProperty("results")]
        public Results Results { get; set; }

        [JsonProperty("faceit_url")]
        public string FaceitUrl { get; set; }
    }
}
