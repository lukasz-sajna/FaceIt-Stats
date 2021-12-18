using Newtonsoft.Json;
using System;

namespace FaceItStats.Api.Models
{
    public class Roster
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("player_id")]
        public Guid PlayerId { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("membership")]
        public Game Membership { get; set; }

        [JsonProperty("game_player_id")]
        public string GamePlayerId { get; set; }

        [JsonProperty("game_player_name")]
        public string GamePlayerName { get; set; }

        [JsonProperty("game_skill_level")]
        public long GameSkillLevel { get; set; }

        [JsonProperty("anticheat_required")]
        public bool AnticheatRequired { get; set; }

        [JsonProperty("game_id")]
        public string GameId { get; set; }

        [JsonProperty("game_name")]
        public string GameName { get; set; }
    }
}
