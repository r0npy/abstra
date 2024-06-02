using Abstra.Core.Domains;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NLog;
using System.Text.Json;

namespace Abstra.Core.Repositories
{
    public class TransactionRepository(IConfiguration config) : ITransactionRepository
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string connectionString = config["ConnectionStrings:Abstra"]!;

        public Task<IEnumerable<Transaction?>?> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<Transaction?> Get(int id)
        {
            _logger.Trace($"Vamos a consultar la transaccion: {id}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"SELECT TransactionId, EventDate, TransactionType, Amount FROM [Transaction] WHERE TransactionId = @id";

            Transaction? record = await connection.QueryFirstOrDefaultAsync<Transaction>(sql, new { id });

            if (record == null)
                _logger.Warn($"No se encontró la transacción: {id}");
            else
                _logger.Info($"Se encontró la transacción: {JsonSerializer.Serialize(record)}");

            return record;
        }

        public async Task<Transaction> Create(Transaction record)
        {
            _logger.Trace($"Vamos a crear la transacción para la cuenta con id: {record.AccountId}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"INSERT INTO [Transaction] (AccountId, EventDate, TransactionType, Amount) 
                            VALUES (@AccountId, SYSDATETIME(), @TransactionType, @Amount)
                            SELECT SCOPE_IDENTITY();";

            record.TransactionId = await connection.ExecuteScalarAsync<int>(sql, record);

            _logger.Info($"Se ha creado la transacción: {JsonSerializer.Serialize(record)}");

            return record;
        }

        public async Task<decimal> GetBalance(int id)
        {
            _logger.Trace($"Vamos a consultar el saldo de la cuenta: {id}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"SELECT 
                                MAX(a.InitialBalance) + SUM(t.Amount) Balance 
                            FROM 
                                [Transaction] t 
                            JOIN 
                                Account a ON t.AccountId = a.AccountId 
                            WHERE 
                                t.AccountId = @id";

            decimal balance = await connection.QueryFirstOrDefaultAsync<decimal>(sql, new { id });

            _logger.Info($"El saldo de la cuenta es: {balance:###,###,##0.##}");

            return balance;
        }

        public async Task<IEnumerable<Transaction>?> GetRunningBalance(int accountId)
        {
            _logger.Trace($"Vamos a consultar las transacciones para la cuenta: {accountId}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"
                SELECT 
                    t.TransactionId,
                    t.AccountId, 
                    t.EventDate, 
                    t.TransactionType, 
                    t.Amount, 
                    a.InitialBalance + SUM(t.Amount) OVER (ORDER BY t.EventDate) AS RunningBalance
                FROM 
                    [Transaction] t
                JOIN 
                    Account a ON t.AccountId = a.AccountId
                WHERE 
                    t.AccountId = @accountId
                ORDER BY 
                    t.EventDate;";

            var records = await connection.QueryAsync<Transaction>(sql, new { accountId });

            _logger.Info($"Running Balance a devolver: {JsonSerializer.Serialize(records)}");

            return records;
        }
    }
}
