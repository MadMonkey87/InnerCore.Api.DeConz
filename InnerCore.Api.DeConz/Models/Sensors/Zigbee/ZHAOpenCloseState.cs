namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAOpenCloseState : IGeneralSensorState
    {
        bool? Open { get; set; }
    }
}
