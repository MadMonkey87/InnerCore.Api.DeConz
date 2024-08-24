using System;
using System.Runtime.Serialization;
using InnerCore.Api.DeConz.Converters;
using Newtonsoft.Json;

namespace InnerCore.Api.DeConz.Models.Groups
{
    public class GroupPresence
    {
        [DataMember(Name = "State")]
        public LightState State { get; set; }

        [JsonProperty("lastupdated")]
        [JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? Lastupdated { get; set; }

        [JsonProperty("presence")]
        public bool Presence { get; set; }

        [JsonProperty("presence_all")]
        public bool PresenceAll { get; set; }
    }
}