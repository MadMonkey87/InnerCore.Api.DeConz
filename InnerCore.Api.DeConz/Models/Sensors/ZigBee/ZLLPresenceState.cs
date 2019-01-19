namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZLLPresenceState : IGeneralSensorState
    {
        bool? Presence { get; set; }
    }
}