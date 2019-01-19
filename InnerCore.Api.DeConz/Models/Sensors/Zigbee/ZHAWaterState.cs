namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAWater : IGeneralSensorState
    {
        bool? Water { get; set; }
    }
}
