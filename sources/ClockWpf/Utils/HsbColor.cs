using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Utils;

/// <summary>
/// Represents a color in HSB coordinates.
/// </summary>
public record struct HsbColor
{
    /// <summary>
    /// Gets the alpha component value of the color.
    /// </summary>
    public byte Alpha { get; }

    /// <summary>
    /// Gets the hue component of the color, measured in degrees.
    /// </summary>
    /// <remarks>
    /// The hue value typically ranges from 0 to 360, representing the position on the color wheel.
    /// A value of 0 or 360 corresponds to red, 120 to green, and 240 to blue.
    /// The exact range and interpretation may depend on the color model used.
    /// </remarks>
    public float Hue { get; }

    /// <summary>
    /// Gets the saturation component of the color. It is a percentage and representing the
    /// intensity or purity of the hue.
    /// </summary>
    public float Saturation { get; }

    /// <summary>
    /// Gets the brightness component of the color as a floating-point value.
    /// </summary>
    public float Brightness { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HsbColor"/> structure with the specified hue,
    /// saturation, and brightness values.
    /// </summary>
    /// <param name="hue">The hue component of the color, in degrees. Valid values are from 0 to 360.</param>
    /// <param name="saturation">The saturation component of the color, as a percentage. Valid values are from 0 to 100.</param>
    /// <param name="brightness">The brightness component of the color, as a percentage. Valid values are from 0 to 100.</param>
    public HsbColor(float hue, float saturation, float brightness)
    {
        Alpha = 0xff;
        Hue = Math.Clamp(hue, 0, 360);
        Saturation = Math.Clamp(saturation, 0, 100);
        Brightness = Math.Clamp(brightness, 0, 100);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HsbColor"/> class with the specified alpha, hue,
    /// saturation, and brightness values.
    /// </summary>
    /// <param name="alpha">The alpha component of the color. Represents the opacity as a value from 0 to 255.</param>
    /// <param name="hue">The hue component of the color, in degrees. Values outside the range 0 to 360 are clamped.</param>
    /// <param name="saturation">The saturation component of the color, as a percentage from 0 to 100. Values outside this range are clamped.</param>
    /// <param name="brightness">The brightness component of the color, as a percentage from 0 to 100. Values outside this range are clamped.</param>
    public HsbColor(byte alpha, float hue, float saturation, float brightness)
    {
        Alpha = alpha;
        Hue = Math.Clamp(hue, 0, 360);
        Saturation = Math.Clamp(saturation, 0, 100);
        Brightness = Math.Clamp(brightness, 0, 100);
    }

    public HsbColor ShiftHue(float delta)
    {
        float newHue = Hue + delta;
        newHue = Math.Clamp(newHue, 0, 360);

        return new HsbColor(Alpha, newHue, Saturation, Brightness);
    }

    public HsbColor IncreaseHue(Percentage percentage)
    {
        float delta = (360 - Hue) * percentage;
        float newHue = Hue + delta;
        newHue = Math.Clamp(newHue, 0, 360);

        return new HsbColor(Alpha, newHue, Saturation, Brightness);
    }

    public HsbColor DecreaseHue(Percentage percentage)
    {
        float delta = Hue * percentage;
        float newHue = Hue - delta;
        newHue = Math.Clamp(newHue, 0, 360);

        return new HsbColor(Alpha, newHue, Saturation, Brightness);
    }

    public HsbColor ShiftSaturation(float delta)
    {
        float newSaturation = Saturation + delta;
        newSaturation = Math.Clamp(newSaturation, 0, 100);

        return new HsbColor(Alpha, Hue, newSaturation, Brightness);
    }

    public HsbColor IncreaseSaturation(Percentage percentage)
    {
        float delta = (100 - Saturation) * percentage;
        float newSaturation = Saturation + delta;
        newSaturation = Math.Clamp(newSaturation, 0, 100);

        return new HsbColor(Alpha, Hue, newSaturation, Brightness);
    }

    public HsbColor DecreaseSaturation(Percentage percentage)
    {
        float delta = Saturation * percentage;
        float newSaturation = Saturation - delta;
        newSaturation = Math.Clamp(newSaturation, 0, 100);

        return new HsbColor(Alpha, Hue, newSaturation, Brightness);
    }

    public HsbColor ShiftBrighness(float delta)
    {
        float newBrightness = Brightness + delta;
        newBrightness = Math.Clamp(newBrightness, 0, 100);

        return new HsbColor(Alpha, Hue, Saturation, newBrightness);
    }

    public HsbColor IncreaseBrighness(Percentage percentage)
    {
        float delta = (100 - Brightness) * percentage;
        float newBrightness = Brightness + delta;
        newBrightness = Math.Clamp(newBrightness, 0, 100);

        return new HsbColor(Alpha, Hue, Saturation, newBrightness);
    }

    public HsbColor DecreaseBrighness(Percentage percentage)
    {
        float delta = Brightness * percentage;
        float newBrightness = Brightness - delta;
        newBrightness = Math.Clamp(newBrightness, 0, 100);

        return new HsbColor(Alpha, Hue, Saturation, newBrightness);
    }

    public Color ToColor()
    {
        float r = Brightness;
        float g = Brightness;
        float b = Brightness;

        if (Saturation != 0)
        {
            float max = Brightness;
            float dif = Brightness * Saturation / 100f;
            float min = Brightness - dif;

            float h = Hue;

            if (h < 60f)
            {
                r = max;
                g = h * dif / 60f + min;
                b = min;
            }
            else if (h < 120f)
            {
                r = -(h - 120f) * dif / 60f + min;
                g = max;
                b = min;
            }
            else if (h < 180f)
            {
                r = min;
                g = max;
                b = (h - 120f) * dif / 60f + min;
            }
            else if (h < 240f)
            {
                r = min;
                g = -(h - 240f) * dif / 60f + min;
                b = max;
            }
            else if (h < 300f)
            {
                r = (h - 240f) * dif / 60f + min;
                g = min;
                b = max;
            }
            else if (h <= 360f)
            {
                r = max;
                g = min;
                b = -(h - 360f) * dif / 60 + min;
            }
            else
            {
                r = 0;
                g = 0;
                b = 0;
            }
        }

        byte alpha = Alpha;
        byte red = (byte)Math.Round(Math.Clamp(r * 255f / 100f, 0, 255));
        byte green = (byte)Math.Round(Math.Clamp(g * 255f / 100f, 0, 255));
        byte blue = (byte)Math.Round(Math.Clamp(b * 255f / 100f, 0, 255));

        return Color.FromArgb(alpha, red, green, blue);
    }

    public static HsbColor FromColor(Color color)
    {
        byte alpha = color.A;
        float hue = 0f;
        float saturation = 0f;
        float brightness = 0f;

        float r = color.R / 255f;
        float g = color.G / 255f;
        float b = color.B / 255f;

        float max = Math.Max(r, Math.Max(g, b));

        float min = Math.Min(r, Math.Min(g, b));
        float diff = max - min;

        if (diff > 0)
        {
            if (max == r)
                hue = 60f * ((g - b) / diff);
            else if (max == g)
                hue = 60f * ((b - r) / diff + 2);
            else if (max == b)
                hue = 60f * ((r - g) / diff + 4);

            if (hue < 0)
                hue += 360f;
        }

        if (max > 0)
            saturation = (diff / max) * 100f;

        brightness = max * 100f;

        return new HsbColor(alpha, hue, saturation, brightness);
    }

    public override string ToString()
    {
        return $"A = {Alpha:N2}; H = {Hue:N2}; S = {Saturation:N2}; B = {Brightness:N2}";
    }

    public static implicit operator Color(HsbColor hsbColor)
    {
        return hsbColor.ToColor();
    }

    public static implicit operator HsbColor(Color color)
    {
        return FromColor(color);
    }
}
