using DustInTheWind.ClockWpf.Utils;


namespace DustInTheWind.ClockWpf.Tests.Utils.HsbColorTests;

public class ShiftHueTests
{
    [Fact]
    public void HavingPositiveDelta_WhenShiftingHue_ThenHueIsIncreased()
    {
        HsbColor HsbColor = new(100, 150, 200);

        HsbColor result = HsbColor.ShiftHue(50);

        Assert.Equal(150, result.Hue);
    }

    [Fact]
    public void HavingNegativeDelta_WhenShiftingHue_ThenHueIsDecreased()
    {
        HsbColor HsbColor = new(100, 150, 200);

        HsbColor result = HsbColor.ShiftHue(-50);

        Assert.Equal(50, result.Hue);
    }

    [Fact]
    public void HavingDeltaThatExceedsMaximum_WhenShiftingHue_ThenHueIsClampedTo255()
    {
        HsbColor HsbColor = new(300, 40, 50);

        HsbColor result = HsbColor.ShiftHue(100);

        Assert.Equal(360, result.Hue);
    }

    [Fact]
    public void HavingDeltaThatGoesBelow0_WhenShiftingHue_ThenHueIsClampedTo0()
    {
        HsbColor HsbColor = new(50, 150, 200);

        HsbColor result = HsbColor.ShiftHue(-100);

        Assert.Equal(0, result.Hue);
    }

    [Fact]
    public void HavingAlpha_WhenShiftingHue_ThenAlphaIsPreserved()
    {
        HsbColor HsbColor = new(128, 100, 40, 50);

        HsbColor result = HsbColor.ShiftHue(20);

        Assert.Equal(128, result.Alpha);
    }

    [Fact]
    public void HavingSaturation_WhenShiftingHue_ThenSaturationIsPreserved()
    {
        HsbColor HsbColor = new(100, 40, 50);

        HsbColor result = HsbColor.ShiftHue(20);

        Assert.Equal(40, result.Saturation);
    }

    [Fact]
    public void HavingBrightness_WhenShiftingHue_ThenBrightnessIsPreserved()
    {
        HsbColor HsbColor = new(100, 40, 50);

        HsbColor result = HsbColor.ShiftHue(20);

        Assert.Equal(50, result.Brightness);
    }
}
