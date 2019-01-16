namespace InnerCore.Api.DeConz.Models.Sensors.CLIP
{
    public interface CLIPTemperatureState : IGeneralSensorState
    {
        /// <summary>
        /// Current temperature in 0.01 degrees Celsius. (3000 is 30.00 degree) Bridge does not verify the range of the value.
        /// </summary>
        int? Temperature { get; set; }
    }
}