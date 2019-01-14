namespace InnerCore.Api.deConz.Models.Sensors.ZigBee
{
    public interface ZLLTemperatureState : IGeneralSensorState
    {
        int? Temperature { get; set; }
    }
}