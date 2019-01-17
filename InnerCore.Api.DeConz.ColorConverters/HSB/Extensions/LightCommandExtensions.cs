using System;
using InnerCore.Api.DeConz.Models.Lights;

namespace InnerCore.Api.DeConz.ColorConverters.HSB.Extensions
{
    public static class LightCommandExtensions
    {
        public static LightCommand SetColor(this LightCommand lightCommand, RGBColor color)
        {
            if (lightCommand == null)
                throw new ArgumentNullException(nameof(lightCommand));

            var hsb = color.GetHSB();
            lightCommand.Brightness = (byte)hsb.Brightness;
            lightCommand.Hue = hsb.Hue;
            lightCommand.Saturation = hsb.Saturation;

            return lightCommand;
        }
    }
}
