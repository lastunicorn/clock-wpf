using DustInTheWind.ClockWpf.Utils;


namespace DustInTheWind.ClockWpf.Tests.Utils.HsbColorTests;

public class ShiftSaturationTests
{
    [Fact]
    public void HavingPositiveDelta_WhenShiftingSaturation_ThenSaturationIsIncreased()
    {
        HsbColor HsbColor = new(100, 40, 50);

        HsbColor result = HsbColor.ShiftSaturation(20);

        Assert.Equal(60, result.Saturation);
    }

    [Fact]
    public void HavingNegativeDelta_WhenShiftingSaturation_ThenSaturationIsDecreased()
    {
        HsbColor HsbColor = new(100, 60, 50);

        HsbColor result = HsbColor.ShiftSaturation(-20);

        Assert.Equal(40, result.Saturation);
    }

    [Fact]
    public void HavingDeltaThatExceedsMaximum_WhenShiftingSaturation_ThenSaturationIsClampedTo255()
    {
        HsbColor HsbColor = new(100, 80, 50);

        HsbColor result = HsbColor.ShiftSaturation(40);

        Assert.Equal(100, result.Saturation);
    }

    [Fact]
    public void HavingDeltaThatGoesBelow0_WhenShiftingSaturation_ThenSaturationIsClampedTo0()
    {
        HsbColor HsbColor = new(100, 20, 50);

        HsbColor result = HsbColor.ShiftSaturation(-40);

        Assert.Equal(0, result.Saturation);
    }

    [Fact]
    public void HavingAlpha_WhenShiftingSaturation_ThenAlphaIsPreserved()
    {
        HsbColor HsbColor = new(128, 100, 40, 50);

        HsbColor result = HsbColor.ShiftSaturation(20);

        Assert.Equal(128, result.Alpha);
    }

    [Fact]
    public void HavingHue_WhenShiftingSaturation_ThenHueIsPreserved()
    {
        HsbColor HsbColor = new(100, 40, 50);

        HsbColor result = HsbColor.ShiftSaturation(20);

        Assert.Equal(100, result.Hue);
    }

    [Fact]
    public void HavingBrightness_WhenShiftingSaturation_ThenBrightnessIsPreserved()
    {
        HsbColor HsbColor = new(100, 40, 50);

        HsbColor result = HsbColor.ShiftSaturation(20);

        Assert.Equal(50, result.Brightness);
    }
}
