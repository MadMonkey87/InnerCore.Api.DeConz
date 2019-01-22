using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;
using InnerCore.Api.DeConz.Converters;

namespace InnerCore.Api.DeConz.Models.Touchlink
{
    [DataContract]
    internal class RawScanResult
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "scanstate")]
        public ScanState State { get; set; }

        [JsonProperty("lastscan")]
        [JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? LastScanned { get; set; }

        [JsonProperty("result")]
        public Dictionary<string, DiscoveredDevice> DiscoveredDevices { get; set; }
    }
}
