using System.Text.Json.Serialization;

namespace Abstra.Mappers.Requests
{
    public class ClientPutRequesUpdateDto
    {
        [JsonPropertyName("clientId")]
        public required int ClientId { get; set; }

        [JsonPropertyName("name")]
        public required string? Name { get; set; }

        [JsonPropertyName("gender")]
        public required char Gender { get; set; }

        [JsonPropertyName("birthdate")]
        public required DateTime Birthdate { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
    }
}
