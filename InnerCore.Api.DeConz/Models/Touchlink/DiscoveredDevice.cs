using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Touchlink
{
    [DataContract]
    public class DiscoveredDevice
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "factorynew")]
        public bool FactoryNew { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }
    }
}
