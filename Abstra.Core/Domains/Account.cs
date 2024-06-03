namespace Abstra.Core.Domains
{
    public class Account
    {
        public int AccountId { get; set; }
        public long AccountNumber { get; set; }
        public char? AccountType { get; set; }        
        public int? ClientId { get; set; }
        public decimal? InitialBalance { get; set; }
        public bool Status { get; set; }

        public Client? Client { get; set; }
    }
}
