namespace InnerCore.Api.DeConz.Models.Sensors.ZigBee
{
    public interface ZLLTemperatureState : IGeneralSensorState
    {
        int? Temperature { get; set; }
    }
}