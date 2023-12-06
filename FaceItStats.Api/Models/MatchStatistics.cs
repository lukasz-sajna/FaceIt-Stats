namespace FaceItStats.Api.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class MatchStatistics
    {
        [JsonProperty("rounds")]
        public List<Round> Rounds { get; set; }
    }
}
