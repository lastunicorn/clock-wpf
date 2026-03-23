using DustInTheWind.ClockWpf.Utils;


namespace DustInTheWind.ClockWpf.Tests.Utils.HsbColorTests;

public class DecreaseSaturationTests
{
    [Fact]
    public void HavingSaturationAt100_WhenDecreasing50Percent_ThenSaturationDecreasesBy50()
    {
        HsbColor HsbColor = new(150, 100, 200);

        HsbColor result = HsbColor.DecreaseSaturation(50);

        Assert.Equal(50, result.Saturation);
    }

    [Fact]
    public void HavingSaturationAt200_WhenDecreasing50Percent_ThenSaturationDecreasesBy100()
    {
        HsbColor HsbColor = new(150, 80, 50);

        HsbColor result = HsbColor.DecreaseSaturation(50);

        Assert.Equal(40, result.Saturation);
    }

    [Fact]
    public void HavingSaturationAt100_WhenDecreasing100Percent_ThenSaturationDecreasesTo0()
    {
        HsbColor HsbColor = new(150, 100, 200);

        HsbColor result = HsbColor.DecreaseSaturation(100);

        Assert.Equal(0, result.Saturation);
    }

    [Fact]
    public void HavingSaturationAt0_WhenDecreasing50Percent_ThenSaturationRemainsAt0()
    {
        HsbColor HsbColor = new(150, 0, 200);

        HsbColor result = HsbColor.DecreaseSaturation(50);

        Assert.Equal(0, result.Saturation);
    }

    [Fact]
    public void HavingSaturationAt100_WhenDecreasingWithPercentageThatGoesBelowZero_ThenSaturationIsClampedTo0()
    {
        HsbColor HsbColor = new(150, 100, 200);

        HsbColor result = HsbColor.DecreaseSaturation(200);

        Assert.Equal(0, result.Saturation);
    }

    [Fact]
    public void HavingAlpha_WhenDecreasingSaturation_ThenAlphaIsPreserved()
    {
        HsbColor HsbColor = new(128, 150, 100, 50);

        HsbColor result = HsbColor.DecreaseSaturation(50);

        Assert.Equal(128, result.Alpha);
    }

    [Fact]
    public void HavingHue_WhenDecreasingSaturation_ThenHueIsPreserved()
    {
        HsbColor HsbColor = new(150, 100, 200);

        HsbColor result = HsbColor.DecreaseSaturation(50);

        Assert.Equal(150, result.Hue);
    }

    [Fact]
    public void HavingBrightness_WhenDecreasingSaturation_ThenBrightnessIsPreserved()
    {
        HsbColor HsbColor = new(150, 100, 50);

        HsbColor result = HsbColor.DecreaseSaturation(50);

        Assert.Equal(50, result.Brightness);
    }
}
