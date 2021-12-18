namespace FaceItStats.Api.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Location
    {
        [JsonProperty("entities")]
        public List<FaceItEntity> Entities { get; set; }

        [JsonProperty("pick")]
        public List<string> Pick { get; set; }
    }
}
