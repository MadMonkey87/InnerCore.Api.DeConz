namespace InnerCore.Api.deConz.Models.Sensors
{
    public interface IGeneralSensor
    {
        string Id { get; set; }

        string Name { get; set; }

        string Type { get; set; }

        string ModelId { get; set; }

        string ManufacturerName { get; set; }

        string UniqueId { get; set; }

        string SwVersion { get; set; }
    }
}
