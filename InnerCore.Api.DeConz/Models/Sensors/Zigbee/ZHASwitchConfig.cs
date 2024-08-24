namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHASwitchConfig : IGeneralSensorConfig
    {
        int? Group { get; set; }
    }
}
