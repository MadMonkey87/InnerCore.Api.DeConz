using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Bridge
{
    public enum UpdateChannel
    {
        [EnumMember(Value = "stable")]
        Stable,

        [EnumMember(Value = "alpha")]
        Alpha,

        [EnumMember(Value = "beta")]
        Beta,
    }
}
