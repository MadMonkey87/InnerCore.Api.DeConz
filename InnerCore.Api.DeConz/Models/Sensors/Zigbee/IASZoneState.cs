namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface IASZoneState : IGeneralSensorState
    {
        bool? LowBattery { get; set; }

        bool? Tampered { get; set; }
    }
}
