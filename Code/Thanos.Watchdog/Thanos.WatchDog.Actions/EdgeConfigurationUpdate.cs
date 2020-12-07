using System;
using Thanos.Adapter.ServiceBus;
using Thanos.Models;

namespace Thanos.WatchDog.Actions
{
    public class EdgeConfigurationUpdate
    {
        public EdgeConfigurationUpdate()
        {
            Receiver.eventPub.EdgeConfigurationReceived += EventPub_EdgeConfigurationReceived;
        }

        private void EventPub_EdgeConfigurationReceived(object sender, Models.EdgeConfiguration e)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("*******   Setting Configuration   *******");
            State.State.Instance.EdgeConfiguration = e;
            Task hearBeat = new Task();
            hearBeat.ID = Statics.HEARBEAT;
            hearBeat.Frequency = 5;
            hearBeat.Type = TASK_TYPE.HEART_BEAT;
            Worker.Instance.Tasks.Add(hearBeat);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("*******   Starting Heartbeat   *******");
        }
    }
}
