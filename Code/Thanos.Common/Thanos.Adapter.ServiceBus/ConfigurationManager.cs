using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Thanos.Models;

namespace Thanos.Adapter.ServiceBus
{
    public class QueueConfigurationManager
    {
        /// <summary>
        /// Brain Q client
        /// </summary>
        public IQueueClient BrainQueueClient { get; set; }

        /// <summary>
        /// Node Q client
        /// </summary>
        public IQueueClient NodeQueueClient { get; set; }

        /// <summary>
        /// Get initial config
        /// </summary>
        /// <returns></returns>
        private void SetInitialConfiguration(IConfiguration configuration)
        {
            Configuration initConfig = new Configuration(configuration);
            BrainQueueClient = new QueueClient(initConfig.QueueConnectionString, initConfig.BrainQueue);
            NodeQueueClient = new QueueClient(initConfig.QueueConnectionString, initConfig.NodeQueue);
        }

        /// <summary>
        /// The only constructor
        /// </summary>
        public QueueConfigurationManager(IConfiguration configuration)
        {
            SetInitialConfiguration(configuration);
        }

    }
}
