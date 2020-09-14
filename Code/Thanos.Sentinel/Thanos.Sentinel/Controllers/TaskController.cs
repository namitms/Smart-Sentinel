using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Thanos.Sentinel.Filters;

namespace Thanos.Sentinel.Controllers
{
    /// <summary>
    /// Task controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    ///Used for Authentication 
    [ServiceFilter(typeof(APIKeyAuthAttribute))]
    public class TaskController : ControllerBase
    {
        /// <summary>
        /// Logger object
        /// </summary>
        private readonly ILogger _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            ///Initialize logger
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("A Get call is made");

            return "It will roll now";

        }
    }
}
