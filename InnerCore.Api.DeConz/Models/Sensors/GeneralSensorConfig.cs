using System.Collections.Generic;

namespace InnerCore.Api.DeConz.Models.Sensors
{
    public interface IGeneralSensorConfig
    {
        bool? On { get; set; }

        bool? Reachable { get; set; }

        int? Battery { get; set; }

        string Alert { get; set; }

        bool? Usertest { get; set; }

        string Url { get; set; }

        List<string> Pending { get; set; }

        bool? LedIndication { get; set; }
    }
}