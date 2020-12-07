namespace Thanos.Models
{

    public enum MESSAGE_TYPE { HEART_BEAT, EDGE_CONFIGUATION, TASK_START, TASK_END, TASK_RESPONSE };

    public enum HEALTH_STATUS { HEALTH, UNHEALTHY, DONT_KNOW }

    public enum EDGE_RESPONSE_TRIGGER { LIGHT_ON }

    public enum EDGE_RESPONSE_ACTION { ALARM }

    public enum ASSET_TYPES { BULB, SENSOR }

    public static class Statics
    {
        public static string HEARBEAT = "HEARTBEAT";
        public static string URL_STRING = "URL";
        public static string PAYLOAD_STRING = "PAYLOAD";
        public static string COLOR_STRING = "COLOR";
        public static string FLOW_STRING = "FLOW";
        public static string HUE_HUB_STRING = "HUE_HUB";
        public static string TASK_RESPONSE_STRING = "TASK_REPONSE";
        public static string HUE_HUB_GROUND = "GROUND";
        public static string HUE_HUB_FIRST = "FIRST";
        public static string ALERT_MODE = "ALERT";
        public static string REPONSE_URL_STRING = "REPONSE_URL";
        public static string EDGE_RESPONSE_TRIGGER_STRING = "EDGE_RESPONSE_TRIGGER";
        public static string EDGE_RESPONSE_ACTION_STRING = "EDGE_RESPONSE_ACTION";
        public static string RESPONSE_ACTION_STARTIME_STRING = "RESPONSE_ACTION_STARTTIME";
        public static string RESPONSE_ACTION_DURATION_STRING = "RESPONSE_ACTION_DURATION";
        public static string TASK_DESCRIPTION_STRING = "TASK_DESCRIPTION";
    }
}
