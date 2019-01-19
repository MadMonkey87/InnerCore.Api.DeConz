namespace InnerCore.Api.DeConz.Models.Sensors
{
    public interface DaylightSensorConfig : IGeneralSensorConfig
    {
        string Long { set; }

        string Lat { set; }

        bool? Configured { get; }

        int? SunriseOffset { get; set; }

        int? SunsetOffset { get; set; }
    }
}