namespace FaceItStats.Api.Client.Models
{
    using Newtonsoft.Json;

    public class CurrentMatch
    {
        [JsonProperty("gain")]
        public int Gain { get; set; }

        [JsonProperty("lose")]
        public int Lose { get; set; }
    }
}