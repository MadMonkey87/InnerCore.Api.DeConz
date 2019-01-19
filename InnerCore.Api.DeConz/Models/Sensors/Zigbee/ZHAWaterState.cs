namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAWaterState : IGeneralSensorState
    {
        bool? Water { get; set; }
    }
}
