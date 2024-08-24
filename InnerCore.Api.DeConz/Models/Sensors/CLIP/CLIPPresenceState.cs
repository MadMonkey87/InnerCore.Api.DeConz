namespace InnerCore.Api.DeConz.Models.Sensors.CLIP
{
    public interface CLIPPresenceState : IGeneralSensorState
    {
        bool? Presence { get; set; }
    }
}
