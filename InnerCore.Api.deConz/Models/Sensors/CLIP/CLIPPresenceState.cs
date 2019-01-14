namespace InnerCore.Api.deConz.Models.Sensors.CLIP
{
    public interface CLIPPresenceState : IGeneralSensorState
    {
        bool? Presence { get; set; }
    }
}