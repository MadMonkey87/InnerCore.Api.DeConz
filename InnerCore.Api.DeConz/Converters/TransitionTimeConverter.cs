using System;
using Newtonsoft.Json;

namespace InnerCore.Api.DeConz.Converters
{
    internal class TransitionTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan?) || objectType == typeof(TimeSpan);
        }

        public override bool CanRead => true;

        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Integer)
            {
                long value = (long)reader.Value;
                return (TimeSpan?)TimeSpan.FromMilliseconds(value * 100);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            TimeSpan span;

            if (value == null)
            {
                writer.WriteValue(0);
                return;
            }

            if (value is TimeSpan?)
                span = ((TimeSpan?)value).Value;
            else
                span = (TimeSpan)value;

            writer.WriteValue((int?)(span.TotalSeconds * 10));
        }
    }
}
