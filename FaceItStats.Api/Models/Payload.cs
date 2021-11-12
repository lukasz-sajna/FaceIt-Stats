using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FaceItStats.Api.Models
{
    public class Payload
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("organizer_id")]
        public string OrganizerId { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("entity")]
        public Entity Entity { get; set; }

        [JsonProperty("teams")]
        public List<Team> Teams { get; set; }

        [JsonProperty("client_custom")]
        public ClientCustom ClientCustom { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("started_at")]
        public DateTimeOffset StartedAt { get; set; }

        [JsonProperty("finished_at")]
        public DateTimeOffset FinishedAt { get; set; }
    }
}
