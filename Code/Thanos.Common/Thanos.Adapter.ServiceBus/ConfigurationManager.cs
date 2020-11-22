using Microsoft.Azure.ServiceBus;
using Thanos.Models;

namespace Thanos.Adapter.ServiceBus
{
    public class ConfigurationManager
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
        private void SetInitialConfiguration()
        {
            Configuration initConfig = new Configuration();
            BrainQueueClient = new QueueClient(initConfig.QueueConnectionString, initConfig.BrainQueue);
            NodeQueueClient = new QueueClient(initConfig.QueueConnectionString, initConfig.NodeQueue);
        }

        /// <summary>
        /// The only constructor
        /// </summary>
        public ConfigurationManager()
        {
            SetInitialConfiguration();
        }

    }
}
