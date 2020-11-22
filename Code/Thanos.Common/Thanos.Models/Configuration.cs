namespace Thanos.Models
{
    /// <summary>
    /// Initial configuration
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Queue connection string
        /// </summary>
        public string QueueConnectionString { get; set; }
        /// <summary>
        /// Queue from brain to node
        /// </summary>
        public string BrainQueue { get; set; }
        /// <summary>
        /// Queue from node to brain
        /// </summary>
        public string NodeQueue { get; set; }

        /// <summary>
        /// The only constructor
        /// </summary>
        public Configuration()
        {
            QueueConnectionString = "Endpoint=sb://sentinel-celestial.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=sS1qO36t929mEYimV60qN3WBAiyfTPHBwLfN8U/29z8=";
            BrainQueue = "brainqueue";
            NodeQueue = "nodequeue";
        }

    }
}
