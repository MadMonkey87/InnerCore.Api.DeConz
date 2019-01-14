using System.Net.Http;
using System.Runtime.Serialization;
using InnerCore.Api.deConz.Converters;
using InnerCore.Api.deConz.Interfaces;
using Newtonsoft.Json;

namespace InnerCore.Api.deConz.Models.Schedule
{
    [DataContract]
    public class InternalBridgeCommand
    {
        [DataMember(Name = "address")]
        public string Address { get; set; }

        [JsonConverter(typeof(HttpMethodConverter))]
        [DataMember(Name = "method")]
        public HttpMethod Method { get; set; }

        [JsonConverter(typeof(CommandBodyConverter))]
        [DataMember(Name = "body")]
        public ICommandBody Body { get; set; }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (Body != null)
            {
                if (Body is GenericScheduleCommand)
                {
                    var genericCommand = Body as GenericScheduleCommand;
                    var invariantAddress = Address.ToLowerInvariant();

                    //Check if it is a scene command
                    if (genericCommand.IsSceneCommand())
                    {
                        Body = genericCommand.AsSceneCommand();
                    }
                    //If it is going to a lights or groups URL, it's probably a LightCommand
                    else if (invariantAddress.Contains("/lights") || invariantAddress.Contains("/groups"))
                    {
                        Body = genericCommand.AsLightCommand();
                    }
                    //If it is going to a sensor url, it's probably a SensorCommand
                    else if (invariantAddress.Contains("/sensors"))
                    {
                        Body = genericCommand.AsSensorCommand();
                    }
                }
            }

        }
    }
}
