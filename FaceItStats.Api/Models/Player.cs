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
    }
}
