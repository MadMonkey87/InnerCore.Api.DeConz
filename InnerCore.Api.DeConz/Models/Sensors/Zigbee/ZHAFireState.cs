namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAFireState : IGeneralSensorState
    {
        bool? Fire { get; set; }
    }
}
