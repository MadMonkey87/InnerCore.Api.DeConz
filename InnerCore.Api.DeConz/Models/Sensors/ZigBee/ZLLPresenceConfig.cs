namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAPresenceConfig : IGeneralSensorConfig
    {
        int? Sensitivity { get; set; }

        int? SensitivityMax { get; set; }
    }
}