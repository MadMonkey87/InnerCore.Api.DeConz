using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace InnerCore.Api.DeConz.Models.Groups
{
    public class GroupPresence
    {
        [DataMember(Name = "State")]
        public State State { get; set; }

        [JsonProperty("lastupdated")]
        public string Lastupdated { get; set; }

        [JsonProperty("presence")]
        public bool Presence { get; set; }

        [JsonProperty("presence_all")]
        public bool PresenceAll { get; set; }
    }
}