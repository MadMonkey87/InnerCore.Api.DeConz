using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Touchlink
{
    [DataContract]
    public enum ScanState
    {
        /// <summary>
        /// Scan is finished or was not started
        /// </summary>
        [EnumMember(Value = "idle")]
        Idle,

        /// <summary>
        /// Scan is in progress
        /// </summary>
        [EnumMember(Value = "scanning")]
        Scanning
    }
}
