using InnerCore.Api.deConz.Interfaces;
using InnerCore.Api.deConz.Models.Lights;
using InnerCore.Api.deConz.Models.Scenes;
using InnerCore.Api.deConz.Models.Sensors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InnerCore.Api.deConz.Models.Schedule
{
    /// <summary>
    /// Can be used to create any type of schedule command
    /// Use a raw jsonString as input
    /// 
    /// dynamic dynamicCOmmand = new ExpandoObject();
    /// dynamicCOmmand.status = 1;
    /// var jsonString = JsonConvert.SerializeObject(dynamicCOmmand);
    /// var commandBody = new GenericScheduleCommand(jsonString);
    /// </summary>
    public class GenericScheduleCommand : ICommandBody
    {
        public string JsonString { get; set; }

        public GenericScheduleCommand(string jsonString)
        {
            JsonString = jsonString;
        }

        public bool IsSceneCommand()
        {
            JObject jObject = JObject.Parse(this.JsonString);

            return jObject["scene"] != null || jObject["Scene"] != null;
        }

        public SceneCommand AsSceneCommand()
        {
            return JsonConvert.DeserializeObject<SceneCommand>(this.JsonString);
        }

        public LightCommand AsLightCommand()
        {
            return JsonConvert.DeserializeObject<LightCommand>(this.JsonString);
        }

        public SensorState AsSensorCommand()
        {
            return JsonConvert.DeserializeObject<SensorState>(this.JsonString);
        }
    }
}
