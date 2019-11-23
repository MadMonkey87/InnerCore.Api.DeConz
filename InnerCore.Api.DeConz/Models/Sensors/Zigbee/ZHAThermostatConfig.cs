namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAThermostatConfig : IGeneralSensorConfig
    {
        /// <summary>
        /// Target temperature in 0.01 degrees Celsius. (3000 is 30.00 degree)
        int? HeatSetPoint { get; set; }
    }
}