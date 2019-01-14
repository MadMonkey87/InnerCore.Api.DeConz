namespace InnerCore.Api.DeConz.Models.Sensors.CLIP
{
    public interface CLIPGenericFlagState : IGeneralSensorState
    {
        bool? Flag { get; set; }
    }
}