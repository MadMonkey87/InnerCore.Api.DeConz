﻿using System;
using System.Runtime.Serialization;
using InnerCore.Api.DeConz.Converters;
using Newtonsoft.Json;

namespace InnerCore.Api.DeConz.Models.Schedule
{
    [DataContract]
    public class Schedule
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "command")]
        public InternalBridgeCommand Command { get; set; }

        [DataMember(Name = "localtime")]
        [JsonConverter(typeof(DeConzDateTimeConverter))]
        public DeConzDateTime LocalTime { get; set; }

        [DataMember(Name = "created")]
        [JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? Created { get; set; }

        /// <summary>
        /// UTC time that the timer was started. Only provided for timers.
        /// </summary>
        [DataMember(Name = "starttime")]
        [JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Application is only allowed to set “enabled” or “disabled”. Disabled causes a timer to reset when activated (i.e. stop & reset). “enabled” when not provided on creation.
        /// </summary>
        [DataMember(Name = "status")]
        public ScheduleStatus? Status { get; set; }

        /// <summary>
        /// If set to true, the schedule will be removed automatically if expired, if set to false it will be disabled. Default is true
        /// </summary>
        [DataMember(Name = "autodelete")]
        public bool? AutoDelete { get; set; }
    }
}
