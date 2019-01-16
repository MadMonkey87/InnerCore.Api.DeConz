using InnerCore.Api.DeConz.Exceptions;
using InnerCore.Api.DeConz.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InnerCore.Api.DeConz
{
    /// <summary>
    /// Partial DeConzClient, contains requests to the /api/ url
    /// </summary>
    public partial class DeConzClient
    {
        /// <summary>
        /// Register your <paramref name="applicationName"/> at the DeConz Bridge.
        /// </summary>
        /// <param name="applicationName">The name of the app.</param>
        /// <returns>Secret key for the app to communicate with the bridge.</returns>
        public async Task<string> RegisterAsync(string applicationName)
        {
            var result = await RegisterAsync(_ip,_port, applicationName);

            if (!string.IsNullOrEmpty(result))
            {
                Initialize(result);
            }

            return result;
        }

        /// <summary>
        /// Register your <paramref name="applicationName"/> at the DeConz Bridge.
        /// </summary>
        /// <param name="ip">ip address of bridge</param>
        /// <param name="port">port of the bridge</param>
        /// <param name="applicationName">The name of the app.</param>
        /// <returns>Secret key for the app to communicate with the bridge.</returns>
        public static async Task<string> RegisterAsync(string ip, int port, string applicationName)
        {
            JObject obj = new JObject();
            obj["devicetype"] = applicationName;

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.PostAsync(new Uri(string.Format("http://{0}:{1}/api", ip, port)), new JsonContent(obj.ToString())).ConfigureAwait(false);
            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            JObject result;
            try
            {
                JArray jresponse = JArray.Parse(stringResponse);
                result = (JObject)jresponse.First;
            }
            catch
            {
                //Not an expected response. Return response as exception
                throw new Exception(stringResponse);
            }

            JToken error;
            if (result.TryGetValue("error", out error))
            {
                if (error["type"].Value<int>() == 101) // link button not pressed
                    throw new LinkButtonNotPressedException("Link button not pressed");
                else
                    throw new Exception(error["description"].Value<string>());
            }

            return result["success"]["username"].Value<string>();
        }

        public async Task<bool> CheckConnection()
        {
            HttpClient client = await GetHttpClient().ConfigureAwait(false);

            try
            {
                //Check if there is a hue bridge on the specified IP by checking the content of description.xml
                var result = await client.GetAsync(string.Format("http://{0}:{1}/description.xml", _ip, _port)).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
                    string res = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(res))
                    {
                        if (!res.ToLower().Contains("philips hue bridge"))
                            return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            try
            {
                //Check if app is registered
                var test = await this.GetBridgeAsync().ConfigureAwait(false);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
