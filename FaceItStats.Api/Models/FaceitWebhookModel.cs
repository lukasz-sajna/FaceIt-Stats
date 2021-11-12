using Newtonsoft.Json;
using System;

namespace FaceItStats.Api.Models
{
    public class FaceitWebhookModel
    {
        [JsonProperty("transaction_id")]
        public Guid TransactionId { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("event_id")]
        public Guid EventId { get; set; }

        [JsonProperty("third_party_id")]
        public Guid ThirdPartyId { get; set; }

        [JsonProperty("app_id")]
        public Guid AppId { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("retry_count")]
        public long RetryCount { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("payload")]
        public Payload Payload { get; set; }
    }


}
