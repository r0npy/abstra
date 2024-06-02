using Abstra.Core.Domains;
using Abstra.Core.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NLog;
using System.Text.Json;
using Dapper;

namespace Abstra.Core.Repositories
{
    public class AccountRepository(IConfiguration config) : IAccountRepository
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string connectionString = config["ConnectionStrings:Abstra"]!;

        public async Task<IEnumerable<Account>?> Get()
        {
            _logger.Trace($"Vamos a consultart todas las cuentas");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @" SELECT 
                                A.AccountId, A.AccountNumber, A.AccountNumber, A.AccountType, A.InitialBalance, A.Status, C.ClientId, C.Name 
                            FROM 
                                Account A
                            JOIN 
                                Client C ON A.ClientId = C.ClientId";

            IEnumerable<Account>? records = await connection.QueryAsync<Account, Client, Account>(
                sql,
                (account, client) =>
                {
                    account.Client = client;
                    return account;
                },
                splitOn: "ClientId"
            );

            if (records == null)
                _logger.Warn($"No se encontraron cuentas");
            else
                _logger.Info($"Se encontró esta lista de cuentas: {JsonSerializer.Serialize(records)}");

            return records;
        }

        public async Task<Account?> Get(int id)
        {
            _logger.Trace($"Vamos a consultar la cuenta: {id}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @" SELECT 
                                A.AccountId, A.AccountNumber, A.AccountNumber, A.AccountType, A.InitialBalance, A.Status, C.ClientId, C.Name 
                            FROM 
                                Account A
                            JOIN 
                                Client C ON A.ClientId = C.ClientId
                            WHERE 
                                A.AccountId = @id";

            Dictionary<int, Account> accountDictionary = [];

            await connection.QueryAsync<Account, Client, Account>(
                sql,
                (account, client) =>
                {
                    if (!accountDictionary.TryGetValue(account.AccountId, out var currentAccount))
                    {
                        currentAccount = account;
                        currentAccount.Client = client;
                        accountDictionary.Add(currentAccount.AccountId, currentAccount);
                    }

                    return currentAccount;
                },
                new { id },
                splitOn: "ClientId"
            );

            Account? record = accountDictionary.Values.FirstOrDefault();

            if (record == null)
                _logger.Warn($"No se encontró la cuenta con Id: {id}");
            else
                _logger.Info($"Se encontró la cuenta: {JsonSerializer.Serialize(record)}");

            return record;
        }

        public async Task<Account> Create(Account record)
        {
            _logger.Trace($"Vamos a crear la cuenta {record.AccountNumber}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"INSERT INTO dbo.Account (AccountNumber, AccountType, ClientId, InitialBalance, Status) 
                            VALUES (@AccountNumber, @AccountType, @ClientId, @InitialBalance, @Status)
                            SELECT SCOPE_IDENTITY();";

            record.Status = true;
            record.ClientId = await connection.ExecuteScalarAsync<int>(sql, record);

            _logger.Info($"Se ha creado la cuenta: {JsonSerializer.Serialize(record)}");

            return record;
        }

        public async Task<int> Update(Account record)
        {
            _logger.Trace($"Vamos a crear actualizar la cuenta {record.ClientId}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"UPDATE dbo.Account SET AccountNumber = @AccountNumber, AccountType = @AccountType, 
                            ClientId = @ClientId WHERE AccountId = @AccountId";

            int affectedRows = await connection.ExecuteAsync(sql, record);

            if (affectedRows == 0)
                throw new BussinessException("No se ha actualizado ningún registro");

            _logger.Info($"Se ha actualizado la cuenta: {JsonSerializer.Serialize(record)}");

            return affectedRows;
        }

        public async Task<int> Remove(int id)
        {
            _logger.Trace($"Vamos a eliminar la cuenta {id}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"DELETE FROM dbo.Account WHERE AccountId = @id";

            int affectedRows = await connection.ExecuteAsync(sql, new { id });

            if (affectedRows == 0)
                throw new BussinessException("No se ha eliminado ningún registro");

            _logger.Info($"Se ha eliminado la cuenta: {id} correctamente");

            return affectedRows;
        }
    }
}
