using Abstra.Core.Domains;
using Abstra.Core.Repositories;
using NLog;
using System.Text.Json;

namespace Abstra.Core.Services
{
    public class AccountService(IAccountRepository accountRepository): IAccountService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task<Account?> Get(int id)
        {
            _logger.Trace($"Vamos a pasar al Repository accountRepository.Get({id})");
            return await accountRepository.Get(id);
        }

        public async Task<IEnumerable<Account>?> Get()
        {
            _logger.Trace("Vamos a pasar al Repository accountRepository.Get()");
            return await accountRepository.Get();
        }

        public async Task<Account> Create(Account record)
        {
            _logger.Trace($"Vamos a pasar al Repository accountRepository.Create({JsonSerializer.Serialize(record)})");
            return await accountRepository.Create(record);
        }

        public async Task<int> Update(Account record)
        {
            _logger.Trace($"Vamos a pasar al Repository accountRepository.Update({JsonSerializer.Serialize(record)})");
            return await accountRepository.Update(record);
        }

        public async Task<int> Remove(int id)
        {
            _logger.Trace($"Vamos a pasar al Repository accountRepository.Remove({id})");
            return await accountRepository.Remove(id);
        }
    }
}
