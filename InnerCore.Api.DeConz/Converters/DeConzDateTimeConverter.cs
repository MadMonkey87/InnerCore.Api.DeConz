using InnerCore.Api.DeConz.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace InnerCore.Api.DeConz.Converters
{
    /// <summary>
    /// Custom DateTime converter for DeConz bridge
    /// </summary>
    public class DeConzDateTimeConverter : IsoDateTimeConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DeConzDateTime);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string returnValue = string.Empty;
            string dateTimeValue = string.Empty;
            string randomTimeValue = string.Empty;
            string timerTimeValue = string.Empty;
            string daysRecurring = string.Empty;
            string recurrences = string.Empty;

            if (value == null)
            {
                return;
            }

            if (!(value is DeConzDateTime deConzDateTime))
            {
                return;
            }

            //DateTime
            if (deConzDateTime.DateTime != null)
            {
                dateTimeValue = deConzDateTime.DateTime.Value.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            }

            //RandomTime
            if (deConzDateTime.RandomizedTime != null)
            {
                randomTimeValue = "A" + deConzDateTime.RandomizedTime.Value.ToString("hh\\:mm\\:ss");
            }

            //TimerTime
            if (deConzDateTime.TimerTime != null)
            {
                timerTimeValue = "T" + deConzDateTime.TimerTime.Value.ToString("hh\\:mm\\:ss");
            }

            //Days recurring
            if (deConzDateTime.RecurringDay != RecurringDay.RecurringNone)
            {
                daysRecurring = $"W{Convert.ToString((int) deConzDateTime.RecurringDay)}";
            }

            //Recurrences
            if (deConzDateTime.NumberOfRecurrences != null)
            {
                recurrences = $"R{Convert.ToString(deConzDateTime.NumberOfRecurrences.Value)}";
            }

            if (!string.IsNullOrEmpty(daysRecurring) && !string.IsNullOrEmpty(timerTimeValue))//recurrenceday with a timerTime
            {
                returnValue = $"{daysRecurring}/{timerTimeValue}{randomTimeValue}";
            }

            else if (!string.IsNullOrEmpty(daysRecurring) && !string.IsNullOrEmpty(dateTimeValue))//recurrenceday with a dateTimeValue
            {
                throw new Exception("Please set TimerTime when using RecurringDay");
            }

            else if (!string.IsNullOrEmpty(timerTimeValue))// (timertime only when in timers and weekdays)
            {
                returnValue = $"P{timerTimeValue}{randomTimeValue}";

                //Recurrences (only with timers)
                if (!string.IsNullOrEmpty(recurrences))
                {
                    returnValue = $"{recurrences}/{returnValue}";
                }
            }
            else
            {
                returnValue = $"{daysRecurring}{dateTimeValue}{randomTimeValue}";
            }

            writer.WriteValue(returnValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DeConzDateTime deconzValueDate = new DeConzDateTime();
            if (reader.TokenType == JsonToken.Date)
            {
                deconzValueDate.DateTime = (DateTime)reader.Value;
                return deconzValueDate;
            }

            string rawValue = reader.Value?.ToString();

            if (string.IsNullOrWhiteSpace(rawValue))
                return null;

            //normal datetimes (optional random time)
            {
                var groups = Regex.Match(rawValue, @"(?<date>[0-9\-]+)T(?<time>[0-9:]+)(A(?<randomtime>[0-9:]+))?").Groups;
                if (groups.Count != 1)
                {
                    deconzValueDate.DateTime = DateTime.ParseExact(groups["date"].Value + "T" + groups["time"].Value, "yyyy-MM-ddTHH:mm:ss", Culture, DateTimeStyles);
                    if (groups["randomtime"].Success)
                    {
                        deconzValueDate.RandomizedTime = TimeSpan.ParseExact(groups["randomtime"].Value, "hh\\:mm\\:ss", Culture);
                    }

                    return deconzValueDate;
                }
            }

            //days recurring (optional random time)
            {
                var groups = Regex.Match(rawValue, @"W(?<daysrecurring>\d{1,3})/T(?<time>[0-9:]+)(A(?<randomtime>[0-9:]+))?").Groups;
                if (groups.Count != 1)
                {
                    deconzValueDate.RecurringDay = (RecurringDay)Convert.ToInt32(groups["daysrecurring"].Value);
                    deconzValueDate.TimerTime = TimeSpan.ParseExact(groups["time"].Value, "hh\\:mm\\:ss", Culture);

                    if (groups["randomtime"].Success)
                    {
                        deconzValueDate.RandomizedTime = TimeSpan.ParseExact(groups["randomtime"].Value, "hh\\:mm\\:ss", Culture);
                    }

                    return deconzValueDate;
                }
            }

            //timers (optional recurrences and random time)
            {
                var groups = Regex.Match(rawValue, @"(R(?<recurrence>\d{2})/)?PT(?<timertime>[0-9:]+)(A(?<randomtime>[0-9:]+))?").Groups;
                deconzValueDate.TimerTime = TimeSpan.ParseExact(groups["timertime"].Value, "hh\\:mm\\:ss", Culture);

                if (groups["randomtime"].Success)
                {
                    deconzValueDate.RandomizedTime = TimeSpan.ParseExact(groups["randomtime"].Value, "hh\\:mm\\:ss", Culture);
                }
                if (groups["recurrence"].Success)
                {
                    deconzValueDate.NumberOfRecurrences = Convert.ToInt32(groups["recurrence"].Value);
                }

                return deconzValueDate;
            }
        }
    }
}
