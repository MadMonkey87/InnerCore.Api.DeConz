using System;

namespace InnerCore.Api.deConz.Models.Lights
{
    /// <summary>
    /// Extension methods to compose a light command
    /// </summary>
    public static class LightCommandExtensions
    {
        /// <summary>
        /// Helper to set the color based on the light's built in XY color schema
        /// </summary>
        /// <param name="lightCommand"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static LightCommand SetColor(this LightCommand lightCommand, double x, double y)
        {
            if (lightCommand == null)
                throw new ArgumentNullException(nameof(lightCommand));

            lightCommand.ColorCoordinates = new[] { x, y };
            return lightCommand;
        }

        /// <summary>
        /// Helper to set the color based on the light's built in CT color scheme
        /// </summary>
        /// <param name="lightCommand"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static LightCommand SetColor(this LightCommand lightCommand, int ct)
        {
            if (lightCommand == null)
                throw new ArgumentNullException(nameof(lightCommand));

            lightCommand.ColorTemperature = ct;
            return lightCommand;
        }

        /// <summary>
        /// Helper to create turn on command
        /// </summary>
        /// <param name="lightCommand"></param>
        /// <returns></returns>
        public static LightCommand TurnOn(this LightCommand lightCommand)
        {
            if (lightCommand == null)
                throw new ArgumentNullException(nameof(lightCommand));

            lightCommand.On = true;
            return lightCommand;
        }

        /// <summary>
        /// Helper to create turn off command
        /// </summary>
        /// <param name="lightCommand"></param>
        /// <returns></returns>
        public static LightCommand TurnOff(this LightCommand lightCommand)
        {
            if (lightCommand == null)
                throw new ArgumentNullException(nameof(lightCommand));

            lightCommand.On = false;
            return lightCommand;
        }

    }
}