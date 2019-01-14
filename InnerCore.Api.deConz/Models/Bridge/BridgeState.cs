using System.Collections.Generic;
using InnerCore.Api.DeConz.Models.Groups;
using InnerCore.Api.DeConz.Models.Lights;

namespace InnerCore.Api.DeConz.Models.Bridge
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