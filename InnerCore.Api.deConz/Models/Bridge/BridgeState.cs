using System.Collections.Generic;
using InnerCore.Api.deConz.Models.Groups;
using InnerCore.Api.deConz.Models.Lights;

namespace InnerCore.Api.deConz.Models.Bridge
{
    /// <summary>
    /// Status data returned from the bridge
    /// </summary>
    internal class BridgeState
    {
        public Dictionary<string, Light> Lights { get; set; }

        public Dictionary<string, Group> Groups { get; set; }

        public BridgeConfig Config { get; set; }

        public Dictionary<string, WhiteList> Whitelist { get; set; }
    }
}