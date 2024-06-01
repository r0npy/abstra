﻿using System.Text.Json.Serialization;

namespace Abstra.Mappers.Responses
{
    public class ClientGetResponseDto
    {
        [JsonPropertyName("ClientId")]
        public int? ClientId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("birthdate")]
        public DateTime? Birthdate { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}