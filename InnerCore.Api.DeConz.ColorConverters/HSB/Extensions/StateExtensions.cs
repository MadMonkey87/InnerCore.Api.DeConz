using InnerCore.Api.DeConz.Models;

namespace InnerCore.Api.DeConz.ColorConverters.HSB.Extensions
{
    public static class StateExtensions
    {
        public static RGBColor ToRgb(this State state)
        {
            HSB hsb = new HSB(state.Hue ?? 0, state.Saturation ?? 0, state.Brightness);
            return hsb.GetRGB();
        }

        public static string ToHex(this State state)
        {
            return state.ToRgb().ToHex();
        }
    }
}
