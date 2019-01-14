namespace InnerCore.Api.deConz.Models.Sensors.ZigBee
{
    public interface ZLLPresenceState : IGeneralSensorState
    {
        bool? Presence { get; set; }
    }
}