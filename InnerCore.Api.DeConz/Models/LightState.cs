using InnerCore.Api.DeConz.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;
using InnerCore.Api.DeConz.Models.Lights;

namespace InnerCore.Api.DeConz.Models
{
    [DataContract]
    public class LightState
    {
        [DataMember(Name = "on")]
        public bool On { get; set; }

        [DataMember(Name = "bri")]
        public byte Brightness { get; set; }

        [DataMember(Name = "hue")]
        public int? Hue { get; set; }

        [DataMember(Name = "sat")]
        public int? Saturation { get; set; }

        [DataMember(Name = "xy")]
        public double[] ColorCoordinates { get; set; }

        [DataMember(Name = "ct")]
        public int? ColorTemperature { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "alert")]
        public Alert Alert { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "effect")]
        public Effect? Effect { get; set; }

        [DataMember(Name = "colormode")]
        public string ColorMode { get; set; }

        [DataMember(Name = "reachable")]
        public bool? IsReachable { get; set; }

        [DataMember(Name = "transitiontime")]
        [JsonConverter(typeof(TransitionTimeConverter))]
        public TimeSpan? TransitionTime { get; set; }

        [DataMember(Name = "mode")]
        public string Mode { get; set; }

        #region DeConz specific

        /// <summary>
        /// Specifies the speed of a colorloop. 1 = very fast, 255 = very slow (default: 15). This parameter only has an effect when it is called together with effect colorloop.
        /// </summary>
        [DataMember(Name = "colorloopspeed")]
        public int ColorLoopSpeed { get; set; }

        #endregion
    }
}
