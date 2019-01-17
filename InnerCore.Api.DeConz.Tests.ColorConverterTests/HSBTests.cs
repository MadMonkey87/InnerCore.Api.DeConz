using InnerCore.Api.DeConz.ColorConverters.HSB;
using Xunit;

namespace InnerCore.Api.DeConz.Tests.ColorConverterTests
{
  public class HSBTests
  {
    [Fact]
    public void TestHSBContructor()
    {
      var hsb = new HSB(0, 0, 0);

      Assert.True(hsb.Hue == 0);

      hsb.Hue += 65535 + 10;

      Assert.True(hsb.Hue == 10);

    }

    [Fact]
    public void TestHSBOverflowContructor()
    {
      var hsb = new HSB(65535 + 1, 0, 0);

      Assert.True(hsb.Hue == 1);
    }

    [Fact]
    public void TestHSBNegativeContructor()
    {
      var hsb = new HSB(-1, 0, 0);

      Assert.True(hsb.Hue == HSB.HueMaxValue - 1);
    }
  }
}
