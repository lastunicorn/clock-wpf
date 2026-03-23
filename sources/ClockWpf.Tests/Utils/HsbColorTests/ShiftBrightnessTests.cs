using DustInTheWind.ClockWpf.Utils;


namespace DustInTheWind.ClockWpf.Tests.Utils.HsbColorTests;

public class ShiftBrightnessTests
{
    [Fact]
    public void HavingPositiveDelta_WhenShiftingBrightness_ThenBrightnessIsIncreased()
    {
        HsbColor HsbColor = new(100, 40, 40);

        HsbColor result = HsbColor.ShiftBrighness(20);

        Assert.Equal(60, result.Brightness);
    }

    [Fact]
    public void HavingNegativeDelta_WhenShiftingBrightness_ThenBrightnessIsDecreased()
    {
        HsbColor HsbColor = new(100, 150, 100);

        HsbColor result = HsbColor.ShiftBrighness(-50);

        Assert.Equal(50, result.Brightness);
    }

    [Fact]
    public void HavingDeltaThatExceedsMaximum_WhenShiftingBrightness_ThenBrightnessIsClampedTo255()
    {
        HsbColor HsbColor = new(100, 40, 80);

        HsbColor result = HsbColor.ShiftBrighness(40);

        Assert.Equal(100, result.Brightness);
    }

    [Fact]
    public void HavingDeltaThatGoesBelow0_WhenShiftingBrightness_ThenBrightnessIsClampedTo0()
    {
        HsbColor HsbColor = new(100, 150, 50);

        HsbColor result = HsbColor.ShiftBrighness(-100);

        Assert.Equal(0, result.Brightness);
    }

    [Fact]
    public void HavingAlpha_WhenShiftingBrightness_ThenAlphaIsPreserved()
    {
        HsbColor HsbColor = new(128, 100, 40, 40);

        HsbColor result = HsbColor.ShiftBrighness(20);

        Assert.Equal(128, result.Alpha);
    }

    [Fact]
    public void HavingHue_WhenShiftingBrightness_ThenHueIsPreserved()
    {
        HsbColor HsbColor = new(100, 150, 100);

        HsbColor result = HsbColor.ShiftBrighness(20);

        Assert.Equal(100, result.Hue);
    }

    [Fact]
    public void HavingSaturation_WhenShiftingBrightness_ThenSaturationIsPreserved()
    {
        HsbColor HsbColor = new(100, 40, 40);

        HsbColor result = HsbColor.ShiftBrighness(20);

        Assert.Equal(40, result.Saturation);
    }
}
