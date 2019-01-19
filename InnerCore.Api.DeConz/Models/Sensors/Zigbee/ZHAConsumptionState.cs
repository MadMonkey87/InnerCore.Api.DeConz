namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAConsumptionState : IGeneralSensorState
    {
        /// <summary>
        /// Consumption measured in Wh
        /// </summary>
        int? Consumption { get; set; }

        /// <summary>
        /// Consumption measured in W
        /// </summary>
        int? Power { get; set; }
    }
}