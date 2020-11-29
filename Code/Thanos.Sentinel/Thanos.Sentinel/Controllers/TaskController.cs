using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Thanos.Adapter.ServiceBus;
using Thanos.Models;

namespace Thanos.Sentinel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] Task task)
        {
            task.ID = task.ID.ToUpper().Trim();
            if (task.Type == TASK_TYPE.HUE_FLOW)
            {
                Flow flow = JsonConvert.DeserializeObject<Flow>(task.Properties[Statics.FLOW_STRING].ToString());
                task.Properties[Statics.FLOW_STRING] = flow;
            }
            Dispatcher.SendMessagesToNodeAsync(JsonConvert.SerializeObject(task), MESSAGE_TYPE.TASK_START).Wait();
        }

        [HttpDelete]
        public void Delete(string id)
        {
            Task deleteTask = new Task() { ID = id.ToUpper().Trim() };
            Dispatcher.SendMessagesToNodeAsync(JsonConvert.SerializeObject(deleteTask), MESSAGE_TYPE.TASK_END).Wait();
        }
    }
}