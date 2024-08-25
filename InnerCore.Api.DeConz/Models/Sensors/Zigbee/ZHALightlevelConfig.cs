namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHALightLevelConfig : IGeneralSensorConfig
    {
        /// <summary>
        /// Threshold the user configured to be used in rules to determine insufficient lightlevel (ie below threshold). Default value 16000
        /// </summary>
        long? TholdDark { get; set; }

        /// <summary>
        /// Threshold the user configured to be used in rules to determine sufficient lightlevel (ie above threshold). Specified as relative offset to the “dark” threshold. Shall be >=1. Default value 7000
        /// </summary>
        long? TholdOffset { get; set; }
    }
}
