namespace FaceItStats.Api.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ContenstResponse
    {
        [JsonProperty("botResponses")]
        public bool BotResponses { get; set; }

        [JsonProperty("totalAmount")]
        public long TotalAmount { get; set; }

        [JsonProperty("totalUsers")]
        public long TotalUsers { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("minBet")]
        public long MinBet { get; set; }

        [JsonProperty("maxBet")]
        public long MaxBet { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("options")]
        public List<OptionResponse> Options { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("startedAt")]
        public DateTimeOffset StartedAt { get; set; }

        [JsonProperty("endedAt")]
        public DateTimeOffset EndedAt { get; set; }
    }
}
