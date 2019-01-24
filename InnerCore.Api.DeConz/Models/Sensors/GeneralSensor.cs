namespace InnerCore.Api.DeConz.Models.Sensors
{
    public interface IGeneralSensor
    {
        string Id { get; set; }

        string Name { get; set; }

        SensorType? Type { get; set; }

        string ModelId { get; set; }

        string ManufacturerName { get; set; }

        string UniqueId { get; set; }

        string SwVersion { get; set; }
    }
}
