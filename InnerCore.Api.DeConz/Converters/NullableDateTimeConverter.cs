using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace InnerCore.Api.DeConz.Converters
{
    /// <summary>
    /// A super simple datetime converter that serializes "none" -> null
    /// Note: serializing won't work because of the NullValueHandling of Json.Net but isn't required for DeConz anyways.
    /// </summary>
    public class NullableDateTimeConverter : IsoDateTimeConverter
    {
        private const string None = "none";

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String &&
                reader.Value?.ToString().Equals(None, StringComparison.InvariantCultureIgnoreCase) == true)
            {
                return null;
            }

            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}
