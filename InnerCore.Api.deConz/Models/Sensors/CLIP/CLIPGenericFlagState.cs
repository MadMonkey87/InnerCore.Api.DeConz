namespace InnerCore.Api.deConz.Models.Sensors.CLIP
{
    public interface CLIPGenericFlagState : IGeneralSensorState
    {
        bool? Flag { get; set; }
    }
}