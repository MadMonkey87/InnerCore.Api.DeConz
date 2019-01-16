using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Bridge
{
    [DataContract]
    public class WhiteList
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "last use date")]
        public string LastUsedDate { get; set; }

        [DataMember(Name = "create date")]
        public string CreateDate { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
