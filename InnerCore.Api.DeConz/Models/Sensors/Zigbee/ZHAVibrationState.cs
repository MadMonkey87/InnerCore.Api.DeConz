using System.Collections.Generic;

namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHAVibrationState : IGeneralSensorState
    {
        bool? Vibration { get; set; }

        int? VibrationStrength { get; set; }

        int? TiltAngle { get; set; }

        IEnumerable<int> Orientation { get; set; }
    }
}
