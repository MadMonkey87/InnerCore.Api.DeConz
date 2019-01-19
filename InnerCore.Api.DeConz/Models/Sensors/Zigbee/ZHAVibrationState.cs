namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAVibrationState : IGeneralSensorState
    {
        bool? Vibration { get; set; }
    }
}