using System;

namespace InnerCore.Api.DeConz.Models.Sensors
{
    public interface IGeneralSensorState
    {
        DateTime? Lastupdated { get; set; }
    }
}