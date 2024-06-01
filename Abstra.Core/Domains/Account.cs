namespace Abstra.Core.Domains
{
    public class Account
    {
        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public char? AccountType { get; set; }
        public virtual Client? Client { get; set; }
        public int? ClientId { get; set; }
        public decimal? InitialBalance { get; set; }
        public bool Status { get; set; }
    }
}
