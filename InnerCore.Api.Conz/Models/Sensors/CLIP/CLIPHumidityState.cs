namespace InnerCore.Api.DeConz.Models.Sensors.CLIP
{
    public interface CLIPHumidityState : IGeneralSensorState
    {
        /// <summary>
        /// Current humidity 0.01% steps (e.g. 2000 is 20%)The bridge does not enforce range/resolution.
        /// </summary>
        int? Humidity { get; set; }
    }
}