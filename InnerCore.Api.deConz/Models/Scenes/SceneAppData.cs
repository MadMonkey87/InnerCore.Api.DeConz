using System.Runtime.Serialization;

namespace InnerCore.Api.deConz.Models.Scenes
{
    [DataContract]
    public class SceneAppData
    {
        [DataMember(Name = "version")]
        public int? Version { get; set; }
        [DataMember(Name = "data")]
        public string Data { get; set; }
    }
}