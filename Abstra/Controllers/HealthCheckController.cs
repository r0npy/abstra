using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Abstra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [HttpGet("healthcheck")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public IActionResult Get()
        {
            _logger.Info("I'm alive");
            return Ok("I'm alive");
        }
    }
}
