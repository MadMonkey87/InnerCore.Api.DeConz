using System.Collections.Generic;
using InnerCore.Api.DeConz.Models.Sensors.CLIP;
using InnerCore.Api.DeConz.Models.Sensors.ZigBee;
using Newtonsoft.Json;

namespace InnerCore.Api.DeConz.Models.Sensors
{
    public class SensorConfig :
        CLIPGenericFlagConfig,
        CLIPGenericStatusConfig,
        CLIPHumidityConfig,
        CLIPLightlevelConfig,
        CLIPOpenCloseConfig,
        CLIPPresenceConfig,
        CLIPSwitchConfig,
        CLIPTemperatureConfig,
        DaylightSensorConfig,
        ZGPSwitchConfig,
        ZLLPresenceConfig,
        ZLLSwitchConfig,
        ZLLTemperatureConfig,
        ZLLPressureConfig
    {
        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("battery")]
        public int? Battery { get; set; }

        [JsonProperty("configured")]
        public bool? Configured { get; }

        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("long")]
        public string Long { get; set; }

        [JsonProperty("ledindication")]
        public bool? LedIndication { get; set; }

        [JsonProperty("on")]
        public bool? On { get; set; }

        [JsonProperty("pending")]
        public List<string> Pending { get; set; }

        [JsonProperty("reachable")]
        public bool? Reachable { get; set; }

        [JsonProperty("sunriseoffset")]
        public int? SunriseOffset { get; set; }

        [JsonProperty("sunsetoffset")]
        public int? SunsetOffset { get; set; }

        [JsonProperty("tholddark")]
        public long? TholdDark { get; set; }

        [JsonProperty("tholdoffset")]
        public long? TholdOffset { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("usertest")]
        public bool? Usertest { get; set; }

        [JsonProperty("sensitivity")]
        public int? Sensitivity { get; set; }

        [JsonProperty("sensitivitymax")]
        public int? SensitivityMax { get; set; }

        [JsonProperty("group")]
        public int? Group { get; set; }
    }
}