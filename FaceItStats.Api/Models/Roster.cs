using Newtonsoft.Json;
using System;

namespace FaceItStats.Api.Models
{
    public class Roster
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("game_id")]
        public string GameId { get; set; }

        [JsonProperty("game_name")]
        public string GameName { get; set; }

        [JsonProperty("game_skill_level")]
        public long GameSkillLevel { get; set; }

        [JsonProperty("membership")]
        public string Membership { get; set; }

        [JsonProperty("anticheat_required")]
        public bool AnticheatRequired { get; set; }
    }
}
