using System.Collections.Generic;
using System.Runtime.Serialization;

namespace InnerCore.Api.deConz.Models.Groups
{
    [DataContract]
    internal class UpdateGroupRequest : CreateGroupRequest
    {
        /// <summary>
        /// The IDs of the lights that are in the group.
        /// </summary>
        [DataMember(Name = "lights")]
        public IEnumerable<string> Lights { get; set; }

        /// <summary>
        /// Indicates the hidden status of the group. Has no effect at the gateway but apps can uses this to hide groups.
        /// </summary>
        [DataMember(Name = "hidden")]
        public bool? Hidden { get; set; }

        /// <summary>
        /// A list of light ids of this group that can be sorted by the user. Need not to contain all light ids of this group.
        /// </summary>
        [DataMember(Name = "lightsequence")]
        public IEnumerable<string> LightSequence { get; set; }

        /// <summary>
        /// A list of light ids of this group that are subsequent ids from multidvices with multiple endpoints like the FLS-PP.
        /// </summary>
        [DataMember(Name = "mulitdeviceids")]
        public IEnumerable<string> MulitDeviceIds { get; set; }
    }
}
