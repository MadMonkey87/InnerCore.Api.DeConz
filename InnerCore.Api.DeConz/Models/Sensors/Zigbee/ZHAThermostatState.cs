namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAThermostatState : IGeneralSensorState
    {
        /// <summary>
        /// Current temperature in 0.01 degrees Celsius. (3000 is 30.00 degree) Bridge does not verify the range of the value.
        /// </summary>
        int? Temperature { get; set; }

        /// <summary>
        /// Heating on/off
        /// </summary>
        bool? On { get; set; }
    }
}
