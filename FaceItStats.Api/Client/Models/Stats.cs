using Newtonsoft.Json;

namespace FaceItStats.Api.Client.Models
{
    public class Stats
    {
        [JsonProperty("lifetime")]
        public Lifetime Lifetime { get; set; }
    }
}
