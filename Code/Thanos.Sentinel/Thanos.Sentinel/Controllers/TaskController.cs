using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thanos.Sentinel.Filters;

namespace Thanos.Sentinel.Controllers
{
    /// <summary>
    /// Task controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    ///Used for Authentication 
    [APIKeyAuth]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "It will roll now";
        }
    }
}
