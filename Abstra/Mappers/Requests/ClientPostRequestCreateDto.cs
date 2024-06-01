using System.Text.Json.Serialization;

namespace Abstra.Mappers.Requests
{
    public class ClientPostRequestCreateDto
    {
        public string? Name { get; set; }
        public char Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
    }
}
