﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using InnerCore.Api.DeConz.Models;
using InnerCore.Api.DeConz.Models.Schedule;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InnerCore.Api.DeConz
{
    /// <summary>
    /// Partial DeConzClient, contains requests to the /Schedules/ url
    /// </summary>
    public partial class DeConzClient
    {
        /// <summary>
        /// Get all schedules
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<Schedule>> GetSchedulesAsync()
        {
            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client
                .GetStringAsync(new Uri(String.Format("{0}schedules", ApiBase)))
                .ConfigureAwait(false);

            List<Schedule> results = new List<Schedule>();

            JToken token = JToken.Parse(stringResult);
            if (token.Type == JTokenType.Object)
            {
                //Each property is a light
                var jsonResult = (JObject)token;

                foreach (var prop in jsonResult.Properties())
                {
                    Schedule newSchedule = JsonConvert.DeserializeObject<Schedule>(
                        prop.Value.ToString()
                    );
                    newSchedule.Id = prop.Name;

                    results.Add(newSchedule);
                }
            }

            return results;
        }

        /// <summary>
        /// Get a single schedule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Schedule> GetScheduleAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id must not be empty", nameof(id));

            CheckInitialized();

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            string stringResult = await client
                .GetStringAsync(new Uri(String.Format("{0}schedules/{1}", ApiBase, id)))
                .ConfigureAwait(false);

            Schedule schedule = DeserializeResult<Schedule>(stringResult);

            if (string.IsNullOrEmpty(schedule.Id))
                schedule.Id = id;

            return schedule;
        }

        /// <summary>
        /// Create a schedule
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task<string> CreateScheduleAsync(Schedule schedule)
        {
            if (schedule == null)
                throw new ArgumentNullException(nameof(schedule));

            CheckInitialized();

            //Set these fields to null
            var scheduleJson = JObject.FromObject(
                schedule,
                new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore }
            );
            scheduleJson.Remove("Id");
            scheduleJson.Remove("created");

            string command = JsonConvert.SerializeObject(
                scheduleJson,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);

            //Create schedule
            var result = await client
                .PostAsync(new Uri(ApiBase + "schedules"), new JsonContent(command))
                .ConfigureAwait(false);

            var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            DefaultDeConzResult[] scheduleResult =
                JsonConvert.DeserializeObject<DefaultDeConzResult[]>(jsonResult);

            if (
                scheduleResult.Length > 0
                && scheduleResult[0].Success != null
                && !string.IsNullOrEmpty(scheduleResult[0].Success.Id)
            )
            {
                return scheduleResult[0].Success.Id;
            }

            return null;
        }

        /// <summary>
        /// Update a schedule
        /// </summary>
        /// <param name="id"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task<DeConzResults> UpdateScheduleAsync(string id, Schedule schedule)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id must not be empty", nameof(id));
            if (schedule == null)
                throw new ArgumentNullException(nameof(schedule));

            CheckInitialized();

            //Set these fields to null
            var scheduleJson = JObject.FromObject(
                schedule,
                new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore }
            );
            scheduleJson.Remove("Id");
            scheduleJson.Remove("created");

            string command = JsonConvert.SerializeObject(
                scheduleJson,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpClient client = await GetHttpClient().ConfigureAwait(false);

            //Update schedule
            var result = await client
                .PutAsync(
                    new Uri(string.Format("{0}schedules/{1}", ApiBase, id)),
                    new JsonContent(command)
                )
                .ConfigureAwait(false);

            var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }

        /// <summary>
        /// Delete a schedule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeConzResults> DeleteScheduleAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (id.Trim() == String.Empty)
                throw new ArgumentException("id must not be empty", nameof(id));

            HttpClient client = await GetHttpClient().ConfigureAwait(false);
            //Delete schedule
            var result = await client
                .DeleteAsync(new Uri(ApiBase + string.Format("schedules/{0}", id)))
                .ConfigureAwait(false);

            string jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            return DeserializeDefaultDeConzResult(jsonResult);
        }
    }
}
