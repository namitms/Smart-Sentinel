using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Thanos.Models;

namespace Thanos.Sentinel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartBeatController : ControllerBase
    {
        private readonly ILogger<HeartBeatController> _logger;
        public HeartBeatController(ILogger<HeartBeatController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<HeartBeat> Get()
        {
            return State.State.Instance.Log;
        }
    }
}