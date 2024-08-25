using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using InnerCore.Api.DeConz.Models;
using InnerCore.Api.DeConz.Models.Sensors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InnerCore.Api.DeConz
{
    /// <summary>
    /// Partial DeConzClient, contains requests to the /Sensors/ url
    /// </summary>
    public partial class DeConzClient
    {
        /// <summary>
        /// Asynchronously gets all sensors registered with the bridge.
        /// </summary>
        /// <returns>An enumerable of <see cref="Sensor"/>s registered with the bridge.</returns>
        public async Task<IReadOnlyCollection<Sensor>> GetSensorsAsync()
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client
                .GetStringAsync(new Uri(String.Format("{0}sensors", ApiBase)))
                .ConfigureAwait(false);

            List<Sensor> results = new List<Sensor>();

            JToken token = JToken.Parse(stringResult);
            if (token.Type == JTokenType.Object)
            {
                //Each property is a scene
                var jsonResult = (JObject)token;

                foreach (var prop in jsonResult.Properties())
                {
                    Sensor scene = JsonConvert.DeserializeObject<Sensor>(prop.Value.ToString());
                    scene.Id = prop.Name;

                    results.Add(scene);
                }
            }

            return results;
        }

        public async Task<string> CreateSensorAsync(Sensor sensor)
        {
            if (sensor == null)
                throw new ArgumentNullException(nameof(sensor));

            CheckInitialized();

            //Set fields to null
            sensor.Id = null;

            string sensorJson = JsonConvert.SerializeObject(
                sensor,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);

            //Create sensor
            var result = await client
                .PostAsync(
                    new Uri(String.Format("{0}sensors", ApiBase)),
                    new JsonContent(sensorJson)
                )
                .ConfigureAwait(false);

            var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            DefaultDeConzResult[] sensorResult =
                JsonConvert.DeserializeObject<DefaultDeConzResult[]>(jsonResult);

            if (
                sensorResult.Length > 0
                && sensorResult[0].Success != null
                && !string.IsNullOrEmpty(sensorResult[0].Success.Id)
            )
            {
                var id = sensorResult[0].Success.Id;
                sensor.Id = id;
                return id;
            }

            return null;
        }

        /// <summary>
        /// Asynchronously gets single sensor
        /// </summary>
        /// <returns><see cref="Sensor"/></returns>
        public async Task<Sensor> GetSensorAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id can not be empty or a blank string", nameof(id));

            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client
                .GetStringAsync(new Uri(String.Format("{0}sensors/{1}", ApiBase, id)))
                .ConfigureAwait(false);

            JToken token = JToken.Parse(stringResult);
            if (token.Type == JTokenType.Array)
            {
                // Hue gives back errors in an array for this request
                JObject error = (JObject)token.First["error"];
                if (error["type"].Value<int>() == 3) // Rule not found
                    return null;

                throw new Exception(error["description"].Value<string>());
            }

            var sensor = token.ToObject<Sensor>();
            sensor.Id = id;

            return sensor;
        }

        /// <summary>
        /// Update a sensor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        public async Task<DeConzResults> UpdateSensorAsync(string id, string newName)
        {
            CheckInitialized();

            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id must not be empty", nameof(id));
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException(nameof(newName));

            JObject jsonObj = new JObject();
            jsonObj.Add("name", newName);

            string jsonString = JsonConvert.SerializeObject(
                jsonObj,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);

            //Update sensor
            var result = await client
                .PutAsync(
                    new Uri(string.Format("{0}sensors/{1}", ApiBase, id)),
                    new JsonContent(jsonString)
                )
                .ConfigureAwait(false);

            var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }

        /// <summary>
        /// Changes the Sensor configuration
        /// </summary>
        /// <param name="id"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<DeConzResults> ChangeSensorConfigAsync(string id, SensorConfig config)
        {
            CheckInitialized();

            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id must not be empty", nameof(id));
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            var updateJson = JObject.FromObject(
                config,
                new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore }
            );

            //Remove properties from json that are readonly
            updateJson.Remove("battery");
            updateJson.Remove("reachable");
            updateJson.Remove("configured");
            updateJson.Remove("pending");
            updateJson.Remove("sensitivitymax");

            string jsonString = JsonConvert.SerializeObject(
                updateJson,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);

            //Change sensor config
            var result = await client
                .PutAsync(
                    new Uri(string.Format("{0}sensors/{1}/config", ApiBase, id)),
                    new JsonContent(jsonString)
                )
                .ConfigureAwait(false);

            var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }

        public async Task<DeConzResults> ChangeSensorStateAsync(string id, SensorState state)
        {
            CheckInitialized();

            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id must not be empty", nameof(id));
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            string jsonString = JsonConvert.SerializeObject(
                state,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);

            //Change sensor state
            var result = await client
                .PutAsync(
                    new Uri(string.Format("{0}sensors/{1}/state", ApiBase, id)),
                    new JsonContent(jsonString)
                )
                .ConfigureAwait(false);

            var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }

        /// <summary>
        /// Deletes a single sensor
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<DeleteDefaultDeConzResult>> DeleteSensorAsync(
            string id
        )
        {
            CheckInitialized();

            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id must not be empty", nameof(id));

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            //Delete sensor
            var result = await client
                .DeleteAsync(new Uri(ApiBase + string.Format("sensors/{0}", id)))
                .ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult<DeleteDefaultDeConzResult>(jsonResult);
        }
    }
}
