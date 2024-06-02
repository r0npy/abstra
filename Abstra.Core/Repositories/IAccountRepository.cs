using Abstra.Core.Domains;
using Abstra.Core.Repositories.Actions;

namespace Abstra.Core.Repositories
{
    public interface IAccountRepository: IReadAction<Account?, int>, ICreateAction<Account, Account>, IUpdateAction<Account>, IDeleteAction<int>
    {
    }
}
