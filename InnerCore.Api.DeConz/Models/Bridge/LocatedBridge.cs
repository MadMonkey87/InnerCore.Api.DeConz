namespace InnerCore.Api.DeConz.Models.Bridge
{
    public class LocatedBridge
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string InternalIpAddress { get; set; }

        public string PublicIpAddress { get; set; }

        public string MacAddress { get; set; }

        public int InternalPort { get; set; }
    }
}
