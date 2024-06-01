
using Abstra.Core.Domains;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NLog;
using System.Text.Json;

namespace Abstra.Core.Repositories
{
    public class ClientRepository(IConfiguration config) : IClientRepository
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string connectionString = config["ConnectionStrings:Abstra"]!;  

        public async Task<IEnumerable<Client>?> Get()
        {
            _logger.Trace($"Vamos a consultar todos los Clientes");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"SELECT ClientId, Name, Gender, Birthdate, Address, Phone from Client";

            IEnumerable<Client>? records = await connection.QueryAsync<Client>(sql);

            if (records == null)
                _logger.Warn($"No se encontraron usuarios");
            else
                _logger.Info($"Se encontró esta lista de usuarios: {JsonSerializer.Serialize(records)}");

            return records;
        }

        public async Task<Client?> Get(int id)
        {
            _logger.Trace($"Vamos a consultar si existe el Cliente: {id}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"SELECT ClientId, Name, Gender, Birthdate, Address, Phone from Client WHERE ClientId = @id";

            Client? record = await connection.QueryFirstOrDefaultAsync<Client>(sql, new { id });

            if (record == null)
                _logger.Warn($"No se encontró el User con Id: {id}");
            else
                _logger.Info($"Se encontró el User: {JsonSerializer.Serialize(record)}");

            return record;
        }
    }
}