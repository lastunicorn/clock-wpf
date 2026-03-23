using DustInTheWind.ClockWpf.Utils;


namespace DustInTheWind.ClockWpf.Tests.Utils.HsbColorTests;

public class DecreaseBrightnessTests
{
    [Fact]
    public void HavingBrightnessAt100_WhenDecreasing50Percent_ThenBrightnessDecreasesBy50()
    {
        HsbColor HsbColor = new(150, 200, 100);

        HsbColor result = HsbColor.DecreaseBrighness(50);

        Assert.Equal(50, result.Brightness);
    }

    [Fact]
    public void HavingBrightnessAt200_WhenDecreasing50Percent_ThenBrightnessDecreasesBy100()
    {
        HsbColor HsbColor = new(150, 50, 80);

        HsbColor result = HsbColor.DecreaseBrighness(50);

        Assert.Equal(40, result.Brightness);
    }

    [Fact]
    public void HavingBrightnessAt100_WhenDecreasing100Percent_ThenBrightnessDecreasesTo0()
    {
        HsbColor HsbColor = new(150, 200, 100);

        HsbColor result = HsbColor.DecreaseBrighness(100);

        Assert.Equal(0, result.Brightness);
    }

    [Fact]
    public void HavingBrightnessAt0_WhenDecreasing50Percent_ThenBrightnessRemainsAt0()
    {
        HsbColor HsbColor = new(150, 200, 0);

        HsbColor result = HsbColor.DecreaseBrighness(50);

        Assert.Equal(0, result.Brightness);
    }

    [Fact]
    public void HavingBrightnessAt100_WhenDecreasingWithPercentageThatGoesBelowZero_ThenBrightnessIsClampedTo0()
    {
        HsbColor HsbColor = new(150, 200, 100);

        HsbColor result = HsbColor.DecreaseBrighness(200);

        Assert.Equal(0, result.Brightness);
    }

    [Fact]
    public void HavingAlpha_WhenDecreasingBrightness_ThenAlphaIsPreserved()
    {
        HsbColor HsbColor = new(128, 150, 50, 100);

        HsbColor result = HsbColor.DecreaseBrighness(50);

        Assert.Equal(128, result.Alpha);
    }

    [Fact]
    public void HavingHue_WhenDecreasingBrightness_ThenHueIsPreserved()
    {
        HsbColor HsbColor = new(150, 200, 100);

        HsbColor result = HsbColor.DecreaseBrighness(50);

        Assert.Equal(150, result.Hue);
    }

    [Fact]
    public void HavingSaturation_WhenDecreasingBrightness_ThenSaturationIsPreserved()
    {
        HsbColor HsbColor = new(150, 50, 100);

        HsbColor result = HsbColor.DecreaseBrighness(50);

        Assert.Equal(50, result.Saturation);
    }
}
