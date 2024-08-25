﻿using System;
using InnerCore.Api.DeConz.Interfaces;
using InnerCore.Api.DeConz.Models.Schedule;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InnerCore.Api.DeConz.Converters
{
    internal class CommandBodyConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ICommandBody);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer
        )
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject jObject = JObject.Load(reader);

                return new GenericScheduleCommand(jObject.ToString());
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is GenericScheduleCommand genericCommand)
            {
                writer.WriteRawValue(genericCommand.JsonString);
            }
            else
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}
