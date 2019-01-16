using System.Runtime.Serialization;
using InnerCore.Api.DeConz.Models.Sensors;
using Newtonsoft.Json;

namespace InnerCore.Api.DeConz.Models.Groups
{
    public class GroupLightLevel
    {
        [DataMember(Name = "State")]
        public SensorState State { get; set; }

        [JsonProperty("lastupdated")]
        public string Lastupdated { get; set; }

        [JsonProperty("dark")]
        public bool Dark { get; set; }

        [JsonProperty("dark_all")]
        public bool DarkAll { get; set; }

        [JsonProperty("daylight")]
        public bool Daylight { get; set; }

        [JsonProperty("daylight_any")]
        public bool DaylightAny { get; set; }

        [JsonProperty("lightlevel")]
        public int LightLevel { get; set; }

        [JsonProperty("lightlevel_min")]
        public int LightLevelMin { get; set; }

        [JsonProperty("lightlevel_max")]
        public int LightLevelMax { get; set; }
    }
}