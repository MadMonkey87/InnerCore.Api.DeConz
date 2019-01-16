using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Groups
{
    [DataContract]
    public class GroupState
    {
        [DataMember(Name = "any_on")]
        public bool? AnyOn { get; set; }

        [DataMember(Name = "all_on")]
        public bool? AllOn { get; set; }
    }
}