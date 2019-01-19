namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZLLSwitchConfig : IGeneralSensorConfig
    {
        int? Group { get; set; }
    }
}