namespace Abstra.Core.Domains
{
    public class Client
    {
        public int ClientId { get; set; }
        public string? Name { get; set; }
        public char Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public bool Status { get; set; }
    }
}
