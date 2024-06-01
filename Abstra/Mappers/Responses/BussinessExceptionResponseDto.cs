using System.Text.Json.Serialization;

namespace Abstra.Mappers.Responses
{
    public class BussinessExceptionResponseDto(int statusCode, string? message = null, string? detail = null)
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; } = statusCode;

        [JsonPropertyName("message")]
        public string? Message { get; } = message;

        [JsonPropertyName("detail")]
        public string? Detail { get; } = detail;
    }
}
