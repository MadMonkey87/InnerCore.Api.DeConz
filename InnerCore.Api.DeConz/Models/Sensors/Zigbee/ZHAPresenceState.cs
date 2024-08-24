namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAPresenceState : IGeneralSensorState
    {
        bool? Presence { get; set; }

        /// <summary>
        /// lightlevel is at or below given dark threshold
        /// </summary>
        bool? Dark { get; set; }
    }
}
