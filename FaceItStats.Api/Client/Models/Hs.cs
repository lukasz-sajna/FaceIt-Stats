using Newtonsoft.Json;

namespace FaceItStats.Api.Client.Models
{
    public class Hs
    {
        [JsonProperty("percentage")]
        public string Percentage { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }
    }
}
