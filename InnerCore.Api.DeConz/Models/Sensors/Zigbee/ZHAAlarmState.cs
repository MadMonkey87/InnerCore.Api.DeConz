namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAAlarmState : IGeneralSensorState
    {
        bool? Alarm { get; set; }
    }
}
