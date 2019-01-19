﻿using Newtonsoft.Json;
using InnerCore.Api.DeConz.Models.Sensors.CLIP;
using InnerCore.Api.DeConz.Models.Sensors.Zigbee;

namespace InnerCore.Api.DeConz.Models.Sensors
{
    public class Sensor :
        CLIPGenericFlag,
        CLIPGenericStatus,
        CLIPHumidity,
        CLIPLightlevel,
        CLIPOpenClose,
        CLIPPresence,
        CLIPSwitch,
        CLIPTemperature,
        DaylightSensor,
        IZGPSwitch,
        ZHAPresence,
        ZLLSwitch,
        ZLLTemperature,
        ZLLPressure,
        ZHAAlarm,
        ZHACarbonMonoxide,
        ZHAFire
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
        public string Type { get; set; }

        [JsonProperty("uniqueid")]
        public string UniqueId { get; set; }

        #region DeConz specific

        [JsonProperty("ep")]
        public string EndPoint { get; set; }

        #endregion
    }
}
