using System;
using InnerCore.Api.DeConz.Models.Lights;
using InnerCore.Api.DeConz.Models.Scenes;
using InnerCore.Api.DeConz.Models.Schedule;
using Newtonsoft.Json;
using Xunit;

namespace InnerCore.Api.DeConz.Tests
{
    public class ScheduleGenericScheduleCommandTests
    {
        [Fact]
        public void CanConvertToSceneCommand()
        {
            SceneCommand sceneCommand = new SceneCommand();
            sceneCommand.Scene = "test123";

            var json = JsonConvert.SerializeObject(sceneCommand);

            GenericScheduleCommand genericCommand = new GenericScheduleCommand(json);

            Assert.True(genericCommand.IsSceneCommand());
            Assert.NotNull(genericCommand.AsSceneCommand());

            var scene = genericCommand.AsSceneCommand();
            Assert.Equal(sceneCommand.Scene, scene.Scene);
        }

        [Fact]
        public void CanConvertToLightCommand()
        {
            LightCommand lightCommand = new LightCommand();
            lightCommand.Alert = Alert.Multiple;
            lightCommand.On = true;

            var json = JsonConvert.SerializeObject(lightCommand);

            GenericScheduleCommand genericCommand = new GenericScheduleCommand(json);

            Assert.False(genericCommand.IsSceneCommand());
            Assert.NotNull(genericCommand.AsLightCommand());

            var light = genericCommand.AsLightCommand();
            Assert.Equal(lightCommand.Alert, light.Alert);
        }
    }
}
