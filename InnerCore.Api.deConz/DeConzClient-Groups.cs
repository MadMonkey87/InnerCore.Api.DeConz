using InnerCore.Api.deConz.Interfaces;
using InnerCore.Api.deConz.Models;
using InnerCore.Api.deConz.Models.Groups;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InnerCore.Api.deConz
{
    /// <summary>
    /// Partial DeConzClient, contains requests to the /Groups/ url
    /// </summary>
    public partial class DeConzClient
    {
        /// <summary>
        /// Create a group for a list of lights
        /// </summary>
        /// <param name="lights">List of lights in the group</param>
        /// <param name="name">Optional name</param>
        /// <returns></returns>
        public async Task<string> CreateGroupAsync(string name = null)
        {
            CheckInitialized();

            CreateGroupRequest jsonObj = new CreateGroupRequest();

            if (!string.IsNullOrEmpty(name))
                jsonObj.Name = name;

            string jsonString = JsonConvert.SerializeObject(jsonObj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            //Create group with the lights we want to target
            var response = await client.PostAsync(new Uri(String.Format("{0}groups", ApiBase)), new JsonContent(jsonString)).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            DeConzResults groupResult = DeserializeDefaultDeConzResult(jsonResult);

            if (groupResult.Count > 0 && groupResult[0].Success != null && !string.IsNullOrEmpty(groupResult[0].Success.Id))
            {
                return groupResult[0].Success.Id.Replace("/groups/", string.Empty);
            }

            if (groupResult.HasErrors())
                throw new Exception(groupResult.Errors.First().Error.Description);

            return null;

        }

        /// <summary>
        /// Deletes a single group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<DeConzResults> DeleteGroupAsync(string groupId)
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            //Delete group 1
            var result = await client.DeleteAsync(new Uri(ApiBase + string.Format("groups/{0}", groupId))).ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);

        }

        /// <summary>
        /// Send command to a group
        /// </summary>
        /// <param name="command"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public Task<DeConzResults> SendGroupCommandAsync(ICommandBody command, string group = "0")
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            string jsonCommand = JsonConvert.SerializeObject(command, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            return SendGroupCommandAsync(jsonCommand, group);
        }

        /// <summary>
        /// Send command to a group
        /// </summary>
        /// <param name="command"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        private async Task<DeConzResults> SendGroupCommandAsync(string command, string group = "0") //Group 0 contains all the lights
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client.PutAsync(new Uri(ApiBase + string.Format("groups/{0}/action", group)), new JsonContent(command)).ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);

        }

        /// <summary>
        /// Get all groups
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<Group>> GetGroupsAsync()
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client.GetStringAsync(new Uri(String.Format("{0}groups", ApiBase))).ConfigureAwait(false);

            List<Group> results = new List<Group>();

            JToken token = JToken.Parse(stringResult);
            if (token.Type == JTokenType.Object)
            {
                //Each property is a light
                var jsonResult = (JObject)token;

                foreach (var prop in jsonResult.Properties())
                {
                    Group newGroup = JsonConvert.DeserializeObject<Group>(prop.Value.ToString());
                    newGroup.Id = prop.Name;

                    results.Add(newGroup);
                }

            }

            return results;

        }

        /// <summary>
        /// Get the state of a single group
        /// </summary>
        /// <returns></returns>
        public async Task<Group> GetGroupAsync(string id)
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client.GetStringAsync(new Uri(String.Format("{0}groups/{1}", ApiBase, id))).ConfigureAwait(false);

            Group group = DeserializeResult<Group>(stringResult);

            if (string.IsNullOrEmpty(group.Id))
                group.Id = id;

            return group;
        }

        /// <summary>
        /// Update a group
        /// </summary>
        /// <param name="id">Group ID</param>
        /// <param name="lights">List of light IDs</param>
        /// <param name="name">Group Name</param>
        /// <returns></returns>
        public async Task<DeConzResults> UpdateGroupAsync(string id, IEnumerable<string> lights, string name = null, bool? hidden = null, IEnumerable<string> lightSequence = null, IEnumerable<string> mulitDeviceIds = null)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id must not be empty", nameof(id));

            UpdateGroupRequest jsonObj = new UpdateGroupRequest();
            jsonObj.Lights = lights ?? Enumerable.Empty<string>();
            jsonObj.Hidden = hidden;
            jsonObj.MulitDeviceIds = mulitDeviceIds ?? Enumerable.Empty<string>();
            jsonObj.LightSequence = lightSequence ?? Enumerable.Empty<string>();

            if (!string.IsNullOrEmpty(name))
                jsonObj.Name = name;

            string jsonString = JsonConvert.SerializeObject(jsonObj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.PutAsync(new Uri(String.Format("{0}groups/{1}", ApiBase, id)), new JsonContent(jsonString)).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }
    }
}
