namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHASwitchState : IGeneralSensorState
    {
        int? ButtonEvent { get; set; }

        int? Gesture { get; set; }
    }
}
