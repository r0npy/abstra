using System.Text.Json.Serialization;

namespace Abstra.Mappers.Requests
{
    public class ClientPostRequestLoginDto
    {
        [JsonPropertyName("clientId")]
        public required int ClientId { get; set; }

        [JsonPropertyName("password")]
        public required string Password { get; set; }
    }
}
