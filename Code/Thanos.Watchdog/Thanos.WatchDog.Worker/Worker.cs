using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thanos.Models;
using Thanos.WatchDog.Events;
using Task = Thanos.Models.Task;

namespace Thanos.WatchDog
{
    public class Worker
    {
        private Worker()
        {
            Tasks = new List<Task>();
        }

        private static Worker onlyObject;

        public static Worker Instance
        {
            get
            {
                if (onlyObject == null)
                {
                    onlyObject = new Worker();
                }
                return onlyObject;
            }
        }

        public void Run(int frequency)
        {
            Parallel.ForEach(Tasks, (currentTask) =>
            {
                if (frequency % currentTask.Frequency == 0)
                {
                    ExecuteTask(currentTask);
                }
            });
        }


        private void ExecuteTask(Task t)
        {
            if (t.Type == TASK_TYPE.HEART_BEAT)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(DateTime.Now.ToString() + " Sending HeartBeat");
                Pulse pulse = new Pulse();
                pulse.SendHeartbeat();
            }
            else if (t.Type == TASK_TYPE.HUE_GET)
            {
                var url = t.Properties[Statics.URL_STRING];
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(DateTime.Now.ToString() + " Processing Hue event, " + t.Type.ToString() + ", " + t.ID);
                HueResponse hr = new HueResponse();
                hr.ProcessHueRequest(t);
            }
            else if (t.Type == TASK_TYPE.HUE_FLOW)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(DateTime.Now.ToString() + " Processing Hue Flow, " + t.Type.ToString() + ", " + t.ID);
                HueResponse hr = new HueResponse();
                hr.ProcessHueFlow(t);
            }
        }

        public List<Task> Tasks { get; set; }
    }
}
