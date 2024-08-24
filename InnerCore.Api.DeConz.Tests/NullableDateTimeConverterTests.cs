using System;
using System.Runtime.Serialization;
using InnerCore.Api.DeConz.Converters;
using Newtonsoft.Json;
using Xunit;

namespace InnerCore.Api.DeConz.Tests
{
    public class NullableDateTimeConverterTests
    {
        [Fact]
        public void Handle_Regular_ISO8601_Value_Test()
        {
            string timeValue = "\"2014-09-20T19:35:26\"";
            string jsonString = "{\"value\":" + timeValue + "}";

            var testSubject = JsonConvert.DeserializeObject<TestSubject>(jsonString);

            Assert.NotNull(testSubject);
            Assert.NotNull(testSubject.Value);
            Assert.Equal(new DateTime(2014, 9, 20, 19, 35, 26), testSubject.Value);

            string result = JsonConvert.SerializeObject(
                testSubject,
                new NullableDateTimeConverter()
            );
            Assert.NotNull(result);
            Assert.Equal(jsonString, result);
        }

        [Fact]
        public void Handle_Custom_None_Value_Test()
        {
            string timeValue = "\"none\"";
            string jsonString = "{\"value\":" + timeValue + "}";

            var testSubject = JsonConvert.DeserializeObject<TestSubject>(jsonString);

            Assert.NotNull(testSubject);
            Assert.Null(testSubject.Value);

            string result = JsonConvert.SerializeObject(
                testSubject,
                new NullableDateTimeConverter()
            );
            Assert.NotNull(result);
            Assert.Equal("{\"value\":null}", result);
        }
    }

    [DataContract]
    public class TestSubject
    {
        [DataMember(Name = "value")]
        [JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? Value { get; set; }
    }
}
