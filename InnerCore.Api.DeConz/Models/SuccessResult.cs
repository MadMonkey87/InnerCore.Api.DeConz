using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InnerCore.Api.DeConz.Models
{
    public class SuccessResult
    {
        public string Id { get; set; }

        [JsonExtensionData]
#pragma warning disable CS0169
		private IDictionary<string, JToken> OtherData;
#pragma warning restore CS0169
	}
}
