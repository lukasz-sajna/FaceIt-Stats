using Newtonsoft.Json;
using System;

namespace FaceItStats.Api.Models
{
    public class Entity
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
