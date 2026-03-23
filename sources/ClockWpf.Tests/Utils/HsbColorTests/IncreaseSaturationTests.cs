using DustInTheWind.ClockWpf.Utils;


namespace DustInTheWind.ClockWpf.Tests.Utils.HsbColorTests;

public class IncreaseSaturationTests
{
    [Fact]
    public void HavingSaturationAt100_WhenIncreasing50Percent_ThenSaturationIncreasesBy77Point5()
    {
        HsbColor HsbColor = new(150, 40, 50);

        HsbColor result = HsbColor.IncreaseSaturation(50);

        Assert.Equal(70f, result.Saturation);
    }

    [Fact]
    public void HavingSaturationAt0_WhenIncreasing50Percent_ThenSaturationIncreasesBy127Point5()
    {
        HsbColor HsbColor = new(150, 0, 50);

        HsbColor result = HsbColor.IncreaseSaturation(50);

        Assert.Equal(50f, result.Saturation);
    }

    [Fact]
    public void HavingSaturationAt200_WhenIncreasing100Percent_ThenSaturationIncreasesTo255()
    {
        HsbColor HsbColor = new(150, 80, 50);

        HsbColor result = HsbColor.IncreaseSaturation(100);

        Assert.Equal(100, result.Saturation);
    }

    [Fact]
    public void HavingSaturationAt255_WhenIncreasing50Percent_ThenSaturationRemainsAt255()
    {
        HsbColor HsbColor = new(150, 100, 50);

        HsbColor result = HsbColor.IncreaseSaturation(50);

        Assert.Equal(100, result.Saturation);
    }

    [Fact]
    public void HavingSaturationAt100_WhenIncreasingWithPercentageThatExceedsMax_ThenSaturationIsClampedTo255()
    {
        HsbColor HsbColor = new(150, 40, 50);

        HsbColor result = HsbColor.IncreaseSaturation(200);

        Assert.Equal(100, result.Saturation);
    }

    [Fact]
    public void HavingAlpha_WhenIncreasingSaturation_ThenAlphaIsPreserved()
    {
        HsbColor HsbColor = new(128, 150, 40, 50);

        HsbColor result = HsbColor.IncreaseSaturation(50);

        Assert.Equal(128, result.Alpha);
    }

    [Fact]
    public void HavingHue_WhenIncreasingSaturation_ThenHueIsPreserved()
    {
        HsbColor HsbColor = new(150, 40, 50);

        HsbColor result = HsbColor.IncreaseSaturation(50);

        Assert.Equal(150, result.Hue);
    }

    [Fact]
    public void HavingBrightness_WhenIncreasingSaturation_ThenBrightnessIsPreserved()
    {
        HsbColor HsbColor = new(150, 40, 50);

        HsbColor result = HsbColor.IncreaseSaturation(50);

        Assert.Equal(50, result.Brightness);
    }
}
