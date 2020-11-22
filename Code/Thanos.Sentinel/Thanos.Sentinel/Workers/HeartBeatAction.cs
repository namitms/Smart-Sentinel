using Newtonsoft.Json;
using Thanos.Adapter.ServiceBus;
using Thanos.Models;

namespace Thanos.Sentinel.Workers
{
    public class HeartBeatAction
    {
        public HeartBeatAction()
        {
            Receiver.eventPub.HearBeatReceived += EventPub_HearBeatReceived;
        }

        private void EventPub_HearBeatReceived(object sender, Models.HeartBeat e)
        {
            if (e.New_Born == true)
            {
                EdgeConfiguration eConfig = new EdgeConfiguration();
                eConfig.HueGroundBaseURL = State.State.Instance.HueGroundBaseURL;
                eConfig.HueTopBaseURL = State.State.Instance.HueFirstBaseURL;
                Dispatcher.SendMessagesToNodeAsync(JsonConvert.SerializeObject(eConfig), MESSAGE_TYPE.EDGE_CONFIGUATION).Wait();
            }
            State.State.Instance.AddLog(e);
        }
    }
}
