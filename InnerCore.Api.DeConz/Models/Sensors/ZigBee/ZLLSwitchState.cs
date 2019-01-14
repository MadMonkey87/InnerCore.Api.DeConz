namespace InnerCore.Api.DeConz.Models.Sensors.ZigBee
{
    public interface ZLLSwitchState : IGeneralSensorState
    {
        int? ButtonEvent { get; set; }
    }
}