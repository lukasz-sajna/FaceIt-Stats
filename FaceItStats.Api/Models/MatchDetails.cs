namespace FaceItStats.Api.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class MatchDetails
    {
        [JsonProperty("match_id")]
        public string MatchId { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("competition_id")]
        public Guid CompetitionId { get; set; }

        [JsonProperty("competition_type")]
        public string CompetitionType { get; set; }

        [JsonProperty("competition_name")]
        public string CompetitionName { get; set; }

        [JsonProperty("organizer_id")]
        public string OrganizerId { get; set; }

        [JsonProperty("teams")]
        public Teams Teams { get; set; }

        [JsonProperty("voting")]
        public Voting Voting { get; set; }

        [JsonProperty("calculate_elo")]
        public bool CalculateElo { get; set; }

        [JsonProperty("configured_at")]
        public long ConfiguredAt { get; set; }

        [JsonProperty("started_at")]
        public long StartedAt { get; set; }

        [JsonProperty("finished_at")]
        public long FinishedAt { get; set; }

        [JsonProperty("demo_url")]
        public List<Uri> DemoUrl { get; set; }

        [JsonProperty("chat_room_id")]
        public string ChatRoomId { get; set; }

        [JsonProperty("best_of")]
        public long BestOf { get; set; }

        [JsonProperty("results")]
        public Results Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("faceit_url")]
        public string FaceitUrl { get; set; }
    }
}
