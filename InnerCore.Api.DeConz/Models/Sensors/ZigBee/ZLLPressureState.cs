namespace InnerCore.Api.DeConz.Models.Sensors.ZigBee
{
    public interface ZLLPressureState : IGeneralSensorState
    {
        /// <summary>
        /// Pressure measured in hPa
        /// </summary>
        int? Pressure { get; set; }
    }
}