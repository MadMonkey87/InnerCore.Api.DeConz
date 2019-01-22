using System;
using System.Runtime.Serialization;
using InnerCore.Api.DeConz.Converters;
using Newtonsoft.Json;

namespace InnerCore.Api.DeConz.Models.Bridge
{
    /// <summary>
    /// Allowed properties to update the BridgeConfig
    /// </summary>
    [DataContract]
    public class BridgeConfigUpdate
    {
        /// <summary>
        /// Name of the gateway.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Set connected state of the gateway.
        /// </summary>
        [DataMember(Name = "rfconnected")]
        public bool? RfConnected { get; set; }

        /// <summary>
        /// Set update channel.
        /// </summary>
        [DataMember(Name = "updatechannel")]
        public UpdateChannel? UpdateChannel { get; set; }

        /// <summary>
        /// Open the network so that other zigbee devices can join. 0 = network closed, 255 = network open, 1..254 = time in seconds the network remains open. The value will decrement automatically.
        /// </summary>
        [DataMember(Name = "permitjoin")]
        public int? PermitJoin { get; set; }

        /// <summary>
        /// Time between two group commands in milliseconds.
        /// </summary>
        [DataMember(Name = "groupdelay")]
        public int? GroupDelay { get; set; }

        /// <summary>
        /// Set OTAU active or inactive.
        /// </summary>
        [DataMember(Name = "otauactive")]
        public bool? OtauActive { get; set; }

        /// <summary>
        /// Set gateway discovery over the internet active or inactive.
        /// </summary>
        [DataMember(Name = "discovery")]
        public bool? Discovery { get; set; }

        /// <summary>
        /// Unlock the gateway so that apps can register themselves to the gateway (time in seconds).
        /// </summary>
        [DataMember(Name = "unlock")]
        public bool? Unlock { get; set; }

        /// <summary>
        /// Set the zigbeechannel of the gateway. Notify other Zigbee devices also to change their channel. It can take values of 11, 15, 20,25.
        /// </summary>
        [DataMember(Name = "zigbeechannel")]
        public int? ZigbeeChannel { get; set; }

        /// <summary>
        /// Set the timezone of the gateway (only on Raspberry Pi). Format: tzdatabase e.g. “Europe/Berlin” https://en.wikipedia.org/wiki/List_of_tz_database_time_zones
        /// </summary>
        [DataMember(Name = "timezone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Set the UTC time of the gateway (only on Raspbery Pi) in ISO 8601 format (yyyy-MM-ddTHH:mm:ss).
        /// </summary>
        [DataMember(Name = "utc")]
        [JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? Utc { get; set; }

        /// <summary>
        /// Can be used to store the timeformat permanently.
        /// </summary>
        [DataMember(Name = "timeformat")]
        public TimeFormat? TimeFormat { get; set; }
    }
}
