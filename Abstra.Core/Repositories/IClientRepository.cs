using Abstra.Core.Domains;
using Abstra.Core.Repositories.Actions;

namespace Abstra.Core.Repositories
{
    public interface IClientRepository : IReadAction<Client?, int>, ICreateAction<Client, Client>, IUpdateAction<Client>, IDeleteAction<int>
    {
    }
}
