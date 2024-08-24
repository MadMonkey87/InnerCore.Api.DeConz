using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InnerCore.Api.DeConz.Models
{
    public class SuccessResult
    {
        public string Id { get; set; }

        [JsonExtensionData]
        private IDictionary<string, JToken> OtherData;
    }
}
