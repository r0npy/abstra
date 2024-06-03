using System.Text.Json.Serialization;

namespace Abstra.Mappers.Requests
{
    public class ClientPostRequestLoginDto
    {
        [JsonPropertyName("userName")]
        public required string UserName { get; set; }

        [JsonPropertyName("password")]
        public required string Password { get; set; }
    }
}
