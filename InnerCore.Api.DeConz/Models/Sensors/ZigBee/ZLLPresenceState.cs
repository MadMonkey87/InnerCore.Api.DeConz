namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAPresenceState : IGeneralSensorState
    {
        bool? Presence { get; set; }
    }
}