﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InnerCore.Api.DeConz.Models;
using InnerCore.Api.DeConz.Models.Bridge;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InnerCore.Api.DeConz
{
    /// <summary>
    /// Partial DeConzClient, contains requests to the /config/ url
    /// </summary>
    public partial class DeConzClient
    {
        /// <summary>
        /// Deletes a whitelist entry
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteWhiteListEntryAsync(string entry)
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);

            var response = await client
                .DeleteAsync(new Uri(string.Format("{0}config/whitelist/{1}", ApiBase, entry)))
                .ConfigureAwait(false);
            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            JArray jresponse = JArray.Parse(stringResponse);
            JObject result = (JObject)jresponse.First;

            JToken error;
            if (result.TryGetValue("error", out error))
            {
                if (error["type"].Value<int>() == 3) // entry not available
                    return false;
                else
                    throw new Exception(error["description"].Value<string>());
            }

            return true;
        }

        /// <summary>
        /// Asynchronously gets the whitelist with the bridge.
        /// </summary>
        /// <returns>An enumerable of <see cref="WhiteList"/>s registered with the bridge.</returns>
        public async Task<IEnumerable<WhiteList>> GetWhiteListAsync()
        {
            //Not needed to check if initialized, can be used without API key

            BridgeConfig config = await GetConfigAsync().ConfigureAwait(false);

            return config.WhiteList.Select(l => l.Value).ToList();
        }

        /// <summary>
        /// Get bridge info
        /// </summary>
        /// <returns></returns>
        public async Task<Bridge> GetBridgeAsync()
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var stringResult = await client.GetStringAsync(new Uri(ApiBase)).ConfigureAwait(false);

            BridgeState jsonResult = DeserializeResult<BridgeState>(stringResult);

            return new Bridge(jsonResult);
        }

        /// <summary>
        /// Get bridge config
        /// </summary>
        /// <returns>BridgeConfig object</returns>
        public async Task<BridgeConfig> GetConfigAsync()
        {
            //Not needed to check if initialized, can be used without API key

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client
                .GetStringAsync(new Uri(String.Format("{0}config", ApiBase)))
                .ConfigureAwait(false);
            JToken token = JToken.Parse(stringResult);
            BridgeConfig config = null;
            if (token.Type == JTokenType.Object)
            {
                var jsonResult = (JObject)token;
                config = JsonConvert.DeserializeObject<BridgeConfig>(jsonResult.ToString());

                //Fix whitelist IDs
                foreach (var whitelist in config.WhiteList)
                    whitelist.Value.Id = whitelist.Key;
            }
            return config;
        }

        /// <summary>
        /// Update bridge config
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<DeConzResults> UpdateBridgeConfigAsync(BridgeConfigUpdate update)
        {
            CheckInitialized();

            string command = JsonConvert.SerializeObject(
                update,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client
                .PutAsync(new Uri(string.Format("{0}config", ApiBase)), new JsonContent(command))
                .ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }

        /// <summary>
        /// Updates the software of the bridge if available
        /// </summary>
        /// <returns></returns>
        public async Task<DeConzResults> UpdateSoftware()
        {
            CheckInitialized();

            string command = JsonConvert.SerializeObject(
                new { },
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client
                .PostAsync(
                    new Uri(string.Format("{0}config/update", ApiBase)),
                    new JsonContent(command)
                )
                .ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }

        /// <summary>
        /// Starts the update firmware process if newer firmware is available.
        /// </summary>
        /// <returns></returns>
        public async Task<DeConzResults> UpdateFirmware()
        {
            CheckInitialized();

            string command = JsonConvert.SerializeObject(
                new { },
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client
                .PostAsync(
                    new Uri(string.Format("{0}config/updatefirmware", ApiBase)),
                    new JsonContent(command)
                )
                .ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }

        /// <summary>
        /// Reset the gateway network settings to factory new and/or delete the deCONZ database (config, lights, scenes, groups, schedules, devices, rules).
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DeConzResults> Reset(ResetRequest request)
        {
            CheckInitialized();

            string command = JsonConvert.SerializeObject(
                request,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client
                .PostAsync(
                    new Uri(string.Format("{0}config/reset", ApiBase)),
                    new JsonContent(command)
                )
                .ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }

        /// <summary>
        /// Change the Password of the Gateway
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DeConzResults> ChangePassword(ChangePasswordRequest request)
        {
            CheckInitialized();

            string command = JsonConvert.SerializeObject(
                request,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client
                .PutAsync(
                    new Uri(string.Format("{0}config/password", ApiBase)),
                    new JsonContent(command)
                )
                .ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }

        /// <summary>
        /// Resets the username and password to default (“delight”,”delight”). Only possible within 10 minutes after gateway start.
        /// </summary>
        /// <returns></returns>
        public async Task<DeConzResults> ResetPassword()
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client
                .DeleteAsync(new Uri(string.Format("{0}config/password", ApiBase)))
                .ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }
    }
}
