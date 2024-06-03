using Dapper.Contrib.Extensions;

namespace Abstra.Core.Domains
{
    public class Transaction
    {
        public long TransactionId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? EventDate { get; set; }
        public char? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        [Computed]
        public decimal? RunningBalance { get; set; }

        public Account? Account { get; set; }
    }
}
