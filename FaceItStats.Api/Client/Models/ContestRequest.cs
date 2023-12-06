using System.Collections.Generic;
using Newtonsoft.Json;

namespace FaceItStats.Api.Client.Models
{
    public class ContestRequest
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("minBet")]
        public int MinBet { get; set; }

        [JsonProperty("maxBet")]
        public int MaxBet { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("botResponses")]
        public bool BotResponses { get; set; }

        [JsonProperty("options")]
        public List<OptionRequest> Options { get; set; }
    }

    public class OptionRequest
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("command")]
        public string Command { get; set; }
    }
}
