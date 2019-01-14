using InnerCore.Api.deConz.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace InnerCore.Api.deConz.Converters
{
    /// <summary>
    /// Custom DateTime converter for deConz bridge
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

            DeConzDateTime deConzDateTime = value as DeConzDateTime;
            if (deConzDateTime == null)
            {
                return;
            }

            //DateTime
            if (deConzDateTime.DateTime != null && deConzDateTime.DateTime.HasValue)
            {
                dateTimeValue = deConzDateTime.DateTime.Value.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            }
            //RandomTime
            if (deConzDateTime.RandomizedTime != null && deConzDateTime.RandomizedTime.HasValue)
            {
                randomTimeValue = "A" + deConzDateTime.RandomizedTime.Value.ToString("hh\\:mm\\:ss");
            }
            //TimerTime
            if (deConzDateTime.TimerTime != null && deConzDateTime.TimerTime.HasValue)
            {
                timerTimeValue = "T" + deConzDateTime.TimerTime.Value.ToString("hh\\:mm\\:ss");
            }
            //Days recurring
            if (deConzDateTime.RecurringDay != RecurringDay.RecurringNone)
            {
                daysRecurring = string.Format("W{0}", Convert.ToString((int)deConzDateTime.RecurringDay));
            }
            //Recurrences
            if (deConzDateTime.NumberOfRecurrences != null && deConzDateTime.NumberOfRecurrences.HasValue)
            {
                recurrences = string.Format("R{0}", Convert.ToString(deConzDateTime.NumberOfRecurrences.Value));
            }

            if (!string.IsNullOrEmpty(daysRecurring) && !string.IsNullOrEmpty(timerTimeValue))//recurrenceday with a timerTime
            {
                returnValue = string.Format("{0}/{1}{2}", daysRecurring, timerTimeValue, randomTimeValue);
            }
            else if (!string.IsNullOrEmpty(daysRecurring) && !string.IsNullOrEmpty(dateTimeValue))//recurrenceday with a dateTimeValue
            {
                throw new Exception("Please set TimerTime when using RecurringDay");
            }
            else if (!string.IsNullOrEmpty(timerTimeValue))// (timertime only when in timers and weekdays)
            {
                returnValue = string.Format("P{0}{1}", timerTimeValue, randomTimeValue);

                //Recurrences (only with timers)
                if (!string.IsNullOrEmpty(recurrences))
                {
                    returnValue = string.Format("{0}/{1}", recurrences, returnValue);
                }
            }
            else
            {
                returnValue = string.Format("{0}{1}{2}", daysRecurring, dateTimeValue, randomTimeValue);
            }

            writer.WriteValue(returnValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DeConzDateTime hueValueDate = new DeConzDateTime();
            if (reader.TokenType == JsonToken.Date)
            {
                hueValueDate.DateTime = (DateTime)reader.Value;
                return hueValueDate;
            }

            string rawValue = reader.Value?.ToString();

            if (string.IsNullOrWhiteSpace(rawValue))
                return null;

            //normal datetimes (optional random time)
            {
                var groups = Regex.Match(rawValue, @"(?<date>[0-9\-]+)T(?<time>[0-9:]+)(A(?<randomtime>[0-9:]+))?").Groups;
                if (groups.Count != 1)
                {
                    hueValueDate.DateTime = DateTime.ParseExact(groups["date"].Value + "T" + groups["time"].Value, "yyyy-MM-ddTHH:mm:ss", (IFormatProvider)base.Culture, base.DateTimeStyles);
                    if (groups["randomtime"].Success)
                    {
                        hueValueDate.RandomizedTime = TimeSpan.ParseExact(groups["randomtime"].Value, "hh\\:mm\\:ss", (IFormatProvider)base.Culture);
                    }

                    return hueValueDate;
                }
            }

            //days recurring (optional random time)
            {
                var groups = Regex.Match(rawValue, @"W(?<daysrecurring>\d{1,3})/T(?<time>[0-9:]+)(A(?<randomtime>[0-9:]+))?").Groups;
                if (groups.Count != 1)
                {
                    hueValueDate.RecurringDay = (RecurringDay)Convert.ToInt32(groups["daysrecurring"].Value);
                    hueValueDate.TimerTime = TimeSpan.ParseExact(groups["time"].Value, "hh\\:mm\\:ss", (IFormatProvider)base.Culture);

                    if (groups["randomtime"].Success)
                    {
                        hueValueDate.RandomizedTime = TimeSpan.ParseExact(groups["randomtime"].Value, "hh\\:mm\\:ss", (IFormatProvider)base.Culture);
                    }

                    return hueValueDate;
                }
            }

            //timers (optional recurrences and random time)
            {
                var groups = Regex.Match(rawValue, @"(R(?<recurrence>\d{2})/)?PT(?<timertime>[0-9:]+)(A(?<randomtime>[0-9:]+))?").Groups;
                hueValueDate.TimerTime = TimeSpan.ParseExact(groups["timertime"].Value, "hh\\:mm\\:ss", (IFormatProvider)base.Culture);

                if (groups["randomtime"].Success)
                {
                    hueValueDate.RandomizedTime = TimeSpan.ParseExact(groups["randomtime"].Value, "hh\\:mm\\:ss", (IFormatProvider)base.Culture);
                }
                if (groups["recurrence"].Success)
                {
                    hueValueDate.NumberOfRecurrences = Convert.ToInt32(groups["recurrence"].Value);
                }

                return hueValueDate;
            }
        }
    }
}
