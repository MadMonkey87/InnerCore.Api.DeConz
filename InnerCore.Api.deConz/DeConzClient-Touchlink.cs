using InnerCore.Api.DeConz.Models.Touchlink;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InnerCore.Api.DeConz
{
    /// <summary>
    /// Partial DeConzClient, contains requests to the /Touchlink/ url
    /// </summary>
    public partial class DeConzClient
    {
        /// <summary>
        /// Starts scanning on all channels for devices which are located close to the gateway. The whole scan process will take about 10 seconds.
        /// </summary>
        public async Task<ScanResult> ScanForNewDevices()
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client.GetStringAsync(new Uri(String.Format("{0}touchlink/scan", ApiBase))).ConfigureAwait(false);

            var result = DeserializeResult<RawScanResult>(stringResult);

            return new ScanResult(result);
        }
    }
}
