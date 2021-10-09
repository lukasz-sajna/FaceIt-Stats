using Newtonsoft.Json;
using System;

namespace FaceItStats.Api.Client.Models
{
    public class LatestMatch
    {
        public string team { get; set; }
        public string teamScore { get; set; }
        public string map { get; set; }
        public string kd { get; set; }

        [JsonProperty("hs")]
        public Headshot Headshots { get; set; }
        public string eloDiff { get; set; }
        public string kills { get; set; }
        public string death { get; set; }
        public string result { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
