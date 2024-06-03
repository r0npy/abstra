using System.Text.Json.Serialization;

namespace Abstra.Mappers.Responses
{
    public class TransactionGetResponseRunningBalanceDto
    {
        [JsonPropertyName("transactionId")]
        public long TransactionId { get; set; }

        [JsonPropertyName("eventDate")]
        public DateTime EventDate { get; set; }

        [JsonPropertyName("transactionType")]
        public string? TransactionType { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("runningBalance")]
        public decimal RunningBalance { get; set; }
    }
}
