using System;
using InnerCore.Api.DeConz.ColorConverters;
using InnerCore.Api.DeConz.ColorConverters.HSB;
using InnerCore.Api.DeConz.ColorConverters.HSB.Extensions;
using InnerCore.Api.DeConz.Models;
using Xunit;

namespace InnerCore.Api.DeConz.Tests.ColorConverterTests
{
    public class StateExtensionsTests
    {
        [Fact]
        public void ColorConversionBlackPoint()
        {
            LightState state = new LightState()
            {
                Hue = 0,
                Brightness = 0,
                Saturation = 0
            };

            var rgb = state.ToRgb();

            Assert.Equal(0, rgb.R);
            Assert.Equal(0, rgb.G);
            Assert.Equal(0, rgb.B);
        }

        [Fact]
        public void ColorConversionWhitePoint()
        {
            string color = "FFFFFF";

            RGBColor rgbColor = new RGBColor(color);
            var hsb = rgbColor.GetHSB();
            LightState state = new LightState()
            {
                Hue = hsb.Hue,
                Brightness = (byte)hsb.Brightness,
                Saturation = hsb.Saturation
            };

            var rgb = state.ToRgb();

            Assert.Equal(1, rgb.R);
            Assert.Equal(1, rgb.G);
            Assert.Equal(1, rgb.B);
        }

        [Fact]
        public void ColorConversionRedPoint()
        {
            string color = "FF0000";

            RGBColor rgbColor = new RGBColor(color);
            var hsb = rgbColor.GetHSB();
            LightState state = new LightState()
            {
                Hue = hsb.Hue,
                Brightness = (byte)hsb.Brightness,
                Saturation = hsb.Saturation
            };

            var rgb = state.ToRgb();

            Assert.Equal(1, rgb.R);
            Assert.Equal(0, rgb.G);
            Assert.Equal(0, rgb.B);
        }

        [Fact]
        public void ColorConversionDarkSeaGreenPoint()
        {
            string color = "8FBC8B";

            RGBColor rgbColor = new RGBColor(color);
            var hsb = rgbColor.GetHSB();
            LightState state = new LightState()
            {
                Hue = hsb.Hue,
                Brightness = (byte)hsb.Brightness,
                Saturation = hsb.Saturation
            };

            var rgb = state.ToRgb();

            Assert.Equal(143d / 255d, rgb.R, 2);
            Assert.Equal(188d / 255d, rgb.G, 2);
            Assert.Equal(139d / 255d, rgb.B, 2);
        }
    }
}
