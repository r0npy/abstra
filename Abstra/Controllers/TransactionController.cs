using Abstra.Core.Domains;
using Abstra.Core.Exceptions;
using Abstra.Core.Services;
using Abstra.Mappers.Requests;
using Abstra.Mappers.Responses;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Text.Json;

namespace Abstra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(ITransactionService transactionService) : ControllerBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(TransactionGetResponseDto))]
        [ProducesResponseType(204)]
        [ProducesResponseType(422, Type = typeof(BussinessExceptionResponseDto))]
        [Authorize(Policy = "BearerToken")]
        public async Task<ActionResult<TransactionGetResponseDto?>> Get(int id)
        {
            _logger.Info($"Recibiendo un pedido para recuperar la transacción {id}");

            if (id <= 0)
            {
                string message = $"El id {id} no puede ser menor a 1";
                _logger.Error(message);
                throw new BussinessException(message);
            }

            Transaction? record = await transactionService.Get(id);

            _logger.Info($"Cuenta recuperada: {JsonSerializer.Serialize(record)}");

            TransactionGetResponseDto response = record.Adapt<TransactionGetResponseDto>();

            _logger.Info($"Transacción transformada a retornar: {JsonSerializer.Serialize(response)}");

            return response == null ? NoContent() : Ok(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(201, Type = typeof(TransactionGetResponseDto))]
        [ProducesResponseType(422, Type = typeof(BussinessExceptionResponseDto))]
        [Authorize(Policy = "BearerToken")]
        public async Task<ActionResult<TransactionGetResponseDto>> Create(TransactionPostRequestCreateDto model)
        {
            _logger.Info($"Recibiendo un pedido para crear la transacción {model}");

            Transaction record = model.Adapt<Transaction>();

            record = await transactionService.Create(record);

            _logger.Info($"Transacción creada: {JsonSerializer.Serialize(record)}");

            TransactionGetResponseDto? response = record.Adapt<TransactionGetResponseDto>();

            _logger.Info($"Transacción transformada a retornar: {JsonSerializer.Serialize(response)}");

            return Created($"/transaction/{response.TransactionId}", response);
        }
    }
}
