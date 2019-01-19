namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZLLPressureState : IGeneralSensorState
    {
        /// <summary>
        /// Pressure measured in hPa
        /// </summary>
        int? Pressure { get; set; }
    }
}