namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZGPSwitchConfig : IGeneralSensorConfig
    {
        int? Group { get; set; }
    }
}
