using System.Windows.Media;
using DustInTheWind.ClockWpf.Utils;


namespace DustInTheWind.ClockWpf.Tests.Utils.HsbColorTests;

public class ImplicitOperatorTests
{
    [Fact]
    public void HavingHsbColor_WhenImplicitlyConvertingToColor_ThenReturnsCorrectColor()
    {
        HsbColor HsbColor = new(0, 100, 100);

        Color color = HsbColor;

        Assert.Equal(255, color.A);
        Assert.Equal(255, color.R);
        Assert.Equal(0, color.G);
        Assert.Equal(0, color.B);
    }

    [Fact]
    public void HavingColor_WhenImplicitlyConvertingToHsbColor_ThenReturnsCorrectHsbColor()
    {
        Color color = Color.FromArgb(255, 255, 0, 0);

        HsbColor HsbColor = color;

        Assert.Equal(255, HsbColor.Alpha);
        Assert.Equal(0, HsbColor.Hue);
        Assert.Equal(100, HsbColor.Saturation);
        Assert.Equal(100, HsbColor.Brightness);
    }

    [Fact]
    public void HavingHsbColorWithAlpha_WhenImplicitlyConvertingToColor_ThenAlphaIsPreserved()
    {
        HsbColor HsbColor = new(128, 0, 100, 100);

        Color color = HsbColor;

        Assert.Equal(128, color.A);
    }

    [Fact]
    public void HavingColorWithAlpha_WhenImplicitlyConvertingToHsbColor_ThenAlphaIsPreserved()
    {
        Color color = Color.FromArgb(128, 255, 0, 0);

        HsbColor HsbColor = color;

        Assert.Equal(128, HsbColor.Alpha);
    }
}
