using System.Text.Json.Serialization;

namespace Abstra.Mappers.Requests
{
    public class AccountPutRequestUpdateDto
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("accountNumber")]
        public long AccountNumber { get; set; }

        [JsonPropertyName("accountType")]
        public char? AccountType { get; set; }

        [JsonPropertyName("clientId")]
        public int? ClientId { get; set; }
    }
}
