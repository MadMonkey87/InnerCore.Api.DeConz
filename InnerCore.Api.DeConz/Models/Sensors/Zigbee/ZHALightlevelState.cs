namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHALightLevelState : IGeneralSensorState
    {
        /// <summary>
        /// Light level in lux
        /// </summary>
        long? Lux { get; set; }

        /// <summary>
        /// Light level in 10000 log10 (lux) +1 measured by sensor. Logarithm scale used because the human eye adjusts to light levels and small changes at low lux levels are more noticeable than at high lux levels.
        /// </summary>
        long? LightLevel { get; set; }

        /// <summary>
        /// lightlevel is at or above light threshold (dark+offset).
        /// </summary>
        bool? Daylight { get; set; }

    }
}