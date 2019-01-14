namespace InnerCore.Api.deConz.Models.Sensors.CLIP
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