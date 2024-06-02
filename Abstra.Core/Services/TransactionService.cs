using Abstra.Core.Domains;
using Abstra.Core.Exceptions;
using Abstra.Core.Repositories;
using NLog;
using System.Text.Json;

namespace Abstra.Core.Services
{
    public class TransactionService(ITransactionRepository transactionRepository) : ITransactionService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task<Transaction?> Get(int id)
        {
            _logger.Trace($"Vamos a pasar al Repository transactionRepository.Get({id})");
            return await transactionRepository.Get(id);
        }

        public async Task<Transaction> Create(Transaction record)
        {
            _logger.Trace($"Vamos a pasar al Repository transactionRepository.Create({JsonSerializer.Serialize(record)})");

            if (record.Amount == 0)
                throw new BussinessValidationException("El valor del movimiento nunca puede ser cero");

            if (record.TransactionType == 'C' && record.Amount < 0)
                throw new BussinessValidationException("Cuando el movimiento es un crédito, el valor debe ser positivo");

            if (record.TransactionType == 'D' && record.Amount > 0)
                throw new BussinessValidationException("Cuando el movimiento es un crédito, el valor debe ser negativo");

            decimal balance = await transactionRepository.GetBalance((int)record.AccountId!);

            decimal? newBalance = balance + record.Amount;

            if (newBalance < 0) 
                throw new BussinessValidationException("El monto del movimiento es superior al saldo disponible");

            return await transactionRepository.Create(record);
        }
    }
}
