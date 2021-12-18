namespace FaceItStats.Api.Models
{
    using System;
    using Newtonsoft.Json;

    public class FaceItEntity
    {
        [JsonProperty("image_sm")]
        public Uri ImageSm { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("class_name")]
        public string ClassName { get; set; }

        [JsonProperty("game_location_id", NullValueHandling = NullValueHandling.Ignore)]
        public string GameLocationId { get; set; }

        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("image_lg")]
        public Uri ImageLg { get; set; }

        [JsonProperty("game_map_id", NullValueHandling = NullValueHandling.Ignore)]
        public string GameMapId { get; set; }
    }
}
