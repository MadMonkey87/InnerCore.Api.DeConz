using System.Collections.Generic;
using System.Runtime.Serialization;

namespace InnerCore.Api.deConz.Models.Bridge
{
    [DataContract]
    public class BridgeConfig
    {
        public BridgeConfig()
        {
            WhiteList = new Dictionary<string, WhiteList>();
        }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "mac")]
        public string MacAddress { get; set; }

        [DataMember(Name = "dhcp")]
        public bool Dhcp { get; set; }

        [DataMember(Name = "ipaddress")]
        public string IpAddress { get; set; }

        [DataMember(Name = "netmask")]
        public string NetMask { get; set; }

        [DataMember(Name = "gateway")]
        public string Gateway { get; set; }

        //Cant be a DateTime? because when value is not available, DeConzBridge sends value "none"
        //TODO: Create custom json deserializer
        [DataMember(Name = "UTC")]
        public string Utc { get; set; }

        [DataMember(Name = "swversion")]
        public string SoftwareVersion { get; set; }

        [DataMember(Name = "whitelist")]
        public IDictionary<string, WhiteList> WhiteList { get; set; }

        [DataMember(Name = "linkbutton")]
        public bool LinkButton { get; set; }

        [DataMember(Name = "portalservices")]
        public bool PortalServices { get; set; }

        [DataMember(Name = "portalconnection")]
        public string PortalConnection { get; set; }

        [DataMember(Name = "apiversion")]
        public string ApiVersion { get; set; }

        //Cant be a DateTime? because when value is not available, DeConzBridge sends value "none"
        //TODO: Create custom json deserializer
        [DataMember(Name = "localtime")]
        public string LocalTime { get; set; }

        [DataMember(Name = "timezone")]
        public string TimeZone { get; set; }

        [DataMember(Name = "portalstate")]
        public PortalState PortalState { get; set; }

        [DataMember(Name = "zigbeechannel")]
        public int ZigbeeChannel { get; set; }

        /// <summary>
        /// Perform a touchlink action if set to true, setting to false is ignored. When set to true a touchlink procedure starts which adds the closet lamp (within range) to the ZigBee network.  You can then search for new lights and lamp will show up in the bridge.
        /// </summary>
        [DataMember(Name = "touchlink")]
        public bool TouchLink { get; set; }

        /// <summary>
        /// Indicates if bridge settings are factory new.
        /// </summary>
        [DataMember(Name = "factorynew")]
        public bool FactoryNew { get; set; }

        /// <summary>
        ///  If a bridge backup file has been restored on this bridge from a bridge with a different bridgeid, it will indicate that bridge id, otherwise it will be null.
        /// </summary>
        [DataMember(Name = "replacesbridgeid")]
        public string ReplacesBridgeId { get; set; }

        /// <summary>
        /// This parameter uniquely identifies the hardware model of the bridge (BSB001, BSB002).
        /// </summary>
        [DataMember(Name = "modelid")]
        public string ModelId { get; set; }

        /// <summary>
        /// The unique bridge id. This is currently generated from the bridge Ethernet mac address.
        /// </summary>
        [DataMember(Name = "bridgeid")]
        public string BridgeId { get; set; }

        [DataMember(Name = "datastoreversion")]
        public string DataStoreVersion { get; set; }

        [DataMember(Name = "starterkitid")]
        public string StarterKitId { get; set; }

        [DataMember(Name = "internetservices")]
        public InternetServices InternetServices { get; set; }

        #region DeConz specific

        [DataMember(Name = "devicename")]
        public string DeviceName { get; set; }

        [DataMember(Name = "fwversion")]
        public string FirmwareVersion { get; set; }

        [DataMember(Name = "networkopenduration")]
        public int NetworkOpenDuration { get; set; }

        [DataMember(Name = "panid")]
        public int PanId { get; set; }

        [DataMember(Name = "rfconnected")]
        public bool RfConnected { get; set; }

        [DataMember(Name = "timeformat")]
        public TimeFormat TimeFormat { get; set; }

        [DataMember(Name = "uuid")]
        public string Uuid { get; set; }

        [DataMember(Name = "websocketnotifyall")]
        public bool WebsocketNotifyAll { get; set; }

        [DataMember(Name = "websocketport")]
        public int WebsocketPort { get; set; }

        #endregion
    }
}
