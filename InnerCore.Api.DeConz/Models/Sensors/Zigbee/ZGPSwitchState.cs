namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZGPSwitchState : IGeneralSensorState
    {
        int? ButtonEvent { get; set; }
    }
}