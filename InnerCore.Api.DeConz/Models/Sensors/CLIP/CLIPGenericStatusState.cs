namespace InnerCore.Api.DeConz.Models.Sensors.CLIP
{
    public interface CLIPGenericStatusState : IGeneralSensorState
    {
        int? Status { get; set; }
    }
}
