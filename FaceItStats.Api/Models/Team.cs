using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FaceItStats.Api.Models
{
    public class Team
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("leader_id")]
        public Guid LeaderId { get; set; }

        [JsonProperty("co_leader_id")]
        public string CoLeaderId { get; set; }

        [JsonProperty("roster")]
        public List<Roster> Roster { get; set; }

        [JsonProperty("substitutions")]
        public long Substitutions { get; set; }

        [JsonProperty("substitutes")]
        public object Substitutes { get; set; }

        [JsonProperty("team_id")]
        public Guid TeamId { get; set; }

        [JsonProperty("premade")]
        public bool Premade { get; set; }

        [JsonProperty("team_stats")]
        public TeamStats TeamStats { get; set; }

        [JsonProperty("players")]
        public List<Player> Players { get; set; }
    }
}
