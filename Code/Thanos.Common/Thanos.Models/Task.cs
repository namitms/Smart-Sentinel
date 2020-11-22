using System.Collections.Generic;

namespace Thanos.Models
{
    public enum TASK_TYPE { HEART_BEAT, HUE_GET, HUE_PUT, HUE_POST, HUE_FLOW }
    public class Task
    {
        public string ID { get; set; }
        public int Frequency { get; set; }
        public TASK_TYPE Type { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public Task()
        {
            Properties = new Dictionary<string, object>();
        }
    }
}
