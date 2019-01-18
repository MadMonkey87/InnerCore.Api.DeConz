using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Bridge
{
    [DataContract]
    public class ResetRequest
    {
        /// <summary>
        /// Set the network settings of the gateway to factory new.
        /// </summary>
        [DataMember(Name = "resetGW")]
        public bool? ResetGateway { get; set; }

        /// <summary>
        /// Delete the Database.
        /// </summary>
        [DataMember(Name = "deleteDB")]
        public bool? DeleteDatabase { get; set; }
    }
}
