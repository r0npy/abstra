using System.Text.Json.Serialization;

namespace Abstra.Mappers.Requests
{
    public class TransactionPostRequestCreateDto
    {
        [JsonPropertyName("accountId")]
        public int? AccountId { get; set; }

        [JsonPropertyName("transactionType")]
        public char? TransactionType { get; set; }

        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }
    }
}
