namespace InnerCore.Api.deConz.Models.Sensors.ZigBee
{
    public interface ZLLSwitchConfig : IGeneralSensorConfig
    {
        int? Group { get; set; }
    }
}