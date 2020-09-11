using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        /// Action implementation called before the method entry
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ///Querry the request header for the APIKey
            if(context.HttpContext.Request.Headers.TryGetValue(ConstantStrings.APIKEY, out var potentialAPIKey))
            {
                ///Retrieve configuration object
                var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

                ///Get APIKey for appsettings
                var apiKey = configuration.GetValue<string>(ConstantStrings.APIKEY);

                ///Match keys
                if (apiKey!=null && apiKey.Trim().Equals(potentialAPIKey.ToString().Trim()))
                {
                    ///Proceed to method implementation
                    await next();
                    return;
                }
            }

            ///Return unauthorized exception if the key is empty or does not match
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
