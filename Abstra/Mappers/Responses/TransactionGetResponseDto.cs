using System.Text.Json.Serialization;

namespace Abstra.Mappers.Responses
{
    public class TransactionGetResponseDto
    {
        [JsonPropertyName("transactionId")]
        public int TransactionId { get; set; }

        [JsonPropertyName("accountId")]
        public DateTime? EventDate { get; set; }

        [JsonPropertyName("transactionType")]
        public string? TransactionType { get; set; }

        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }
    }
}
