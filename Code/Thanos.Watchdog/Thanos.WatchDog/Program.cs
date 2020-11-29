using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;
using Thanos.Adapter.ServiceBus;
using Thanos.WatchDog.Actions;
using Thanos.WatchDog.Events;

namespace Thanos.WatchDog
{
    class Program
    {
        static void Main(string[] args)
        {
            EdgeConfigurationUpdate ecUpdate = new EdgeConfigurationUpdate();
            string sValue = ConfigurationManager.AppSettings["QueueConnectionString"];
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>())
                                              .Build();

            Configuration["QueueConnectionString"] = sValue;
            Receiver.Configuration = Configuration;
            Dispatcher.Configuration = Configuration;
            Receiver.RegisterOnMessageHandlerAndReceiveMessages(false);
            Pulse wakepuPulse = new Pulse();
            TaskRequest taskMaster = new TaskRequest();
            wakepuPulse.SendHeartbeat(true);
            int counter = 0;

            while (true)
            {
                if (counter >= int.MaxValue)
                {
                    counter = 0;
                }

                System.Threading.Tasks.Task.Delay(1000).Wait();

                Worker.Instance.Run(counter);
                counter++;
            }
        }
    }
}
