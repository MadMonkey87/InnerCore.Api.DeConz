using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.WebSocket
{
    /// <summary>
    /// Type of resource to which the message belongs
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// the id field refers to a sensor resource
        /// </summary>
        [EnumMember(Value = "sensors")]
        Sensor,

        /// <summary>
        /// the id field refers to a light resource
        /// </summary>
        [EnumMember(Value = "lights")]
        Light,
    }
}