using System.Runtime.Serialization;

namespace InnerCore.Api.deConz.Models.Bridge
{
    /// <summary>
    /// Possible InternetServices States
    /// </summary>
    public enum InternetServicesState
    {
        [EnumMember(Value = "connected")]
        Connected,
        [EnumMember(Value = "disconnected")]
        Disconnected,
    }
}