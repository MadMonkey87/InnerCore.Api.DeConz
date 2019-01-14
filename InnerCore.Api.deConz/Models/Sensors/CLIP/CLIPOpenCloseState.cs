namespace InnerCore.Api.deConz.Models.Sensors.CLIP
{
    public interface CLIPOpenCloseState : IGeneralSensorState
    {
        bool? Open { get; set; }
    }
}