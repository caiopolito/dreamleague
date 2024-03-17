using dreamleague.common.Entities.HealthCheck;
using dreamleague.domain.Services.HealthCheck;
using Microsoft.AspNetCore.Mvc;

namespace dreamleague.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IGetHealthCheckService getHealthCheckService;
        public HealthCheckController(
                IGetHealthCheckService getHealthCheckService
            )
        {
            this.getHealthCheckService = getHealthCheckService;
        }

        /// GET /api/healthcheck
        /// <summary>
        /// Checks if the API has a healthy upstream.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(HealthCheckStatus))]
        [ProducesResponseType(503, Type = typeof(HealthCheckStatus))]
        public async Task<OkObjectResult> GetApiHealthCheckStatus()
        {
            return Ok(await getHealthCheckService.Execute());
        }

        /// GET /api/healthcheck/ping
        /// <summary>
        /// Checks if the API has a healthy upstream and its delay to respond.
        /// </summary>
        [HttpGet("ping")]
        [ProducesResponseType(200)]
        public IActionResult GetApiHealthCheckPing()
        {
            return Ok();
        }

    }
}
