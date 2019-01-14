namespace InnerCore.Api.deConz.Models.Sensors.CLIP
{
    public interface CLIPGenericStatusState : IGeneralSensorState
    {
        int? Status { get; set; }
    }
}