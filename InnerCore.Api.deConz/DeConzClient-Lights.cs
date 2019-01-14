using InnerCore.Api.deConz.Extensions;
using InnerCore.Api.deConz.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InnerCore.Api.deConz.Models.Lights;

namespace InnerCore.Api.deConz
{
    /// <summary>
    /// Partial DeConzClient, contains requests to the /Lights/ url
    /// </summary>
    public partial class DeConzClient
    {
        /// <summary>
        /// Asynchronously retrieves an individual light.
        /// </summary>
        /// <param name="id">The light's Id.</param>
        /// <returns>The <see cref="Light"/> if found, <c>null</c> if not.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="id"/> is empty or a blank string.</exception>
        public async Task<Light> GetLightAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id can not be empty or a blank string", nameof(id));

            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client.GetStringAsync(new Uri(String.Format("{0}lights/{1}", ApiBase, id))).ConfigureAwait(false);

            JToken token = JToken.Parse(stringResult);
            if (token.Type == JTokenType.Array)
            {
                // Hue gives back errors in an array for this request
                JObject error = (JObject)token.First["error"];
                if (error["type"].Value<int>() == 3) // Light not found
                    return null;

                throw new Exception(error["description"].Value<string>());
            }

            var light = token.ToObject<Light>();
            light.Id = id;

            return light;
        }

        /// <summary>
        /// Sets the light name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<DeConzResults> SetLightNameAsync(string id, string name)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id can not be empty or a blank string", nameof(id));

            CheckInitialized();

            string command = JsonConvert.SerializeObject(new { name = name });

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client.PutAsync(new Uri(String.Format("{0}lights/{1}", ApiBase, id)), new JsonContent(command)).ConfigureAwait(false);

            var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }

        /// <summary>
        /// Asynchronously gets all lights registered with the bridge.
        /// </summary>
        /// <returns>An enumerable of <see cref="Light"/>s registered with the bridge.</returns>
        public async Task<IEnumerable<Light>> GetLightsAsync()
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client.GetStringAsync(new Uri(String.Format("{0}lights", ApiBase))).ConfigureAwait(false);

            List<Light> results = new List<Light>();

            JToken token = JToken.Parse(stringResult);
            if (token.Type == JTokenType.Object)
            {
                //Each property is a light
                var jsonResult = (JObject)token;

                foreach (var prop in jsonResult.Properties())
                {
                    Light newLight = JsonConvert.DeserializeObject<Light>(prop.Value.ToString());
                    newLight.Id = prop.Name;
                    results.Add(newLight);
                }
            }
            return results;
        }

        /// <summary>
        /// Send a lightCommand to a list of lights
        /// </summary>
        /// <param name="command"></param>
        /// <param name="lightList">if null, send command to all lights</param>
        /// <returns></returns>
        public Task<DeConzResults> SendCommandAsync(LightCommand command, IEnumerable<string> lightList = null)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            string jsonCommand = JsonConvert.SerializeObject(command, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            return SendCommandRawAsync(jsonCommand, lightList);
        }


        /// <summary>
        /// Send a json command to a list of lights
        /// </summary>
        /// <param name="command"></param>
        /// <param name="lightList">if null, send command to all lights</param>
        /// <returns></returns>
        public async Task<DeConzResults> SendCommandRawAsync(string command, IEnumerable<string> lightList = null)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            CheckInitialized();

            if (lightList == null || !lightList.Any())
            {
                //Group 0 always contains all the lights
                return await SendGroupCommandAsync(command).ConfigureAwait(false);
            }
            else
            {
                DeConzResults results = new DeConzResults();

                await lightList.ForEachAsync(_parallelRequests, async (lightId) =>
                {
                    HttpClient client = await GetHttpClient().ConfigureAwait(false);
                    var result = await client.PutAsync(new Uri(ApiBase + string.Format("lights/{0}/state", lightId)), new JsonContent(command)).ConfigureAwait(false);

                    string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                    results.AddRange(DeserializeDefaultDeConzResult(jsonResult));

                }).ConfigureAwait(false);

                return results;
            }
        }

        /// <summary>
        /// Deletes a single light
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<DeleteDefaultDeConzResult>> DeleteLightAsync(string id)
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            //Delete light
            var result = await client.DeleteAsync(new Uri(ApiBase + string.Format("lights/{0}", id))).ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult<DeleteDefaultDeConzResult>(jsonResult);
        }
    }
}
