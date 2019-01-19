namespace InnerCore.Api.DeConz.Models.Sensors
{
    public interface DaylightSensorState : IGeneralSensorState
    {
        bool? Daylight { get; set; }
    }
}