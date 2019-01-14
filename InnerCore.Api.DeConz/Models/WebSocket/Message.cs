using InnerCore.Api.DeConz.Models.Sensors;
using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.WebSocket
{
    [DataContract]
    public class Message
    {
        [DataMember(Name = "t")]
        public MessageType Type { get; set; }

        [DataMember(Name = "e")]
        public EventType? Event { get; set; }

        [DataMember(Name = "r")]
        public ResourceType ResourceType { get; set; }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public SensorState State { get; set; }

        [DataMember]
        public SensorConfig Config { get; set; }
    }
}
