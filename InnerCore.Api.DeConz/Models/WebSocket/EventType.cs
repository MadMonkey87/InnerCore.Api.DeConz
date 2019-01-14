using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.WebSocket
{
    /// <summary>
    /// Specifies the type of an event message
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// the message holds an event
        /// </summary>
        [EnumMember(Value = "changed")]
        Changed,
    }
}