using System.Collections.Generic;
using InnerCore.Api.DeConz.Models.Groups;
using InnerCore.Api.DeConz.Models.Lights;
using InnerCore.Api.DeConz.Models.Rules;
using InnerCore.Api.DeConz.Models.Sensors;

namespace InnerCore.Api.DeConz.Models.Bridge
{
    /// <summary>
    /// Status data returned from the bridge
    /// </summary>
    internal class BridgeState
    {
        public Dictionary<string, Light> Lights { get; set; }

        public Dictionary<string, Sensor> Sensors { get; set; }

        public Dictionary<string, Group> Groups { get; set; }

        public BridgeConfig Config { get; set; }

        public Dictionary<string, Schedule.Schedule> Schedules { get; set; }

        public Dictionary<string, Scenes.Scene> Scenes { get; set; }

        public Dictionary<string, Rule> Rules { get; set; }

        public Dictionary<string, WhiteList> Whitelist { get; set; }
    }
}
