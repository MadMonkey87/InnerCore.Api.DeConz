using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Touchlink
{
    [DataContract]
    internal class RawScanResult
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "scanstate")]
        public ScanState State { get; set; }

        [JsonProperty("lastscan")]
        public string LastScanned { get; set; }

        [JsonProperty("result")]
        public Dictionary<string, DiscoveredDevice> DiscoveredDevices { get; set; }
    }
}
