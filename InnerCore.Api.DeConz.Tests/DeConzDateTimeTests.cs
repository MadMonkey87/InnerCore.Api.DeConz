using System;
using System.Collections.Generic;
using System.Text;
using InnerCore.Api.DeConz.Converters;
using InnerCore.Api.DeConz.Models;
using InnerCore.Api.DeConz.Models.Schedule;
using Newtonsoft.Json;
using Xunit;

namespace InnerCore.Api.DeConz.Tests
{
    public class DeConzDateTimeTests
    {
        [Fact]
        public void AbsoluteTimeDeConzDateTimeType()
        {
            string timeValue = "\"2014-09-20T19:35:26\"";
            string jsonString = "{ \"name\": \"some name\",\"description\": \"\",\"localtime\": " + timeValue + "}";

            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(jsonString);

            Assert.NotNull(schedule);
            Assert.NotNull(schedule.LocalTime);
            Assert.True(schedule.LocalTime.DateTime.HasValue);
            Assert.Equal(new DateTime(2014, 9, 20, 19, 35, 26), schedule.LocalTime.DateTime.Value);

            string result = JsonConvert.SerializeObject(schedule.LocalTime, new JsonConverter[] { new DeConzDateTimeConverter() });
            Assert.NotNull(result);
            Assert.Equal(timeValue, result);
        }

        [Fact]
        public void RandomizedDateTimeType()
        {
            string timeValue = "\"2014-09-20T19:35:26A00:30:00\"";
            string jsonString = "{ \"name\": \"some name\",\"description\": \"\",\"localtime\": " + timeValue + "}";

            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(jsonString);

            Assert.NotNull(schedule);
            Assert.NotNull(schedule.LocalTime);
            Assert.True(schedule.LocalTime.DateTime.HasValue);
            Assert.Equal(new DateTime(2014, 9, 20, 19, 35, 26), schedule.LocalTime.DateTime.Value);
            Assert.Equal(new TimeSpan(0, 30, 0), schedule.LocalTime.RandomizedTime);

            string result = JsonConvert.SerializeObject(schedule.LocalTime, new JsonConverter[] { new DeConzDateTimeConverter() });
            Assert.NotNull(result);
            Assert.Equal(timeValue, result);
        }

        [Fact]
        public void RecurringDateTimeType()
        {
            string timeValue = "\"W32/T19:45:00\"";
            string jsonString = "{ \"name\": \"some name\",\"description\": \"\",\"localtime\": " + timeValue + "}";

            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(jsonString);

            Assert.NotNull(schedule);
            Assert.NotNull(schedule.LocalTime);
            Assert.Null(schedule.LocalTime.RandomizedTime);
            Assert.True(schedule.LocalTime.TimerTime.HasValue);
            Assert.Equal(new TimeSpan(19, 45, 00), schedule.LocalTime.TimerTime.Value);
            Assert.Equal(RecurringDay.RecurringTuesday, schedule.LocalTime.RecurringDay); //W32

            string result = JsonConvert.SerializeObject(schedule.LocalTime, new JsonConverter[] { new DeConzDateTimeConverter() });
            Assert.NotNull(result);
            Assert.Equal(timeValue, result);
        }

        [Fact]
        public void RecurringRandomizedDateTimeType()
        {
            string timeValue = "\"W127/T19:45:00A00:30:00\"";
            string jsonString = "{ \"name\": \"some name\",\"description\": \"\",\"localtime\": " + timeValue + "}";

            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(jsonString);

            Assert.NotNull(schedule);
            Assert.NotNull(schedule.LocalTime);
            Assert.True(schedule.LocalTime.TimerTime.HasValue);
            Assert.Equal(new TimeSpan(19, 45, 00), schedule.LocalTime.TimerTime.Value);
            Assert.Equal(new TimeSpan(0, 30, 0), schedule.LocalTime.RandomizedTime);
            Assert.Equal(RecurringDay.RecurringAlldays, schedule.LocalTime.RecurringDay); //W127

            string result = JsonConvert.SerializeObject(schedule.LocalTime, new JsonConverter[] { new DeConzDateTimeConverter() });
            Assert.NotNull(result);
            Assert.Equal(timeValue, result);
        }

        [Fact]
        public void NormalTimerDateTimeType()
        {
            string timeValue = "\"PT19:45:00\"";
            string jsonString = "{ \"name\": \"some name\",\"description\": \"\",\"localtime\": " + timeValue + "}";

            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(jsonString);

            Assert.NotNull(schedule);
            Assert.NotNull(schedule.LocalTime);
            Assert.Null(schedule.LocalTime.RandomizedTime);
            Assert.True(schedule.LocalTime.TimerTime.HasValue);
            Assert.Equal(new TimeSpan(19, 45, 00), schedule.LocalTime.TimerTime.Value);

            string result = JsonConvert.SerializeObject(schedule.LocalTime, new JsonConverter[] { new DeConzDateTimeConverter() });
            Assert.NotNull(result);
            Assert.Equal(timeValue, result);
        }

        [Fact]
        public void TimerRandomizedDateTimeType()
        {
            string timeValue = "\"PT19:45:00A00:30:00\"";
            string jsonString = "{ \"name\": \"some name\",\"description\": \"\",\"localtime\": " + timeValue + "}";

            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(jsonString);

            Assert.NotNull(schedule);
            Assert.NotNull(schedule.LocalTime);
            Assert.True(schedule.LocalTime.TimerTime.HasValue);
            Assert.Equal(new TimeSpan(19, 45, 00), schedule.LocalTime.TimerTime.Value);
            Assert.Equal(new TimeSpan(0, 30, 0), schedule.LocalTime.RandomizedTime);

            string result = JsonConvert.SerializeObject(schedule.LocalTime, new JsonConverter[] { new DeConzDateTimeConverter() });
            Assert.NotNull(result);
            Assert.Equal(timeValue, result);
        }

        [Fact]
        public void RecurringTimerDateTimeType()
        {
            string timeValue = "\"R65/PT19:45:00\"";
            string jsonString = "{ \"name\": \"some name\",\"description\": \"\",\"localtime\": " + timeValue + "}";

            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(jsonString);

            Assert.NotNull(schedule);
            Assert.NotNull(schedule.LocalTime);
            Assert.Null(schedule.LocalTime.RandomizedTime);
            Assert.True(schedule.LocalTime.TimerTime.HasValue);
            Assert.True(schedule.LocalTime.NumberOfRecurrences.HasValue);
            Assert.Equal(new TimeSpan(19, 45, 00), schedule.LocalTime.TimerTime.Value);
            Assert.Equal<int?>(65, schedule.LocalTime.NumberOfRecurrences.Value);

            string result = JsonConvert.SerializeObject(schedule.LocalTime, new JsonConverter[] { new DeConzDateTimeConverter() });
            Assert.NotNull(result);
            Assert.Equal(timeValue, result);

        }

        [Fact]
        public void RecurringTimerRandomizedDateTimeType()
        {
            string timeValue = "\"R65/PT19:45:00A00:30:00\"";
            string jsonString = "{ \"name\": \"some name\",\"description\": \"\",\"localtime\": " + timeValue + "}";

            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(jsonString);

            Assert.NotNull(schedule);
            Assert.NotNull(schedule.LocalTime);
            Assert.True(schedule.LocalTime.TimerTime.HasValue);
            Assert.True(schedule.LocalTime.NumberOfRecurrences.HasValue);
            Assert.Equal(new TimeSpan(19, 45, 00), schedule.LocalTime.TimerTime.Value);
            Assert.Equal<int?>(65, schedule.LocalTime.NumberOfRecurrences.Value);
            Assert.Equal(new TimeSpan(0, 30, 0), schedule.LocalTime.RandomizedTime);

            string result = JsonConvert.SerializeObject(schedule.LocalTime, new JsonConverter[] { new DeConzDateTimeConverter() });
            Assert.NotNull(result);
            Assert.Equal(timeValue, result);
        }
    }
}
