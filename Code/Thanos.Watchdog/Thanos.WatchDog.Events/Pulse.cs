using Newtonsoft.Json;
using System;
using Thanos.Adapter.ServiceBus;
using Thanos.Models;

namespace Thanos.WatchDog.Events
{
    public class Pulse
    {
        public HeartBeat hb = null;

        public Pulse()
        {
            hb = new HeartBeat();
            hb.New_Born = false;
            hb.Status = HEALTH_STATUS.HEALTH;
            hb.Description = "All is well";
        }

        /// <summary>
        /// Heartbeat to Brain
        /// </summary>
        public void SendHeartbeat(bool newborn = false)
        {
            hb.When = DateTime.Now;
            hb.New_Born = newborn;
            string hbString = JsonConvert.SerializeObject(hb);
            Dispatcher.SendMessagesToBrainAsync(hbString, MESSAGE_TYPE.HEART_BEAT).Wait();
        }

    }
}
