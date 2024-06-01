using Abstra.Core.Domains;
using Abstra.Core.Repositories.Actions;

namespace Abstra.Core.Repositories
{
    public interface IClientRepository : IReadAction<Client?, int>
    {
        Task<IEnumerable<Client>?> Get();
    }
}
