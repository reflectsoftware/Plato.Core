using System.Collections.Generic;

namespace Plato.Messaging.RMQ.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public class RMQConfigSettings
    {
        /// <summary>
        /// Gets or sets the connections.
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        public List<RMQConnectionSettings> Connections { get; set; }

        /// <summary>
        /// Gets or sets the exchanges.
        /// </summary>
        /// <value>
        /// The exchanges.
        /// </value>
        public List<RMQExchangeSettings> Exchanges { get; set; }

        /// <summary>
        /// Gets or sets the queues.
        /// </summary>
        /// <value>
        /// The queues.
        /// </value>
        public List<RMQQueueSettings> Queues { get; set; }

        public RMQConfigSettings()
        {
            Connections = new List<RMQConnectionSettings>();
            Exchanges = new List<RMQExchangeSettings>();
            Queues = new List<RMQQueueSettings>();
        }
    }
}
