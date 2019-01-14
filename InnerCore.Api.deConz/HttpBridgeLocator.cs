using InnerCore.Api.DeConz.Models.Bridge;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace InnerCore.Api.DeConz
{
    /// <summary>
    /// Uses the special nupnp url from dresden elektronik to find registered bridges based on your external IP
    /// </summary>
    public class HttpBridgeLocator
    {
        /// <summary>
        /// Locate bridges
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LocatedBridge>> LocateBridgesAsync(TimeSpan timeout)
        {
            // since this specifies timeout (and probably isn't called much), don't use shared client

            HttpClient client = new HttpClient() { Timeout = timeout };

            string response = await client.GetStringAsync(new Uri(Constants.DiscoveryUrl)).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<LocatedBridge[]>(response);
        }
    }
}