namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZLLSwitchState : IGeneralSensorState
    {
        int? ButtonEvent { get; set; }
    }
}