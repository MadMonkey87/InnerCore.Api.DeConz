namespace InnerCore.Api.DeConz.Models.Sensors.CLIP
{
    public interface CLIPOpenCloseState : IGeneralSensorState
    {
        bool? Open { get; set; }
    }
}