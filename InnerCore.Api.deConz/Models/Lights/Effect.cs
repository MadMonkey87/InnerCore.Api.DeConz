using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Lights
{
    /// <summary>
    /// Possible light effects
    /// </summary>
    public enum Effect
    {
        /// <summary>
        /// Stop current effect
        /// </summary>
        [EnumMember(Value = "none")]
        None,

        /// <summary>
        /// Color loop
        /// </summary>
        [EnumMember(Value = "colorloop")]
        ColorLoop
    }
}