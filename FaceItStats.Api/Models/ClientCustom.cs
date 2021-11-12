using Newtonsoft.Json;

namespace FaceItStats.Api.Models
{
    public class ClientCustom
    {
        [JsonProperty("server_ip")]
        public string ServerIp { get; set; }

        [JsonProperty("server")]
        public Server Server { get; set; }

        [JsonProperty("server_port")]
        public long ServerPort { get; set; }

        [JsonProperty("map")]
        public string Map { get; set; }

        [JsonProperty("match_id")]
        public string MatchId { get; set; }
    }
}
