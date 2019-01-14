namespace InnerCore.Api.deConz.Models.Sensors.ZigBee
{
    public interface ZLLPresenceConfig : IGeneralSensorConfig
    {
        int? Sensitivity { get; set; }

        int? SensitivityMax { get; set; }
    }
}