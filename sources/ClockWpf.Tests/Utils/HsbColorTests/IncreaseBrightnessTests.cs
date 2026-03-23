using DustInTheWind.ClockWpf.Utils;


namespace DustInTheWind.ClockWpf.Tests.Utils.HsbColorTests;

public class IncreaseBrightnessTests
{
    [Fact]
    public void HavingBrightnessAt100_WhenIncreasing50Percent_ThenBrightnessIncreasesBy77Point5()
    {
        HsbColor HsbColor = new(150, 50, 40);

        HsbColor result = HsbColor.IncreaseBrighness(50);

        Assert.Equal(70f, result.Brightness);
    }

    [Fact]
    public void HavingBrightnessAt0_WhenIncreasing50Percent_ThenBrightnessIncreasesBy127Point5()
    {
        HsbColor HsbColor = new(150, 50, 0);

        HsbColor result = HsbColor.IncreaseBrighness(50);

        Assert.Equal(50f, result.Brightness);
    }

    [Fact]
    public void HavingBrightnessAt200_WhenIncreasing100Percent_ThenBrightnessIncreasesTo255()
    {
        HsbColor HsbColor = new(150, 50, 80);

        HsbColor result = HsbColor.IncreaseBrighness(100);

        Assert.Equal(100, result.Brightness);
    }

    [Fact]
    public void HavingBrightnessAt255_WhenIncreasing50Percent_ThenBrightnessRemainsAt255()
    {
        HsbColor HsbColor = new(150, 50, 100);

        HsbColor result = HsbColor.IncreaseBrighness(50);

        Assert.Equal(100, result.Brightness);
    }

    [Fact]
    public void HavingBrightnessAt100_WhenIncreasingWithPercentageThatExceedsMax_ThenBrightnessIsClampedTo255()
    {
        HsbColor HsbColor = new(150, 50, 40);

        HsbColor result = HsbColor.IncreaseBrighness(200);

        Assert.Equal(100, result.Brightness);
    }

    [Fact]
    public void HavingAlpha_WhenIncreasingBrightness_ThenAlphaIsPreserved()
    {
        HsbColor HsbColor = new(128, 150, 50, 40);

        HsbColor result = HsbColor.IncreaseBrighness(50);

        Assert.Equal(128, result.Alpha);
    }

    [Fact]
    public void HavingHue_WhenIncreasingBrightness_ThenHueIsPreserved()
    {
        HsbColor HsbColor = new(150, 50, 40);

        HsbColor result = HsbColor.IncreaseBrighness(50);

        Assert.Equal(150, result.Hue);
    }

    [Fact]
    public void HavingSaturation_WhenIncreasingBrightness_ThenSaturationIsPreserved()
    {
        HsbColor HsbColor = new(150, 50, 40);

        HsbColor result = HsbColor.IncreaseBrighness(50);

        Assert.Equal(50, result.Saturation);
    }
}
