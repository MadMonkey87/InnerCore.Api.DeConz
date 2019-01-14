namespace InnerCore.Api.DeConz.Models.Sensors.CLIP
{
    public interface CLIPSwitchState : IGeneralSensorState
    {
        int? ButtonEvent { get; set; }
    }
}