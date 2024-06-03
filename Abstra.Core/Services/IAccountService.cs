using Abstra.Core.Domains;

namespace Abstra.Core.Services
{
    public interface IAccountService
    {
        Task<Account?> Get(int id);

        Task<IEnumerable<Account>?> Get();

        Task<Account> Create(Account record);

        Task<int> Update(Account record);

        Task<int> Remove(int id);
    }
}
