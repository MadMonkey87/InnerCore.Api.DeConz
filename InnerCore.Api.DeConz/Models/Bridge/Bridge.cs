using InnerCore.Api.DeConz.Models.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using InnerCore.Api.DeConz.Models.Lights;

namespace InnerCore.Api.DeConz.Models.Bridge
{
    /// <summary>
    /// DeConz Bridge
    /// </summary>
    public class Bridge
    {
        internal Bridge(BridgeState bridge)
        {
            if (bridge == null)
                throw new ArgumentNullException(nameof(bridge));

            Config = bridge.Config;

            foreach (var light in bridge.Lights)
                light.Value.Id = light.Key;
            Lights = bridge.Lights.Select(l => l.Value).ToList();

            foreach (var group in bridge.Groups)
                group.Value.Id = group.Key;
            Groups = bridge.Groups.Select(l => l.Value).ToList();

            //Fix whitelist IDs
            foreach (var whitelist in bridge.Config.WhiteList)
                whitelist.Value.Id = whitelist.Key;
            WhiteList = bridge.Config.WhiteList.Select(l => l.Value).ToList();
        }

        /// <summary>
        /// Light info from the bridge
        /// </summary>
        public IEnumerable<Light> Lights { get; private set; }

        /// <summary>
        /// Group info from the bridge
        /// </summary>
        public IEnumerable<Group> Groups { get; private set; }

        /// <summary>
        /// Bridge config info
        /// </summary>
        public BridgeConfig Config { get; private set; }

        /// <summary>
        /// Light info from the bridge
        /// </summary>
        public IEnumerable<WhiteList> WhiteList { get; private set; }
    }
}
