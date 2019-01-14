using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace InnerCore.Api.deConz.Converters
{
    internal class HttpMethodConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(HttpMethod);
        }

        public override bool CanRead => true;

        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return null;
                case JsonToken.String:
                    string value = (string)reader.Value;
                    if (string.IsNullOrEmpty(value))
                        return null;

                    return new HttpMethod(value);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(((HttpMethod)value).Method);
        }
    }
}
