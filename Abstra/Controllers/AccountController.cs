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
    public class AccountController(IAccountService accountService, IConfiguration config) : ControllerBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(AcccountGetResponseDto))]
        [ProducesResponseType(204)]
        [ProducesResponseType(422, Type = typeof(BussinessExceptionResponseDto))]
        [Authorize(Policy = "BearerToken")]
        public async Task<ActionResult<AcccountGetResponseDto?>> Get(int id)
        {
            _logger.Info($"Recibiendo un pedido para recuperar la cuenta con id {id}");

            if (id <= 0)
            {
                string message = $"El id {id} no puede ser menor a 1";
                _logger.Error(message);
                throw new BussinessException(message);
            }

            Account? record = await accountService.Get(id);

            _logger.Info($"Cuenta recuperada: {JsonSerializer.Serialize(record)}");

            AcccountGetResponseDto response = record.Adapt<AcccountGetResponseDto>();

            _logger.Info($"Listado de clientes transformados a retornar: {JsonSerializer.Serialize(response)}");

            return response == null ? NoContent() : Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AcccountGetResponseDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(422, Type = typeof(BussinessExceptionResponseDto))]
        [Authorize(Policy = "BearerToken")]
        public async Task<ActionResult<AcccountGetResponseDto>> Get()
        {
            _logger.Info($"Recibiendo un pedido para recuperar todas las cuentas");

            IEnumerable<Account>? record = await accountService.Get();

            _logger.Info($"Listado de cuentas recibidos: {JsonSerializer.Serialize(record)}");

            IEnumerable<AcccountGetResponseDto>? response = record.Adapt<IEnumerable<AcccountGetResponseDto>>();

            _logger.Info($"Listado cuentas transformados a retornar: {JsonSerializer.Serialize(response)}");

            return response == null ? NoContent() : Ok(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(201, Type = typeof(IEnumerable<AcccountGetResponseDto>))]
        [ProducesResponseType(422, Type = typeof(BussinessExceptionResponseDto))]
        [Authorize(Policy = "BearerToken")]
        public async Task<ActionResult<AcccountGetResponseDto>> Create(AccountPostRequestCreateDto model)
        {
            _logger.Info($"Recibiendo un pedido para crear la cuenta {model}");

            Account record = model.Adapt<Account>();

            record = await accountService.Create(record);

            _logger.Info($"Listado de cuenntas recibidas: {JsonSerializer.Serialize(record)}");

            AcccountGetResponseDto? response = record.Adapt<AcccountGetResponseDto>();

            _logger.Info($"Listado cuentas transformados a retornar: {JsonSerializer.Serialize(response)}");

            return Created($"/account/{response.AccountId}", response);
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(422, Type = typeof(BussinessExceptionResponseDto))]
        [Authorize(Policy = "BearerToken")]
        public async Task<ActionResult> Update(AccountPutRequestUpdateDto model)
        {
            _logger.Info($"Recibiendo un pedido para actualizar la cuenta {JsonSerializer.Serialize(model)}");

            Account record = model.Adapt<Account>();

            _ = await accountService.Update(record);

            _logger.Info($"Se ha actualizado correctamente la cuenta: {JsonSerializer.Serialize(record)}");

            return NoContent();
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(422, Type = typeof(BussinessExceptionResponseDto))]
        [Authorize(Policy = "BearerToken")]
        public async Task<ActionResult> Remove(int id)
        {
            _logger.Info($"Recibiendo un pedido para eliminar la cuenta {id}");

            _ = await accountService.Remove(id);

            _logger.Info($"Se ha eliminado correctamente la cuenta: {id}");

            return NoContent();
        }
    }
}
