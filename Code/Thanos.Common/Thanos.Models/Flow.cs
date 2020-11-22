using System.Collections.Generic;

namespace Thanos.Models
{
    public enum TANSITION_TYPE { INSTANT, MORPH }
    public class Flow
    {
        public int Current_Step_Index { get; set; }
        public int Current_Duration_Tick { get; set; }
        public bool Loop { get; set; }
        public TimeLine Timeline { get; set; }

        public Flow()
        {
            Current_Step_Index = 0;
            Current_Duration_Tick = 0;
            Loop = true;
        }
    }

    public class TimeLine
    {
        public List<Step> Steps { get; set; }
        public TimeLine()
        {
            Steps = new List<Step>();
        }
    }

    public class Step
    {
        public int Persistance_Duration { get; set; }
        public List<Asset> Assets { get; set; }
        public TANSITION_TYPE Transition { get; set; }
        public Step()
        {
            Persistance_Duration = 0;
            Assets = new List<Asset>();
        }
    }

    public class Asset
    {
        public ASSET_TYPES Asset_Type { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public Asset()
        {
            Properties = new Dictionary<string, object>();
        }
    }

}
