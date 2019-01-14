namespace InnerCore.Api.DeConz.Models.Sensors.ZigBee
{
    public interface ZLLPresenceState : IGeneralSensorState
    {
        bool? Presence { get; set; }
    }
}