using System.Runtime.Serialization;

namespace InnerCore.Api.deConz.Models.Groups
{
    [DataContract]
    internal class CreateGroupRequest
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
