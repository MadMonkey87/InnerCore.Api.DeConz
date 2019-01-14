using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Groups
{
    [DataContract]
    internal class CreateGroupRequest
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
