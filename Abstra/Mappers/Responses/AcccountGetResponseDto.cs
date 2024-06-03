using System.Text.Json.Serialization;

namespace Abstra.Mappers.Responses
{
    public class AcccountGetResponseDto
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("accountNumber")]
        public long AccountNumber { get; set; }

        [JsonPropertyName("accountType")]
        public string? AccountType { get; set; }

        [JsonPropertyName("clientId")]
        public int? ClientId { get; set; }

        [JsonPropertyName("clientName")]
        public string? ClientName { get; set; }

        [JsonPropertyName("initialBalance")]
        public decimal? InitialBalance { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

    }
}
