namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAPressureState : IGeneralSensorState
    {
        /// <summary>
        /// Pressure measured in hPa
        /// </summary>
        int? Pressure { get; set; }
    }
}
