using System.Runtime.Serialization;

namespace InnerCore.Api.deConz.Models.Groups
{
    /// <summary>
    /// Possible group types
    /// </summary>
    public enum GroupType
    {
        [EnumMember(Value = "LightGroup")]
        LightGroup,
        [EnumMember(Value = "Room")]
        Room,
        [EnumMember(Value = "Luminaire")]
        Luminaire,
        [EnumMember(Value = "LightSource")]
        LightSource
    }
}