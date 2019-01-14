using System.Runtime.Serialization;

namespace InnerCore.Api.deConz.Models.Bridge
{
    public enum TimeFormat
    {
        [EnumMember(Value = "12h")]
        Hours12,
        [EnumMember(Value = "24h")]
        Hours24,
    }
}
