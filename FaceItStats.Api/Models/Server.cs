using FaceItStats.Api.Helpers;
using Newtonsoft.Json;

namespace FaceItStats.Api.Models
{
    public class Server
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("port")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Port { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }
    }
}
