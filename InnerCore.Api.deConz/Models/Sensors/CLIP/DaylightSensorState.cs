namespace InnerCore.Api.DeConz.Models.Sensors.CLIP
{
    public interface DaylightSensorState : IGeneralSensorState
    {
        bool? Daylight { get; set; }

    }
}