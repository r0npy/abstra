
using Abstra.Core.Domains;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NLog;
using System.Text.Json;
using Abstra.Core.Exceptions;

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

            string sql = @"SELECT ClientId, Name, Gender, Birthdate, Address, Phone, Status from Client";

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

            string sql = @"SELECT ClientId, Name, Gender, Birthdate, Address, Phone, Status from Client WHERE ClientId = @id";

            Client? record = await connection.QueryFirstOrDefaultAsync<Client>(sql, new { id });

            if (record == null)
                _logger.Warn($"No se encontró el User con Id: {id}");
            else
                _logger.Info($"Se encontró el User: {JsonSerializer.Serialize(record)}");

            return record;
        }

        public async Task<Client> Create(Client record)
        {
            _logger.Trace($"Vamos a crear usuario {record.Name}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"INSERT INTO dbo.Client (Name, Gender, Birthdate, Address, Phone, Password, Status) 
                            VALUES (@Name, @Gender, @Birthdate, @Address, @Phone, @Password, @Status)
                            SELECT SCOPE_IDENTITY();";

            string salt = config["Salt"]!;
            record.Status = true;
            record.Password = Helpers.Encrypt.ComputeSha512Hash(record.Password + salt);

            record.ClientId = await connection.ExecuteScalarAsync<int>(sql, record);

            _logger.Info($"Se ha creado el cliente: {JsonSerializer.Serialize(record)}");

            return record;
        }

        public async Task<int> Update(Client record)
        {
            _logger.Trace($"Vamos a crear actualizar el cliente {record.ClientId}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"UPDATE dbo.Client SET Name = @Name, Gender = @Gender, Birthdate = @Birthdate, 
                            Address = @Address, Phone = @Phone, Status = @Status WHERE ClientId = @ClientId";

            int affectedRows = await connection.ExecuteAsync(sql, record);

            if (affectedRows == 0)
                throw new BussinessException("No se ha actualizado ningún registro");

            _logger.Info($"Se ha actualizado el cliente: {JsonSerializer.Serialize(record)}");

            return affectedRows;
        }
    }
}