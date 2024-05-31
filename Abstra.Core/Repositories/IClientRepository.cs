using Abstra.Core.Repositories.Actions;
using Abstra.Core.Domains;

namespace Abstra.Core.Repositories
{
    public interface IClientRepository : IReadAction<Client?, int>
    {
    }
}
