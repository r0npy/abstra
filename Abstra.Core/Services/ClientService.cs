using Abstra.Core.Domains;
using Abstra.Core.Repositories;
using NLog;

namespace Abstra.Core.Services
{
    public class ClientService(IClientRepository clientRepository) : IClientService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task<Client?> Get(int id)
        {
            return await clientRepository.Get(id);
        }

        public async Task<IEnumerable<Client>?> Get()
        {
            return await clientRepository.Get();   
        }
    }
}
