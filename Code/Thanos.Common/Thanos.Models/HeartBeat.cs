using System;

namespace Thanos.Models
{
    public class HeartBeat
    {
        /// <summary>
        /// Date time when the heartbeat was reported
        /// </summary>
        public DateTime When { get; set; }

        /// <summary>
        /// Is first heartbeat?
        /// </summary>
        public bool New_Born { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public HEALTH_STATUS Status { get; set; }

        /// <summary>
        /// Additional information
        /// </summary>
        public string Description { get; set; }
    }
}
