﻿using System.Text.Json.Serialization;

namespace Pocketsharp.Objects
{
    public class Record
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;

        [JsonPropertyName("collectionId")]
        public string? CollectionId { get; set; } = string.Empty;

        [JsonPropertyName("collectionName")]
        public string? CollectionName { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        public string? Username { get; set; } = string.Empty;

        [JsonPropertyName("verified")]
        public bool? Verified { get; set; } = false;

        [JsonPropertyName("emailVisibility")]
        public bool? EmailVisibility { get; set; } = false;

        [JsonPropertyName("email")]
        public string? Email { get; set; } = string.Empty;

        [JsonPropertyName("created")]
        public DateTimeOffset? Created { get; set; } = DateTime.Now;

        [JsonPropertyName("updated")]
        public DateTimeOffset? Updated { get; set; } = DateTime.Now;

        [JsonPropertyName("name")]
        public string? Name { get; set; } = string.Empty;

        [JsonPropertyName("avatar")]
        public byte[]? Avatar { get; set; } = null;
    }
}