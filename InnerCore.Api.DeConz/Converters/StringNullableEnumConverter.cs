using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InnerCore.Api.DeConz.Converters
{
    public class StringNullableEnumConverter : StringEnumConverter
    {
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer
        )
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (JsonSerializationException)
            {
                if (IsNullableType(objectType))
                    return null;
                else
                    throw;
            }
        }

        private bool IsNullableType(Type t)
        {
            return (
                t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>)
            );
        }
    }
}
