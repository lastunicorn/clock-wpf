using System.Windows.Media;
using DustInTheWind.ClockWpf.Utils;


namespace DustInTheWind.ClockWpf.Tests.Utils.HsbColorTests;

public class FromColorTests
{
    [Fact]
    public void HavingBlackRgb_WhenConvertingFromColor_ThenReturnsBlackHsb()
    {
        Color color = Color.FromArgb(255, 0, 0, 0);

        HsbColor HsbColor = HsbColor.FromColor(color);

        Assert.Equal(255, HsbColor.Alpha);
        Assert.Equal(0, HsbColor.Hue);
        Assert.Equal(0, HsbColor.Saturation);
        Assert.Equal(0, HsbColor.Brightness);
    }

    [Fact]
    public void HavingWhiteRgb_WhenConvertingFromColor_ThenReturnsWhiteHsb()
    {
        Color color = Color.FromArgb(255, 255, 255, 255);

        HsbColor HsbColor = HsbColor.FromColor(color);

        Assert.Equal(255, HsbColor.Alpha);
        Assert.Equal(0, HsbColor.Hue);
        Assert.Equal(0, HsbColor.Saturation);
        Assert.Equal(100, HsbColor.Brightness);
    }

    [Fact]
    public void HavingRedRgb_WhenConvertingFromColor_ThenReturnsRedHsb()
    {
        Color color = Color.FromArgb(255, 255, 0, 0);

        HsbColor HsbColor = HsbColor.FromColor(color);

        Assert.Equal(255, HsbColor.Alpha);
        Assert.Equal(0, HsbColor.Hue);
        Assert.Equal(100, HsbColor.Saturation);
        Assert.Equal(100, HsbColor.Brightness);
    }

    [Fact]
    public void HavingGreenRgb_WhenConvertingFromColor_ThenReturnsGreenHsb()
    {
        Color color = Color.FromArgb(255, 0, 255, 0);

        HsbColor HsbColor = HsbColor.FromColor(color);

        Assert.Equal(255, HsbColor.Alpha);
        Assert.Equal(120, HsbColor.Hue);
        Assert.Equal(100, HsbColor.Saturation);
        Assert.Equal(100, HsbColor.Brightness);
    }

    [Fact]
    public void HavingBlueRgb_WhenConvertingFromColor_ThenReturnsBlueHsb()
    {
        Color color = Color.FromArgb(255, 0, 0, 255);

        HsbColor HsbColor = HsbColor.FromColor(color);

        Assert.Equal(255, HsbColor.Alpha);
        Assert.Equal(240, HsbColor.Hue);
        Assert.Equal(100, HsbColor.Saturation);
        Assert.Equal(100, HsbColor.Brightness);
    }

    [Fact]
    public void HavingAlpha128_WhenConvertingFromColor_ThenAlphaIsPreserved()
    {
        Color color = Color.FromArgb(128, 255, 255, 255);

        HsbColor HsbColor = HsbColor.FromColor(color);

        Assert.Equal(128, HsbColor.Alpha);
    }

    [Fact]
    public void HavingYellowRgb_WhenConvertingFromColor_ThenReturnsYellowHsb()
    {
        Color color = Color.FromArgb(255, 255, 255, 0);

        HsbColor HsbColor = HsbColor.FromColor(color);

        Assert.Equal(255, HsbColor.Alpha);
        Assert.Equal(60, HsbColor.Hue);
        Assert.Equal(100, HsbColor.Saturation);
        Assert.Equal(100, HsbColor.Brightness);
    }

    [Fact]
    public void HavingCyanRgb_WhenConvertingFromColor_ThenReturnsCyanHsb()
    {
        Color color = Color.FromArgb(255, 0, 255, 255);

        HsbColor HsbColor = HsbColor.FromColor(color);

        Assert.Equal(255, HsbColor.Alpha);
        Assert.Equal(180, HsbColor.Hue);
        Assert.Equal(100, HsbColor.Saturation);
        Assert.Equal(100, HsbColor.Brightness);
    }

    [Fact]
    public void HavingMagentaRgb_WhenConvertingFromColor_ThenReturnsMagentaHsb()
    {
        Color color = Color.FromArgb(255, 255, 0, 255);

        HsbColor HsbColor = HsbColor.FromColor(color);

        Assert.Equal(255, HsbColor.Alpha);
        Assert.Equal(300, HsbColor.Hue);
        Assert.Equal(100, HsbColor.Saturation);
        Assert.Equal(100, HsbColor.Brightness);
    }

    [Fact]
    public void HavingGrayRgb_WhenConvertingFromColor_ThenReturnsGrayHsb()
    {
        Color color = Color.FromArgb(255, 128, 128, 128);

        HsbColor HsbColor = HsbColor.FromColor(color);

        Assert.Equal(255, HsbColor.Alpha);
        Assert.Equal(0, HsbColor.Hue);
        Assert.Equal(0, HsbColor.Saturation);
        Assert.Equal(50, HsbColor.Brightness, 0);
    }
}
