using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InnerCore.Api.deConz.Models.Rules
{
    [DataContract]
    public class RuleCondition
    {
        [DataMember(Name = "address")]
        public string Address { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "operator")]
        public RuleOperator Operator { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}