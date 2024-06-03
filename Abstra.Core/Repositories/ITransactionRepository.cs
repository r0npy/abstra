using Abstra.Core.Domains;
using Abstra.Core.Repositories.Actions;

namespace Abstra.Core.Repositories
{
    public interface ITransactionRepository: IReadAction<Transaction?, int>, ICreateAction<Transaction, Transaction>
    {
        Task<decimal> GetBalance(int id);

        Task<IEnumerable<Transaction>?> GetRunningBalance(int accountId);
    }
}
