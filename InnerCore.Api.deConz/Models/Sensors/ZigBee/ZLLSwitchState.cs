namespace InnerCore.Api.deConz.Models.Sensors.ZigBee
{
    public interface ZLLSwitchState : IGeneralSensorState
    {
        int? ButtonEvent { get; set; }
    }
}