namespace FaceItStats.Api.Models
{
    using Newtonsoft.Json;

    public class Results
    {
        [JsonProperty("winner")]
        public string Winner { get; set; }

        [JsonProperty("score")]
        public Score Score { get; set; }
    }
}
