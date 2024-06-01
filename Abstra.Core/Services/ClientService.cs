using Abstra.Core.Domains;
using Abstra.Core.Repositories;
using NLog;
using System.Text.Json;

namespace Abstra.Core.Services
{
    public class ClientService(IClientRepository clientRepository) : IClientService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task<Client?> Get(int id)
        {
            _logger.Trace($"Vamos a pasar al Repository clientRepository.Get({id})");
            return await clientRepository.Get(id);
        }

        public async Task<IEnumerable<Client>?> Get()
        {
            _logger.Trace("Vamos a pasar al Repository clientRepository.Get()");
            return await clientRepository.Get();   
        }

        public async Task<Client> Create(Client record)
        {
            _logger.Trace($"Vamos a pasar al Repository clientRepository.Create({JsonSerializer.Serialize(record)})");
            return await clientRepository.Create(record);
        }

        public async Task<int> Update(Client record)
        {
            _logger.Trace($"Vamos a pasar al Repository clientRepository.Update({JsonSerializer.Serialize(record)})");
            return await clientRepository.Update(record);
        }
    }
}
