﻿namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAVibrationConfig : IGeneralSensorConfig
    {
        int? Sensitivity { get; set; }

        int? SensitivityMax { get; set; }
    }
}
