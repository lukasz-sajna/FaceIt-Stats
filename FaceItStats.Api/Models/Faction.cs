namespace FaceItStats.Api.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Faction
    {
        [JsonProperty("faction_id")]
        public Guid FactionId { get; set; }

        [JsonProperty("leader")]
        public Guid Leader { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("roster")]
        public List<Roster> Roster { get; set; }

        [JsonProperty("substituted")]
        public bool Substituted { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("team_id")]
        public string TeamId { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("players")]
        public List<Player> Players { get; set; }
    }
}
