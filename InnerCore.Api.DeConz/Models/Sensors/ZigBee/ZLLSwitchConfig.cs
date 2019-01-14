namespace InnerCore.Api.DeConz.Models.Sensors.ZigBee
{
    public interface ZLLSwitchConfig : IGeneralSensorConfig
    {
        int? Group { get; set; }
    }
}