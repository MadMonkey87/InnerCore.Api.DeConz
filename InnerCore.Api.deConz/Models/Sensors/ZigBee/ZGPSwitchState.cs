namespace InnerCore.Api.deConz.Models.Sensors.ZigBee
{
    public interface ZGPSwitchState : IGeneralSensorState
    {
        int? ButtonEvent { get; set; }
    }
}