using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thanos.Sentinel.Helpers;

namespace Thanos.Sentinel.Filters
{
    /// <summary>
    /// Attribute implementation to authenticate REST API calls using APIKey configured in appsettings file
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class APIKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        /// <summary>
        /// Logger object
        /// </summary>
        private readonly ILogger _logger;

        public APIKeyAuthAttribute(ILogger<APIKeyAuthAttribute> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Action implementation called before the method entry
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                ///Querry the request header for the APIKey
                if (context.HttpContext.Request.Headers.TryGetValue(ConstantStrings.APIKEY, out var potentialAPIKey))
                {
                    ///Retrieve configuration object
                    var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

                    ///Get APIKey for appsettings
                    var apiKey = configuration.GetValue<string>(ConstantStrings.APIKEY);

                    ///Match keys
                    if (apiKey != null && apiKey.Trim().Equals(potentialAPIKey.ToString().Trim()))
                    {
                        ///Proceed to method implementation
                        await next();
                        return;
                    }
                }

                ///Return unauthorized exception if the key is empty or does not match
                context.Result = new UnauthorizedResult();
                _logger.LogWarning("Unauthorized access");
                return;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error authenticating");
                throw;
            }
        }
    }
}
