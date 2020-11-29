using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using Thanos.Models;

namespace Thanos.Adapter.ServiceBus
{
    public class EventPublisher
    {
        public virtual void OnHearBeatReceived(HeartBeat e)
        {
            EventHandler<HeartBeat> handler = HearBeatReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual void OnEdgeConfigurationReceived(EdgeConfiguration e)
        {
            EventHandler<EdgeConfiguration> handler = EdgeConfigurationReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual void OnTaskStartRequestReceived(Thanos.Models.Task e)
        {
            EventHandler<Thanos.Models.Task> handler = TaskStartRequestReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual void OnTaskEndRequestReceived(Thanos.Models.Task e)
        {
            EventHandler<Thanos.Models.Task> handler = TaskEndRequestReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual void OnTaskResponseReceived(Thanos.Models.Task e)
        {
            EventHandler<Thanos.Models.Task> handler = TaskResponseReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<HeartBeat> HearBeatReceived;

        public event EventHandler<EdgeConfiguration> EdgeConfigurationReceived;

        public event EventHandler<Thanos.Models.Task> TaskStartRequestReceived;

        public event EventHandler<Thanos.Models.Task> TaskEndRequestReceived;

        public event EventHandler<Thanos.Models.Task> TaskResponseReceived;
    }
}
