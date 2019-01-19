namespace InnerCore.Api.DeConz.Models.Sensors.Zigbee
{
    public interface ZHACarbonMonoxideState : IGeneralSensorState
    {
        bool? CarbonMonoxide { get; set; }
    }
}
