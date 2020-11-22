using Newtonsoft.Json;
using System;
using System.Linq;
using Thanos.Adapter.ServiceBus;
using Thanos.Models;
using Flow = Thanos.Models.Flow;

namespace Thanos.WatchDog.Actions
{
    public class TaskRequest
    {
        public TaskRequest()
        {
            Receiver.eventPub.TaskStartRequestReceived += EventPub_TaskStartRequestReceived;
            Receiver.eventPub.TaskEndRequestReceived += EventPub_TaskEndRequestReceived;
        }

        private void EventPub_TaskEndRequestReceived(object sender, Task e)
        {
            var delTask = Worker.Instance.Tasks.Where(t => t.ID == e.ID.ToUpper().Trim()).FirstOrDefault();
            if (delTask != null)
            {
                Worker.Instance.Tasks.Remove(delTask);
                Console.WriteLine("*******   Removing Task   *******");
                Console.WriteLine("Task ID : " + delTask.ID);
                Console.WriteLine("Task Type : " + delTask.Type.ToString());
            }
        }

        private void EventPub_TaskStartRequestReceived(object sender, Models.Task e)
        {
            if (e.Type == TASK_TYPE.HUE_FLOW)
            {
                Flow flow = JsonConvert.DeserializeObject<Flow>(e.Properties[Statics.FLOW_STRING].ToString());
                e.Properties[Statics.FLOW_STRING] = flow;
            }
            Worker.Instance.Tasks.Add(e);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("*******   Added Task   *******");
            Console.WriteLine("Task ID : " + e.ID);
            Console.WriteLine("Task Type : " + e.Type.ToString());
        }
    }
}
