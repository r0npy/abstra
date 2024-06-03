using System.Text.Json.Serialization;

namespace Abstra.Mappers.Requests
{
    public class ClientPostRequestCreateDto
    {
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

        [JsonPropertyName("password")]
        public required string Password { get; set; }

        public override string ToString()
        {
            return $"{{ name:{Name}, gender: {Gender}, birthdate: {Birthdate}, address: {Address}, phone: {Phone} }}";
        }
    }
}
