using System.Text.Json.Serialization;

namespace Abstra.Mappers.Requests
{
    public class AccountPostRequestCreateDto
    {
        [JsonPropertyName("accountNumber")]
        public long AccountNumber { get; set; }

        [JsonPropertyName("accountType")]
        public char? AccountType { get; set; }

        [JsonPropertyName("clientId")]
        public int? ClientId { get; set; }

        [JsonPropertyName("initialBalance")]
        public decimal? InitialBalance { get; set; }
    }
}
