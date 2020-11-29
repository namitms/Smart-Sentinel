using Microsoft.Extensions.Configuration;

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
        public Configuration(IConfiguration configuration)
        {
            QueueConnectionString = configuration["QueueConnectionString"];
            BrainQueue = "brainqueue";
            NodeQueue = "nodequeue";
        }

    }
}
