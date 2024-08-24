using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.WebSocket
{
    /// <summary>
    /// Specifies the message type
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// the message holds an event
        /// </summary>
        [EnumMember(Value = "event")]
        Event,
    }
}
