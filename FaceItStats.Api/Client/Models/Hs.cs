using FaceItStats.Api.Helpers;
using Newtonsoft.Json;

namespace FaceItStats.Api.Client.Models
{
    public class Hs
    {
        [JsonProperty("percentage")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Percentage { get; set; }

        [JsonProperty("number")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Number { get; set; }
    }
}
