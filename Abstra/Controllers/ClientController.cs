using Abstra.Core.Domains;
using Abstra.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Abstra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IClientService clientService) : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<Client?>> Get(int id)
        {
            var result = await clientService.Get(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>?>> Get()
        {
            var result = await clientService.Get();
            return Ok(result);
        }
    }
}
