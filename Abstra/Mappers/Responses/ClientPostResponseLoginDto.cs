using System.Text.Json.Serialization;

namespace Abstra.Mappers.Responses
{
    public class ClientPostResponseLoginDto
    {
        [JsonPropertyName("access_token")]
        public required string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public required string RefreshToken { get; set; }
    }
}
