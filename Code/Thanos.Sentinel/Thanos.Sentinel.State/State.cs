using System.Collections.Generic;
using Thanos.Models;

namespace Thanos.Sentinel.State
{
    public class State
    {
        private static State onlyObject;

        private static int Max_Logs = 10;

        public string HueGroundBaseURL { get; set; }

        public string HueFirstBaseURL { get; set; }

        private State()
        {
            Log = new List<HeartBeat>();
        }

        public static State Instance
        {
            get
            {
                if (onlyObject == null)
                {
                    onlyObject = new State();
                }
                return onlyObject;
            }
        }

        public List<HeartBeat> Log { get; set; }

        public void AddLog(HeartBeat log)
        {
            if (Log.Count >= Max_Logs)
            {
                Log.RemoveAt(0);
            }
            Log.Add(log);
        }

    }
}
