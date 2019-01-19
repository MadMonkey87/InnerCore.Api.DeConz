namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZLLPresenceConfig : IGeneralSensorConfig
    {
        int? Sensitivity { get; set; }

        int? SensitivityMax { get; set; }
    }
}