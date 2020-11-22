using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace Thanos.Scheduer.Poll.Sensors
{
    public static class Sensors
    {
        [FunctionName("Sensors")]
        public static void Run([TimerTrigger("5 * * ? * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
