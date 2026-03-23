using DustInTheWind.ClockWpf.Utils;


namespace DustInTheWind.ClockWpf.Tests.Utils.HsbColorTests;

public class IncreaseHueTests
{
    [Fact]
    public void HavingHueAt100_WhenIncreasing50Percent_ThenHueIncreasesBy77Point5()
    {
        HsbColor HsbColor = new(100, 40, 50);

        HsbColor result = HsbColor.IncreaseHue(50);

        Assert.Equal(230f, result.Hue);
    }

    [Fact]
    public void HavingHueAt0_WhenIncreasing50Percent_ThenHueIncreasesBy127Point5()
    {
        HsbColor HsbColor = new(0, 40, 50);

        HsbColor result = HsbColor.IncreaseHue(50);

        Assert.Equal(180f, result.Hue);
    }

    [Fact]
    public void HavingHueAt200_WhenIncreasing100Percent_ThenHueIncreasesTo255()
    {
        HsbColor HsbColor = new(200, 40, 50);

        HsbColor result = HsbColor.IncreaseHue(100);

        Assert.Equal(360, result.Hue);
    }

    [Fact]
    public void HavingHueAt255_WhenIncreasing50Percent_ThenHueRemainsAt255()
    {
        HsbColor HsbColor = new(360, 40, 50);

        HsbColor result = HsbColor.IncreaseHue(50);

        Assert.Equal(360, result.Hue);
    }

    [Fact]
    public void HavingHueAt100_WhenIncreasingWithPercentageThatExceedsMax_ThenHueIsClampedTo255()
    {
        HsbColor HsbColor = new(100, 40, 50);

        HsbColor result = HsbColor.IncreaseHue(200);

        Assert.Equal(360, result.Hue);
    }

    [Fact]
    public void HavingAlpha_WhenIncreasingHue_ThenAlphaIsPreserved()
    {
        HsbColor HsbColor = new(128, 100, 40, 50);

        HsbColor result = HsbColor.IncreaseHue(50);

        Assert.Equal(128, result.Alpha);
    }

    [Fact]
    public void HavingSaturation_WhenIncreasingHue_ThenSaturationIsPreserved()
    {
        HsbColor HsbColor = new(100, 40, 50);

        HsbColor result = HsbColor.IncreaseHue(50);

        Assert.Equal(40, result.Saturation);
    }

    [Fact]
    public void HavingBrightness_WhenIncreasingHue_ThenBrightnessIsPreserved()
    {
        HsbColor HsbColor = new(100, 40, 50);

        HsbColor result = HsbColor.IncreaseHue(50);

        Assert.Equal(50, result.Brightness);
    }
}
