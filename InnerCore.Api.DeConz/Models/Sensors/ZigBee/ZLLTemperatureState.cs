namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZLLTemperatureState : IGeneralSensorState
    {
        int? Temperature { get; set; }
    }
}