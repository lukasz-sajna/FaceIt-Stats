using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FaceItStats.Api.Models
{

    public class Temperatures
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

    public class Payload
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("organizer_id")]
        public string OrganizerId { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("entity")]
        public Entity Entity { get; set; }

        [JsonProperty("teams")]
        public List<Team> Teams { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("started_at")]
        public DateTimeOffset StartedAt { get; set; }

        [JsonProperty("finished_at")]
        public DateTimeOffset FinishedAt { get; set; }
    }

    public class Entity
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Team
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("leader_id")]
        public Guid LeaderId { get; set; }

        [JsonProperty("co_leader_id")]
        public string CoLeaderId { get; set; }

        [JsonProperty("roster")]
        public List<Roster> Roster { get; set; }

        [JsonProperty("substitutions")]
        public long Substitutions { get; set; }

        [JsonProperty("substitutes")]
        public object Substitutes { get; set; }
    }

    public class Roster
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("game_id")]
        public string GameId { get; set; }

        [JsonProperty("game_name")]
        public string GameName { get; set; }

        [JsonProperty("game_skill_level")]
        public long GameSkillLevel { get; set; }

        [JsonProperty("membership")]
        public string Membership { get; set; }

        [JsonProperty("anticheat_required")]
        public bool AnticheatRequired { get; set; }
    }
}
