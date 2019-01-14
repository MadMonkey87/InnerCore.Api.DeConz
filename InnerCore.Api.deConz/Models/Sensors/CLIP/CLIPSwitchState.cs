namespace InnerCore.Api.deConz.Models.Sensors.CLIP
{
    public interface CLIPSwitchState : IGeneralSensorState
    {
        int? ButtonEvent { get; set; }
    }
}