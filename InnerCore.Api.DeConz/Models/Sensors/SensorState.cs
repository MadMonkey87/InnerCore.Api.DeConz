using InnerCore.Api.DeConz.Interfaces;
using InnerCore.Api.DeConz.Models.Sensors.CLIP;
using InnerCore.Api.DeConz.Models.Sensors.Xiaomi;
using InnerCore.Api.DeConz.Models.Sensors.Zigbee;
using Newtonsoft.Json;

namespace InnerCore.Api.DeConz.Models.Sensors
{
    public class SensorState : ICommandBody,
        CLIPGenericFlagState,
        CLIPGenericStatusState,
        ZHAHumidityState,
        ZHALightLevelState,
        ZHAOpenCloseState,
        CLIPPresenceState,
        CLIPSwitchState,
        DaylightSensorState,
        ZGPSwitchState,
        ZHAPresenceState,
        ZLLSwitchState,
        ZHATemperatureState,
        ZHAPressureState,
        ZHAAlarmState,
        ZHACarbonMonoxideState,
        ZHAFireState,
        IASZoneState,
        GenericXiaomiState
    {
        [JsonProperty("buttonevent")]
        public int? ButtonEvent { get; set; }

        [JsonProperty("dark")]
        public bool? Dark { get; set; }

        [JsonProperty("daylight")]
        public bool? Daylight { get; set; }

        [JsonProperty("flag")]
        public bool? Flag { get; set; }

        [JsonProperty("humidity")]
        public int? Humidity { get; set; }

        [JsonProperty("lastupdated")]
        public string Lastupdated { get; set; }

        [JsonProperty("lux")]
        public long? Lux { get; set; }

        [JsonProperty("lightlevel")]
        public long? LightLevel { get; set; }

        [JsonProperty("open")]
        public bool? Open { get; set; }

        [JsonProperty("presence")]
        public bool? Presence { get; set; }

        [JsonProperty("status")]
        public int? Status { get; set; }

        [JsonProperty("temperature")]
        public int? Temperature { get; set; }

        [JsonProperty("pressure")]
        public int? Pressure { get; set; }

        [JsonProperty("alarm")]
        public bool? Alarm { get; set; }

        [JsonProperty("carbonmonoxide")]
        public bool? CarbonMonoxide { get; set; }

        [JsonProperty("fire")]
        public bool? Fire { get; set; }

        [JsonProperty("lowbattery")]
        public bool? LowBattery { get; set; }

        [JsonProperty("tampered")]
        public bool? Tampered { get; set; }
    }
}