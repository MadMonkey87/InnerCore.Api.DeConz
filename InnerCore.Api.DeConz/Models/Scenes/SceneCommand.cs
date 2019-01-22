using InnerCore.Api.DeConz.Interfaces;
using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Scenes
{
    /// <summary>
    /// Send a SceneID as command
    /// </summary>
    [DataContract]
    public class SceneCommand : ICommandBody
    {
        public SceneCommand()
        {

        }

        public SceneCommand(string sceneId)
        {
            this.Scene = sceneId;
        }

        /// <summary>
        /// Scene ID to activate
        /// </summary>
        [DataMember(Name = "scene")]
        public string Scene { get; set; }

        [DataMember(Name = "storelightstate")]
        public bool? StoreLightState { get; set; }
    }
}
