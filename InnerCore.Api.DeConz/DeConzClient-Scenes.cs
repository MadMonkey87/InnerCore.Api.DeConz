using InnerCore.Api.DeConz.Models;
using InnerCore.Api.DeConz.Models.Scenes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InnerCore.Api.DeConz.Models.Lights;

namespace InnerCore.Api.DeConz
{
    /// <summary>
    /// Partial DeConzClient, contains requests to the /Scenes/ url
    /// </summary>
    public partial class DeConzClient
    {
        #region Public Methods
        /// <summary>
        /// Asynchronously gets all scenes by Group Id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>An enumerable of <see cref="Scene"/>s registered with the bridge.</returns>
        public async Task<IReadOnlyCollection<Scene>> GetScenesAsync(int groupId)
        {
            return await GetScenesAsync(groupId.ToString());
        }
        /// <summary>
        /// Asynchronously gets all scenes by Group Name
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="asStartWith">true = startWith / False = 1:1</param>
        /// <returns>An enumerable of <see cref="Scene"/>s registered with the bridge.</returns>
        public async Task<IReadOnlyCollection<Scene>> GetScenesByGroupNameAsync(string groupName, bool asStartWith = false)
        {
            var groups = await GetGroupsAsync();
            Models.Groups.Group group;
            if (asStartWith)
            {
                group = groups.FirstOrDefault(x => x.Name.StartsWith(groupName));
            }
            else
            {
                group = groups.FirstOrDefault(x => x.Name == groupName);
            }
            if (group == null)
                throw new NullReferenceException("No Group with this Name was found");

            return await GetScenesAsync(group.Id);
        }
        /// <summary>
        /// Asynchronously gets all scenes by Group Id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>An enumerable of <see cref="Scene"/>s registered with the bridge.</returns>
        public async Task<IReadOnlyCollection<Scene>> GetScenesAsync(string groupId)
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client.GetStringAsync(new Uri(String.Format("{0}groups/{1}/scenes", ApiBase, groupId))).ConfigureAwait(false);

            List<Scene> results = new List<Scene>();

            JToken token = JToken.Parse(stringResult);
            if (token.Type == JTokenType.Object)
            {
                //Each property is a scene
                var jsonResult = (JObject)token;

                foreach (var prop in jsonResult.Properties())
                {
                    Scene scene = JsonConvert.DeserializeObject<Scene>(prop.Value.ToString());
                    scene.Id = prop.Name;

                    results.Add(scene);
                }

            }

            return results;

        }

        /// <summary>
        /// Creates a new scene for a group. The actual state of each light will become the lights scene state.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public async Task<string> CreateSceneAsync(string sceneName, string groupid)
        {
            CheckInitialized();
            Scene scene = new Scene() { Name = sceneName };
            //Filter non updatable properties
            //Set these fields to null
            var sceneJson = JObject.FromObject(scene, new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore });
            sceneJson.Remove("Id");
            sceneJson.Remove("version");
            sceneJson.Remove("lastupdated");
            sceneJson.Remove("locked");
            sceneJson.Remove("owner");
            sceneJson.Remove("lightstates");

            string jsonString = JsonConvert.SerializeObject(sceneJson, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.PostAsync(new Uri(String.Format("{0}/groups/{1}/scenes", ApiBase,groupid)), new JsonContent(jsonString)).ConfigureAwait(false);

            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            DeConzResults sceneResult = DeserializeDefaultDeConzResult(jsonResult);

            if (sceneResult.Count > 0 && sceneResult[0].Success != null && !string.IsNullOrEmpty(sceneResult[0].Success.Id))
            {
                return sceneResult[0].Success.Id;
            }

            if (sceneResult.HasErrors())
                throw new Exception(sceneResult.Errors.First().Error.Description);

            return null;

        }

        /// <summary>
        /// UpdateSceneAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="lights"></param>
        /// <param name="storeLightState">If set, the lightstates of the lights in the scene will be overwritten by the current state of the lights. Can also be used in combination with transitiontime to update the transition time of a scene.</param>
        /// <param name="transitionTime">Can be used in combination with storeLightState</param>
        /// <returns></returns>
        [ObsoleteAttribute("This Method is obsolete.", true)]
        public async Task<DeConzResults> UpdateSceneAsync(string id, string name, IEnumerable<string> lights, bool? storeLightState = null, TimeSpan? transitionTime = null)
        {
            CheckInitialized();

            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id must not be empty", nameof(id));
            if (lights == null)
                throw new ArgumentNullException(nameof(lights));

            JObject jsonObj = new JObject();
            jsonObj.Add("lights", JToken.FromObject(lights));

            if (storeLightState.HasValue)
            {
                jsonObj.Add("storelightstate", storeLightState.Value);

                //Transitiontime can only be used in combination with storeLightState
                if (transitionTime.HasValue)
                {
                    jsonObj.Add("transitiontime", (uint)transitionTime.Value.TotalSeconds * 10);
                }
            }

            if (!string.IsNullOrEmpty(name))
                jsonObj.Add("name", name);


            string jsonString = JsonConvert.SerializeObject(jsonObj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.PutAsync(new Uri(String.Format("{0}scenes/{1}", ApiBase, id)), new JsonContent(jsonString)).ConfigureAwait(false);

            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);

        }

        /// <summary>
        /// UpdateSceneAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scene"></param>
        /// <returns></returns>
        [ObsoleteAttribute("This Method is obsolete.", true)]
        public async Task<DeConzResults> UpdateSceneAsync(string id, Scene scene)
        {
            CheckInitialized();

            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id must not be empty", nameof(id));
            if (scene == null)
                throw new ArgumentNullException(nameof(scene));

            //Set these fields to null
            var sceneJson = JObject.FromObject(scene, new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore });
            sceneJson.Remove("Id");
            sceneJson.Remove("recycle");
            sceneJson.Remove("version");
            sceneJson.Remove("lastupdated");
            sceneJson.Remove("locked");
            sceneJson.Remove("owner");
            sceneJson.Remove("lightstates");

            string jsonString = JsonConvert.SerializeObject(sceneJson, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.PutAsync(new Uri(String.Format("{0}scenes/{1}", ApiBase, id)), new JsonContent(jsonString)).ConfigureAwait(false);

            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);

        }


        public async Task<DeConzResults> ModifySceneAsync(string sceneId, string groupId, string lightId, LightCommand command)
        {
            CheckInitialized();

            if (sceneId == null)
                throw new ArgumentNullException(nameof(sceneId));
            if (sceneId.Trim() == String.Empty)
                throw new ArgumentException("sceneId must not be empty", nameof(sceneId));
            if (groupId == null)
                throw new ArgumentNullException(nameof(groupId));
            if (groupId.Trim() == String.Empty)
                throw new ArgumentException("groupId must not be empty", nameof(groupId));
            if (lightId == null)
                throw new ArgumentNullException(nameof(lightId));
            if (lightId.Trim() == String.Empty)
                throw new ArgumentException("lightId must not be empty", nameof(lightId));

            if (command == null)
                throw new ArgumentNullException(nameof(command));

            string jsonCommand = JsonConvert.SerializeObject(command, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.PutAsync(new Uri(String.Format("{0}/groups/{1}/scenes/{2}/lights/{3}/state", ApiBase,groupId, sceneId, lightId)), new JsonContent(jsonCommand)).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return DeserializeDefaultDeConzResult(jsonResult);
        }

        /// <summary>
        /// Recalls a scene. The actual state of each light in the group will become the lights scene state stored in each light.
        /// </summary>
        /// <param name="sceneId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Task<DeConzResults> RecallSceneAsync(string sceneId, string groupId = "0")
        {
            if (sceneId == null)
                throw new ArgumentNullException(nameof(sceneId));

            var groupCommand = new SceneCommand() { Scene = sceneId };

            return this.SendGroupCommandForScenesAsync(groupCommand, groupId);

        }

        /// <summary>
        /// Deletes a scene
        /// </summary>
        /// <param name="sceneId"></param>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public async Task<DeConzResults> DeleteSceneAsync(string sceneId, string groupid)
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client.DeleteAsync(new Uri(String.Format("{0}groups/{1}/scenes/{2}", ApiBase, groupid, sceneId))).ConfigureAwait(false);
            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);

        }

        /// <summary>
        /// Get a single scene
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Scene> GetSceneAsync(string id)
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client.GetStringAsync(new Uri(String.Format("{0}scenes/{1}", ApiBase, id))).ConfigureAwait(false);

            Scene scene = DeserializeResult<Scene>(stringResult);

            if (string.IsNullOrEmpty(scene.Id))
                scene.Id = id;

            return scene;

        }
        #endregion Public Methods
        #region Private Methods
        /// <summary>
        /// Send Scene command to a group
        /// </summary>
        /// <param name="command"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        private async Task<DeConzResults> SendGroupCommandForScenesAsync(SceneCommand command, string group = "0") //Group 0 contains all the lights
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client.PutAsync(new Uri(ApiBase + string.Format("groups/{0}/scenes/{1}/recall", group, command.Scene)), null).ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);

        }
        #endregion Private Methods
    }
}
