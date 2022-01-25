namespace FaceItStats.Api.Models
{
    using System;
    using Newtonsoft.Json;

    public class Player
    {
        [JsonProperty("player_id")]
        public Guid PlayerId { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("player_stats")]
        public PlayerStats PlayerStats { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("faceit_url")]
        public string FaceitUrl { get; set; }

        [JsonProperty("game_player_id")]
        public string GamePlayerId { get; set; }

        [JsonProperty("game_player_name")]
        public string GamePlayerName { get; set; }

        [JsonProperty("skill_level")]
        public long SkillLevel { get; set; }
    }
}
