using System;
using System.Net.Http;
using System.Threading.Tasks;
using InnerCore.Api.DeConz.Models;
using InnerCore.Api.DeConz.Models.Touchlink;
using Newtonsoft.Json;

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
            string stringResult = await client
                .GetStringAsync(new Uri(String.Format("{0}touchlink/scan", ApiBase)))
                .ConfigureAwait(false);

            var result = DeserializeResult<RawScanResult>(stringResult);

            return new ScanResult(result);
        }

        /// <summary>
        /// Puts a device into identify mode for example a light will blink a few times.
        /// </summary>
        /// <param name="id">id must be one of the indentifiers which are returned in the scan result</param>
        /// <returns></returns>
        public async Task<DeConzResults> Identity(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id can not be empty or a blank string", nameof(id));

            CheckInitialized();

            string command = JsonConvert.SerializeObject(
                new { },
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client
                .PostAsync(
                    new Uri(string.Format("{0}touchlink/{1}/identify", ApiBase, id)),
                    new JsonContent(command)
                )
                .ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }

        /// <summary>
        /// Send a reset to factory new request to a device.
        /// </summary>
        /// <param name="id">id must be one of the indentifiers which are returned in the scan result</param>
        /// <returns></returns>
        public async Task<DeConzResults> ResetDevice(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id can not be empty or a blank string", nameof(id));

            CheckInitialized();

            string command = JsonConvert.SerializeObject(
                new { },
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client
                .PostAsync(
                    new Uri(string.Format("{0}touchlink/{1}/reset", ApiBase, id)),
                    new JsonContent(command)
                )
                .ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }
    }
}
