using DustInTheWind.ClockWpf.Utils;


namespace DustInTheWind.ClockWpf.Tests.Utils.HsbColorTests;

public class DecreaseHueTests
{
    [Fact]
    public void HavingHueAt100_WhenDecreasing50Percent_ThenHueDecreasesBy50()
    {
        HsbColor HsbColor = new(100, 150, 200);

        HsbColor result = HsbColor.DecreaseHue(50);

        Assert.Equal(50, result.Hue);
    }

    [Fact]
    public void HavingHueAt200_WhenDecreasing50Percent_ThenHueDecreasesBy100()
    {
        HsbColor HsbColor = new(200, 150, 200);

        HsbColor result = HsbColor.DecreaseHue(50);

        Assert.Equal(100, result.Hue);
    }

    [Fact]
    public void HavingHueAt100_WhenDecreasing100Percent_ThenHueDecreasesTo0()
    {
        HsbColor HsbColor = new(100, 150, 200);

        HsbColor result = HsbColor.DecreaseHue(100);

        Assert.Equal(0, result.Hue);
    }

    [Fact]
    public void HavingHueAt0_WhenDecreasing50Percent_ThenHueRemainsAt0()
    {
        HsbColor HsbColor = new(0, 150, 200);

        HsbColor result = HsbColor.DecreaseHue(50);

        Assert.Equal(0, result.Hue);
    }

    [Fact]
    public void HavingHueAt100_WhenDecreasingWithPercentageThatGoesBelowZero_ThenHueIsClampedTo0()
    {
        HsbColor HsbColor = new(100, 150, 200);

        HsbColor result = HsbColor.DecreaseHue(200);

        Assert.Equal(0, result.Hue);
    }

    [Fact]
    public void HavingAlpha_WhenDecreasingHue_ThenAlphaIsPreserved()
    {
        HsbColor HsbColor = new(128, 100, 40, 50);

        HsbColor result = HsbColor.DecreaseHue(50);

        Assert.Equal(128, result.Alpha);
    }

    [Fact]
    public void HavingSaturation_WhenDecreasingHue_ThenSaturationIsPreserved()
    {
        HsbColor HsbColor = new(100, 40, 50);

        HsbColor result = HsbColor.DecreaseHue(50);

        Assert.Equal(40, result.Saturation);
    }

    [Fact]
    public void HavingBrightness_WhenDecreasingHue_ThenBrightnessIsPreserved()
    {
        HsbColor HsbColor = new(100, 40, 50);

        HsbColor result = HsbColor.DecreaseHue(50);

        Assert.Equal(50, result.Brightness);
    }
}
