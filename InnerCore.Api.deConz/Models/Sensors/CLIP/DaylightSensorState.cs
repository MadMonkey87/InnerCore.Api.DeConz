namespace InnerCore.Api.deConz.Models.Sensors.CLIP
{
    public interface DaylightSensorState : IGeneralSensorState
    {
        bool? Daylight { get; set; }

    }
}