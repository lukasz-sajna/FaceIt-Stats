namespace FaceItStats.Api.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Voting
    {
        [JsonProperty("voted_entity_types")]
        public List<string> VotedEntityTypes { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("map")]
        public Location Map { get; set; }
    }
}
