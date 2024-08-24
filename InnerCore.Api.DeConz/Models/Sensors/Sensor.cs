using InnerCore.Api.DeConz.Converters;
using InnerCore.Api.DeConz.Models.Sensors.CLIP;
using InnerCore.Api.DeConz.Models.Sensors.Xiaomi;
using InnerCore.Api.DeConz.Models.Sensors.Zigbee;
using Newtonsoft.Json;

namespace InnerCore.Api.DeConz.Models.Sensors
{
    public class Sensor
        : CLIPGenericFlag,
            CLIPGenericStatus,
            ZHAHumidity,
            ZHALightLevel,
            ZHAOpenClose,
            CLIPPresence,
            CLIPSwitch,
            DaylightSensor,
            ZHAPresence,
            ZHASwitch,
            ZHATemperature,
            ZHAThermostat,
            ZHAPressure,
            ZHAAlarm,
            ZHACarbonMonoxide,
            ZHAFire,
            IASZone,
            GenericXiaomi,
            ZHAWater,
            ZHAVibration,
            ZHAConsumption,
            ZHAPower,
            ZGPSwitch
    {
        [JsonProperty("state")]
        public SensorState State { get; set; }

        [JsonProperty("config")]
        public SensorConfig Config { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("manufacturername")]
        public string ManufacturerName { get; set; }

        [JsonProperty("modelid")]
        public string ModelId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("swversion")]
        public string SwVersion { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringNullableEnumConverter))]
        public SensorType? Type { get; set; }

        [JsonProperty("uniqueid")]
        public string UniqueId { get; set; }

        #region DeConz specific

        [JsonProperty("ep")]
        public string EndPoint { get; set; }

        #endregion
    }
}
