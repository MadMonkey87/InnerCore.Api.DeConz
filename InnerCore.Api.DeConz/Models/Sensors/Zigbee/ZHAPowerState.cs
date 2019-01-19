namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAPowerState : IGeneralSensorState
    {
        /// <summary>
        /// Consumption measured in W
        /// </summary>
        int? Power { get; set; }

        /// <summary>
        /// Voltage measured in V
        /// </summary>
        int? Voltage { get; set; }

        /// <summary>
        /// Current measured in mA
        /// </summary>
        int? Current { get; set; }
    }
}
