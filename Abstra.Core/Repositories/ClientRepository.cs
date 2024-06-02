
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

            string sql = @"SELECT ClientId, Name, Gender, Birthdate, Address, Phone, Status FROM Client";

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

            string sql = @"SELECT ClientId, Name, Gender, Birthdate, Address, Phone, Status FROM Client WHERE ClientId = @id";

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
                            Address = @Address, Phone = @Phone WHERE ClientId = @ClientId";

            int affectedRows = await connection.ExecuteAsync(sql, record);

            if (affectedRows == 0)
                throw new BussinessException("No se ha actualizado ningún registro");

            _logger.Info($"Se ha actualizado el cliente: {JsonSerializer.Serialize(record)}");

            return affectedRows;
        }

        public async Task<int> Remove(int id)
        {
            _logger.Trace($"Vamos a crear eliminar el cliente {id}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"DELETE FROM dbo.Client WHERE ClientId = @id";

            int affectedRows = await connection.ExecuteAsync(sql, new { id });

            if (affectedRows == 0)
                throw new BussinessException("No se ha eliminado ningún registro");

            _logger.Info($"Se ha eliminado el cliente: {id} correctamente");

            return affectedRows;
        }

        public async Task ChangePassword(int clientId, string oldPassword, string newPassword)
        {
            _logger.Trace($"Vamos a crear actualizar el password del cliente {clientId}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"UPDATE dbo.Client SET Password = @newPassword WHERE ClientId = @clientId and Password = @oldPassword";

            string salt = config["Salt"]!;

            int affectedRows = await connection.ExecuteAsync(sql, 
                new {
                    clientId, 
                    oldPassword = Helpers.Encrypt.ComputeSha512Hash(oldPassword + salt), 
                    newPassword = Helpers.Encrypt.ComputeSha512Hash(newPassword + salt)
                });

            if (affectedRows == 0)
                throw new BussinessException("No se ha actualizado el password");

            _logger.Info($"Se ha actualizado el password del cliente: {clientId}");
        }

        public async Task<Client?> Login(int clientId, string password)
        {
            _logger.Trace($"Vamos a realizar el login: {clientId}");

            await using SqlConnection connection = new(connectionString);

            await connection.OpenAsync();

            string sql = @"SELECT ClientId, Name, Gender, Birthdate, Address, Phone, Status from Client WHERE ClientId = @clientId and Password = @password";

            string salt = config["Salt"]!;

            Client? record = await connection.QueryFirstOrDefaultAsync<Client>(sql, new { clientId, password = Helpers.Encrypt.ComputeSha512Hash(password + salt) });

            return record;
        }
    }
}