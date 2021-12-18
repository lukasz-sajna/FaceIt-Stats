namespace FaceItStats.Api.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class MatchStatistics
    {
        [JsonProperty("rounds")]
        public List<Round> Rounds { get; set; }
    }
}
