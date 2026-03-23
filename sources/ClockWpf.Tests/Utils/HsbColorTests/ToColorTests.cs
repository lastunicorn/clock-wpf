using System.Windows.Media;
using DustInTheWind.ClockWpf.Utils;


namespace DustInTheWind.ClockWpf.Tests.Utils.HsbColorTests;

public class ToColorTests
{
    [Fact]
    public void HavingBlackColor_WhenConvertingToColor_ThenReturnsBlackRgb()
    {
        HsbColor HsbColor = new(0, 0, 0);

        Color color = HsbColor.ToColor();

        Assert.Equal(255, color.A);
        Assert.Equal(0, color.R);
        Assert.Equal(0, color.G);
        Assert.Equal(0, color.B);
    }

    [Fact]
    public void HavingWhiteColor_WhenConvertingToColor_ThenReturnsWhiteRgb()
    {
        HsbColor HsbColor = new(0, 0, 100);

        Color color = HsbColor.ToColor();

        Assert.Equal(255, color.A);
        Assert.Equal(255, color.R);
        Assert.Equal(255, color.G);
        Assert.Equal(255, color.B);
    }

    [Fact]
    public void HavingRedColor_WhenConvertingToColor_ThenReturnsRedRgb()
    {
        HsbColor HsbColor = new(0, 100, 100);

        Color color = HsbColor.ToColor();

        Assert.Equal(255, color.A);
        Assert.Equal(255, color.R);
        Assert.Equal(0, color.G);
        Assert.Equal(0, color.B);
    }

    [Fact]
    public void HavingGreenColor_WhenConvertingToColor_ThenReturnsGreenRgb()
    {
        HsbColor HsbColor = new(120, 100, 100);

        Color color = HsbColor.ToColor();

        Assert.Equal(255, color.A);
        Assert.Equal(0, color.R);
        Assert.Equal(255, color.G);
        Assert.Equal(0, color.B);
    }

    [Fact]
    public void HavingBlueColor_WhenConvertingToColor_ThenReturnsBlueRgb()
    {
        HsbColor HsbColor = new(240, 100, 100);

        Color color = HsbColor.ToColor();

        Assert.Equal(255, color.A);
        Assert.Equal(0, color.R);
        Assert.Equal(0, color.G);
        Assert.Equal(255, color.B);
    }

    [Fact]
    public void HavingAlpha128_WhenConvertingToColor_ThenAlphaIsPreserved()
    {
        HsbColor HsbColor = new(128, 0, 0, 100);

        Color color = HsbColor.ToColor();

        Assert.Equal(128, color.A);
    }

    [Fact]
    public void HavingYellowColor_WhenConvertingToColor_ThenReturnsYellowRgb()
    {
        HsbColor HsbColor = new(60, 100, 100);

        Color color = HsbColor.ToColor();

        Assert.Equal(255, color.A);
        Assert.Equal(255, color.R);
        Assert.Equal(255, color.G);
        Assert.Equal(0, color.B);
    }

    [Fact]
    public void HavingCyanColor_WhenConvertingToColor_ThenReturnsCyanRgb()
    {
        HsbColor HsbColor = new(180, 100, 100);

        Color color = HsbColor.ToColor();

        Assert.Equal(255, color.A);
        Assert.Equal(0, color.R);
        Assert.Equal(255, color.G);
        Assert.Equal(255, color.B);
    }

    [Fact]
    public void HavingMagentaColor_WhenConvertingToColor_ThenReturnsMagentaRgb()
    {
        HsbColor HsbColor = new(300, 100, 100);

        Color color = HsbColor.ToColor();

        Assert.Equal(255, color.A);
        Assert.Equal(255, color.R);
        Assert.Equal(0, color.G);
        Assert.Equal(255, color.B);
    }

    [Fact]
    public void HavingGrayColor_WhenConvertingToColor_ThenReturnsGrayRgb()
    {
        HsbColor HsbColor = new(0, 0, 50.196f);

        Color color = HsbColor.ToColor();

        Assert.Equal(255, color.A);
        Assert.Equal(128, color.R);
        Assert.Equal(128, color.G);
        Assert.Equal(128, color.B);
    }
}
