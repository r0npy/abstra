using Abstra.Core.Domains;

namespace Abstra.Core.Services
{
    public interface IClientService
    {
        Task<Client?> Get(int id);

        Task<IEnumerable<Client>?> Get();

        Task<Client> Create(Client record);

        Task<int> Update(Client record);

        Task<int> Remove(int id);
    }
}
