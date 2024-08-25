﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using InnerCore.Api.DeConz.Models.Sensors.CLIP;
using InnerCore.Api.DeConz.Models.Sensors.Xiaomi;
using InnerCore.Api.DeConz.Models.Sensors.Zigbee;
using Newtonsoft.Json;

namespace InnerCore.Api.DeConz.Models.Sensors
{
    [DataContract]
    public class SensorConfig
        : CLIPGenericFlagConfig,
            CLIPGenericStatusConfig,
            ZHAHumidityConfig,
            ZHALightLevelConfig,
            ZHAOpenCloseConfig,
            CLIPPresenceConfig,
            CLIPSwitchConfig,
            DaylightSensorConfig,
            ZHAPresenceConfig,
            ZHASwitchConfig,
            ZHATemperatureConfig,
            ZHAThermostatConfig,
            ZHAPressureConfig,
            ZHAAlarmConfig,
            ZHACarbonMonoxideConfig,
            ZHAFireConfig,
            IASZoneConfig,
            GenericXiaomiConfig,
            ZHAWaterConfig,
            ZHAVibrationConfig,
            ZHAConsumptionConfig,
            ZHAPowerConfig,
            ZGPSwitchConfig
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

        [JsonProperty("temperature")]
        public int? Temperature { get; set; }

        [JsonProperty("heatsetpoint")]
        public int? HeatSetPoint { get; set; }
    }
}
