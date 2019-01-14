using InnerCore.Api.DeConz.Models;
using InnerCore.Api.DeConz.Models.Rules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InnerCore.Api.DeConz.Models.Schedule;

namespace InnerCore.Api.DeConz
{
    /// <summary>
    /// Partial DeConzClient, contains requests to the /Rules/ url
    /// </summary>
    public partial class DeConzClient
    {

        /// <summary>
        /// Asynchronously gets all rules registered with the bridge.
        /// </summary>
        /// <returns>An enumerable of <see cref="Rule"/>s registered with the bridge.</returns>
        public async Task<IReadOnlyCollection<Rule>> GetRulesAsync()
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client.GetStringAsync(new Uri(String.Format("{0}rules", ApiBase))).ConfigureAwait(false);

            List<Rule> results = new List<Rule>();

            JToken token = JToken.Parse(stringResult);
            if (token.Type == JTokenType.Object)
            {
                //Each property is a scene
                var jsonResult = (JObject)token;

                foreach (var prop in jsonResult.Properties())
                {
                    Rule rule = JsonConvert.DeserializeObject<Rule>(prop.Value.ToString());
                    rule.Id = prop.Name;

                    results.Add(rule);
                }

            }

            return results;

        }

        /// <summary>
        /// Asynchronously gets single rule
        /// </summary>
        /// <returns><see cref="Rule"/></returns>
        public async Task<Rule> GetRuleAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id can not be empty or a blank string", nameof(id));

            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client.GetStringAsync(new Uri(String.Format("{0}rules/{1}", ApiBase, id))).ConfigureAwait(false);

            JToken token = JToken.Parse(stringResult);
            if (token.Type == JTokenType.Array)
            {
                // Hue gives back errors in an array for this request
                JObject error = (JObject)token.First["error"];
                if (error["type"].Value<int>() == 3) // Rule not found
                    return null;

                throw new Exception(error["description"].Value<string>());
            }

            var rule = token.ToObject<Rule>();
            rule.Id = id;

            return rule;
        }

        public Task<string> CreateRule(Rule rule)
        {
            if (rule == null)
                throw new ArgumentNullException(nameof(rule));

            return CreateRule(rule.Name, rule.Conditions, rule.Actions);
        }

        public async Task<string> CreateRule(string name, IEnumerable<RuleCondition> conditions, IEnumerable<InternalBridgeCommand> actions)
        {
            CheckInitialized();

            if (conditions == null || !conditions.Any())
                throw new ArgumentNullException(nameof(conditions));
            if (actions == null || !actions.Any())
                throw new ArgumentNullException(nameof(actions));

            if (conditions.Count() > 8)
                throw new ArgumentException("Max 8 conditions allowed", nameof(conditions));
            if (actions.Count() > 8)
                throw new ArgumentException("Max 8 actions allowed", nameof(actions));

            JObject jsonObj = new JObject();
            if (conditions != null && conditions.Any())
                jsonObj.Add("conditions", JToken.FromObject(conditions, new JsonSerializer { NullValueHandling = NullValueHandling.Ignore }));
            if (actions != null && actions.Any())
                jsonObj.Add("actions", JToken.FromObject(actions, new JsonSerializer { NullValueHandling = NullValueHandling.Ignore }));

            if (!string.IsNullOrEmpty(name))
                jsonObj.Add("name", name);

            string jsonString = JsonConvert.SerializeObject(jsonObj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            //Create group with the lights we want to target
            var response = await client.PostAsync(new Uri(String.Format("{0}rules", ApiBase)), new JsonContent(jsonString)).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            DeConzResults rulesResult = DeserializeDefaultDeConzResult(jsonResult);

            if (rulesResult.Count > 0 && rulesResult[0].Success != null && !string.IsNullOrEmpty(rulesResult[0].Success.Id))
            {
                return rulesResult[0].Success.Id;
            }

            if (rulesResult.HasErrors())
                throw new Exception(rulesResult.Errors.First().Error.Description);

            return null;
        }

        public Task<DeConzResults> UpdateRule(Rule rule)
        {
            if (rule == null)
                throw new ArgumentNullException(nameof(rule));

            return UpdateRule(rule.Id, rule.Name, rule.Conditions, rule.Actions);
        }

        public async Task<DeConzResults> UpdateRule(string id, string name, IEnumerable<RuleCondition> conditions, IEnumerable<InternalBridgeCommand> actions)
        {
            CheckInitialized();

            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id must not be empty", nameof(id));


            JObject jsonObj = new JObject();
            if (conditions != null && conditions.Any())
                jsonObj.Add("conditions", JToken.FromObject(conditions, new JsonSerializer { NullValueHandling = NullValueHandling.Ignore }));
            if (actions != null && actions.Any())
                jsonObj.Add("actions", JToken.FromObject(actions, new JsonSerializer { NullValueHandling = NullValueHandling.Ignore }));

            if (!string.IsNullOrEmpty(name))
                jsonObj.Add("name", name);

            string jsonString = JsonConvert.SerializeObject(jsonObj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var response = await client.PutAsync(new Uri(String.Format("{0}rules/{1}", ApiBase, id)), new JsonContent(jsonString)).ConfigureAwait(false);

            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);

        }

        /// <summary>
        /// Deletes a rule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeConzResults> DeleteRule(string id)
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            var result = await client.DeleteAsync(new Uri(ApiBase + string.Format("rules/{0}", id))).ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);

        }

    }
}
