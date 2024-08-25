using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Sensors
{
    public enum SensorType
    {
        [EnumMember(Value = "ZGPSwitch")]
        ZGPSwitch,

        [EnumMember(Value = "ZHAAlarm")]
        ZHAAlarm,

        [EnumMember(Value = "ZHACarbonMonoxide")]
        ZHACarbonMonoxide,

        [EnumMember(Value = "ZHAConsumption")]
        ZHAConsumption,

        [EnumMember(Value = "ZHAFire")]
        ZHAFire,

        [EnumMember(Value = "ZHAHumidity")]
        ZHAHumidity,

        [EnumMember(Value = "ZHALightLevel")]
        ZHALightLevel,

        [EnumMember(Value = "ZHAOpenClose")]
        ZHAOpenClose,

        [EnumMember(Value = "ZHAPower")]
        ZHAPower,

        [EnumMember(Value = "ZHAPresence")]
        ZHAPresence,

        [EnumMember(Value = "ZHAPressure")]
        ZHAPressure,

        [EnumMember(Value = "ZHASwitch")]
        ZHASwitch,

        [EnumMember(Value = "ZHATemperature")]
        ZHATemperature,

        [EnumMember(Value = "ZHAVibration")]
        ZHAVibration,

        [EnumMember(Value = "ZHAWater")]
        ZHAWater,

        [EnumMember(Value = "CLIPGenericFlag")]
        CLIPGenericFlag,

        [EnumMember(Value = "CLIPGenericStatus")]
        CLIPGenericStatus,

        [EnumMember(Value = "CLIPPresence")]
        CLIPPresence,

        [EnumMember(Value = "CLIPSwitch")]
        CLIPSwitch,
    }
}
