using Abstra.Core.Domains;

namespace Abstra.Core.Services
{
    public interface ITransactionService
    {
        Task<Transaction?> Get(int id);

        Task<Transaction> Create(Transaction record);

        Task<IEnumerable<Transaction>?> GetRunningBalance(int accountId);
    }
}
