using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Touchlink
{
    [DataContract]
    public class ScanResult
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "scanstate")]
        public ScanState State { get; set; }

        [JsonProperty("lastscan")]
        public string LastScanned { get; set; }
    }
}
