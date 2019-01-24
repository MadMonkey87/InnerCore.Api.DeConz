using InnerCore.Api.DeConz.Models.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using InnerCore.Api.DeConz.Models.Lights;
using InnerCore.Api.DeConz.Models.Sensors;
using InnerCore.Api.DeConz.Models.Rules;

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
            {
                light.Value.Id = light.Key;
            }
            Lights = bridge.Lights.Select(l => l.Value).ToList();

            foreach (var sensor in bridge.Sensors)
            {
                sensor.Value.Id = sensor.Key;
            }
            Sensors = bridge.Sensors.Select(s => s.Value).ToList();

            foreach (var group in bridge.Groups)
            {
                group.Value.Id = group.Key;
            }
            Groups = bridge.Groups.Select(l => l.Value).ToList();






            foreach (var scene in bridge.Scenes)
            {
                scene.Value.Id = scene.Key;
            }
            Scenes = bridge.Scenes.Select(s => s.Value).ToList();

            foreach (var rule in bridge.Rules)
            {
                rule.Value.Id = rule.Key;
            }
            Rules = bridge.Rules.Select(r => r.Value).ToList();

            foreach (var schedule in bridge.Schedules)
            {
                schedule.Value.Id = schedule.Key;
            }
            Schedules = bridge.Schedules.Select(s => s.Value).ToList();








            foreach (var whitelist in bridge.Config.WhiteList)
            {
                whitelist.Value.Id = whitelist.Key;
            }
            WhiteList = bridge.Config.WhiteList.Select(l => l.Value).ToList();
        }

        /// <summary>
        /// Light info from the bridge
        /// </summary>
        public IEnumerable<Light> Lights { get; private set; }

        /// <summary>
        /// Sensor info from the bridge
        /// </summary>
        public IEnumerable<Sensor> Sensors { get; private set; }

        /// <summary>
        /// Group info from the bridge
        /// </summary>
        public IEnumerable<Group> Groups { get; private set; }

        /// <summary>
        /// Bridge config info
        /// </summary>
        public BridgeConfig Config { get; private set; }

        /// <summary>
        /// Schedule info from the bridge
        /// </summary>
        public IEnumerable<Schedule.Schedule> Schedules { get; private set; }

        /// <summary>
        /// Rule info from the bridge
        /// </summary>
        public IEnumerable<Rule> Rules { get; private set; }

        /// <summary>
        /// Scene info from the bridge
        /// </summary>
        public IEnumerable<Scenes.Scene> Scenes { get; private set; }

        /// <summary>
        /// Light info from the bridge
        /// </summary>
        public IEnumerable<WhiteList> WhiteList { get; private set; }
    }
}
