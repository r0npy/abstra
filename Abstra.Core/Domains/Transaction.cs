namespace Abstra.Core.Domains
{
    public class Transaction
    {
        public long TransactionId { get; set; }
        public virtual Account? Account { get; set; }
        public int? AccountId { get; set; }
        public DateTime? EventDate { get; set; }
        public string? TransactionType { get; set; }
        public decimal? Ammount { get; set; }
    }
}
