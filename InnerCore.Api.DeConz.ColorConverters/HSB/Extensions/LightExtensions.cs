using InnerCore.Api.DeConz.Models.Lights;

namespace InnerCore.Api.DeConz.ColorConverters.HSB.Extensions
{
    public static class LightExtensions
    {
        public static string ToHex(this Light light)
        {
            return light.State.ToRgb().ToHex();
        }
    }
}
