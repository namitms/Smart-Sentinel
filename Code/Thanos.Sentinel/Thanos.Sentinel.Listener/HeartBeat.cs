using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Thanos.Sentinel.Listener
{
    /// <summary>
    /// Heartbeat event handler
    /// </summary>
    public static class HeartBeat
    {
        /// <summary>
        /// Listens to the brain queue (Azure Service Bus) for messages from the client
        /// </summary>
        /// <param name="myQueueItem">Queue message</param>
        /// <param name="log">ILogger instance</param>
        [FunctionName("HeartBeat")]
        public static void Run([ServiceBusTrigger("brainqueue", Connection = "QueueConnectionString")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
