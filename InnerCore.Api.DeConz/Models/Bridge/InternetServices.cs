using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Bridge
{
    [DataContract]
    public class InternetServices
    {
        /// <summary>
        /// Connected:   If remote CLIP is available.
        /// Disconnected:  If remoteaccess is unavailable, reasons can be portalservices are false or no remote connection is available.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "remoteaccess")]
        public InternetServicesState RemoteAccess { get; set; }

        /// <summary>
        /// Connected:    Bridge has a connection to Internet.
        /// Disconnected:   Bridge cannot reach the Internet.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "internet")]
        public InternetServicesState Internet { get; set; }

        /// <summary>
        /// Connected:    Time was synchronized with internet service.
        /// Disconnected:  Internet time service was not reachable for 48hrs.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "time")]
        public InternetServicesState Time { get; set; }

        /// <summary>
        /// Connected:    swupdate server is available.
        /// Disconnected:  swupdate server was not reachable in the last 24 hrs.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "swupdate")]
        public InternetServicesState SwUpdate { get; set; }
    }
}
