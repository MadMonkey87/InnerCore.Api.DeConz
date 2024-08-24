using System;
using InnerCore.Api.DeConz.Models.Sensors;

namespace InnerCore.Api.DeConz.Models.WebSocket
{
    public class SensorChangedEvent : EventArgs
    {
        public string Id { get; set; }

        public SensorState State { get; set; }

        public SensorConfig Config { get; set; }
    }
}
