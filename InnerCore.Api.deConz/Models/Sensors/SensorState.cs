using InnerCore.Api.deConz.Interfaces;
using InnerCore.Api.deConz.Models.Sensors.CLIP;
using InnerCore.Api.deConz.Models.Sensors.ZigBee;
using Newtonsoft.Json;

namespace InnerCore.Api.deConz.Models.Sensors
{
    public class SensorState : ICommandBody,
        CLIPGenericFlagState,
        CLIPGenericStatusState,
        CLIPHumidityState,
        CLIPLightlevelState,
        CLIPOpenCloseState,
        CLIPPresenceState,
        CLIPSwitchState,
        CLIPTemperatureState,
        DaylightSensorState,
        ZGPSwitchState,
        ZLLPresenceState,
        ZLLSwitchState,
        ZLLTemperatureState
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
    }
}