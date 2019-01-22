using System;
using System.Runtime.Serialization;
using InnerCore.Api.DeConz.Converters;
using Newtonsoft.Json;

namespace InnerCore.Api.DeConz.Models.Bridge
{
    [DataContract]
    public class WhiteList
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "last use date")]
        [JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? LastUsedDate { get; set; }

        [DataMember(Name = "create date")]
        [JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? CreateDate { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
