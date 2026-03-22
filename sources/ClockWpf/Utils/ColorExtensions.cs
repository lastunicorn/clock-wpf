using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Utils;

public static class ColorExtensions
{
    public static Color ShiftHue(this Color color, float hueDelta)
    {
        return HsbColor.FromColor(color)
            .ShiftHue(hueDelta)
            .ToColor();
    }

    public static Color ShiftSaturation(this Color color, float saturationDelta)
    {
        return HsbColor.FromColor(color)
            .ShiftSaturation(saturationDelta)
            .ToColor();
    }

    public static Color ShiftBrighness(this Color color, float brightnessDelta)
    {
        return HsbColor.FromColor(color)
            .ShiftBrighness(brightnessDelta)
            .ToColor();
    }

    /// <summary>
    /// Creates a bright, saturated variant of the color suitable for light gradient stops.
    /// Increases brightness to near maximum and boosts saturation.
    /// </summary>
    public static Color ToBrightVariant(this Color color, float saturationBoost = 50f)
    {
        HsbColor hsbColor = HsbColor.FromColor(color);

        // Target high brightness (around 90-100% of max)
        //float targetBrightness = 255f;
        float brightnessDelta = (255f - hsbColor.Brightness) * new Percentage(80);

        // Boost saturation but not to extreme
        float saturationDelta = -(hsbColor.Saturation * new Percentage(80));

        return hsbColor
            .ShiftBrighness(new Percentage(90))
            .ShiftSaturation(saturationDelta)
            .ToColor();
    }

    /// <summary>
    /// Creates a dark, saturated variant of the color suitable for dark gradient stops.
    /// Decreases brightness significantly and increases saturation for depth.
    /// </summary>
    public static Color ToDarkVariant(this Color color, float saturationBoost = 80f)
    {
        HsbColor hsbColor = HsbColor.FromColor(color);

        // Target low brightness (around 40-50% of current)
        float targetBrightness = hsbColor.Brightness * 0.5f;
        float brightnessDelta = targetBrightness - hsbColor.Brightness;

        // Significantly boost saturation for rich, deep colors
        float targetSaturation = Math.Min(255f, hsbColor.Saturation + saturationBoost);
        float saturationDelta = targetSaturation - hsbColor.Saturation;

        return hsbColor
            .ShiftBrighness(brightnessDelta)
            //.ShiftSaturation(saturationDelta)
            .ToColor();
    }

    /// <summary>
    /// Creates a very bright, softly saturated variant suitable for face/background gradients.
    /// </summary>
    public static Color ToSoftBrightVariant(this Color color)
    {
        HsbColor hsbColor = HsbColor.FromColor(color);

        // Maximum brightness
        float brightnessDelta = 255f - hsbColor.Brightness;

        // Moderate saturation boost (less than bright variant)
        float saturationDelta = 20f;

        return hsbColor
            .ShiftBrighness(brightnessDelta)
            //.ShiftSaturation(saturationDelta)
            .ToColor();
    }

    /// <summary>
    /// Creates a medium-dark, moderately saturated variant suitable for face/background gradients.
    /// </summary>
    public static Color ToMediumVariant(this Color color)
    {
        HsbColor hsbColor = HsbColor.FromColor(color);

        // Target 60% of current brightness
        float targetBrightness = hsbColor.Brightness * 0.6f;
        float brightnessDelta = targetBrightness - hsbColor.Brightness;

        // Moderate saturation boost
        float saturationDelta = 40f;

        return hsbColor
            .ShiftBrighness(brightnessDelta)
            //.ShiftSaturation(saturationDelta)
            .ToColor();
    }
}
