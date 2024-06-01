using Abstra.Core.Domains;
using Abstra.Core.Exceptions;
using Abstra.Core.Services;
using Abstra.Mappers.Requests;
using Abstra.Mappers.Responses;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Text.Json;

namespace Abstra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IClientService clientService) : ControllerBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(ClientGetResponseDto))]
        [ProducesResponseType(204)]
        [ProducesResponseType(422, Type = typeof(BussinessExceptionResponseDto))]
        public async Task<ActionResult<ClientGetResponseDto?>> Get(int id)
        {
            _logger.Info($"Recibiendo un pedido para recuperar el usuario con id {id}");

            if (id <= 0)
            {
                string message = $"El id {id} no puede ser menor a 1";
                _logger.Error(message);
                throw new BussinessException(message);
            }

            Client? record = await clientService.Get(id);

            _logger.Info($"Cliente recuperado: {JsonSerializer.Serialize(record)}");

            ClientGetResponseDto response = record.Adapt<ClientGetResponseDto>();

            _logger.Info($"Listado de clientes transformados a retornar: {JsonSerializer.Serialize(response)}");

            return response == null ? NoContent() : Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClientGetResponseDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(422, Type = typeof(BussinessExceptionResponseDto))]
        public async Task<ActionResult<IEnumerable<ClientGetResponseDto>?>> Get()
        {
            _logger.Info($"Recibiendo un pedido para recuperar todos los usuarios");

            IEnumerable<Client>? record = await clientService.Get();

            _logger.Info($"Listado de clientes recibidos: {JsonSerializer.Serialize(record)}");

            IEnumerable<ClientGetResponseDto>? response = record.Adapt<IEnumerable<ClientGetResponseDto>>();

            _logger.Info($"Listado de clientes transformados a retornar: {JsonSerializer.Serialize(response)}");

            return response == null ? NoContent() : Ok(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(201, Type = typeof(IEnumerable<ClientGetResponseDto>))]
        [ProducesResponseType(422, Type = typeof(BussinessExceptionResponseDto))]
        public async Task<ActionResult<IEnumerable<ClientGetResponseDto>?>> Create(ClientPostRequestCreateDto model)
        {
            _logger.Info($"Recibiendo un pedido para crear el cliente {JsonSerializer.Serialize(model)}");

            Client record = model.Adapt<Client>();

            record = await clientService.Create(record);

            _logger.Info($"Listado de clientes recibidos: {JsonSerializer.Serialize(record)}");

            ClientGetResponseDto? response = record.Adapt<ClientGetResponseDto>();

            _logger.Info($"Listado de clientes transformados a retornar: {JsonSerializer.Serialize(response)}");

            return Created($"/users/{response.ClientId}", response);
        }
    }
}
