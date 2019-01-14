namespace InnerCore.Api.DeConz.Models.Sensors.ZigBee
{
    public interface ZGPSwitchState : IGeneralSensorState
    {
        int? ButtonEvent { get; set; }
    }
}