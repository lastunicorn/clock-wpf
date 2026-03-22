using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Utils;

/// <summary>
/// Represents a color in HSB coordinates.
/// </summary>
/// <remarks>
/// Based on an original script developed by Vladimir Yangurskiy
/// </remarks>
public record struct HsbColor
{
    public byte Alpha { get; }

    public float Hue { get; }

    public float Saturation { get; }

    public float Brightness { get; }

    public HsbColor(float hue, float saturation, float brightness)
    {
        Alpha = 0xff;
        Hue = Math.Clamp(hue, 0, 255);
        Saturation = Math.Clamp(saturation, 0, 255);
        Brightness = Math.Clamp(brightness, 0, 255);
    }

    public HsbColor(byte alpha, float hue, float saturation, float brightness)
    {
        Alpha = alpha;
        Hue = Math.Clamp(hue, 0, 255);
        Saturation = Math.Clamp(saturation, 0, 255);
        Brightness = Math.Clamp(brightness, 0, 255);
    }

    public HsbColor ShiftHue(float delta)
    {
        float newHue = Hue + delta;
        newHue = Math.Clamp(newHue, 0, 255);

        return new HsbColor(Alpha, newHue, Saturation, Brightness);
    }

    public HsbColor IncreaseHue(Percentage percentage)
    {
        float delta = (255 - Hue) * percentage;
        float newHue = Hue + delta;
        newHue = Math.Clamp(newHue, 0, 255);

        return new HsbColor(Alpha, newHue, Saturation, Brightness);
    }

    public HsbColor DecreaseHue(Percentage percentage)
    {
        float delta = Hue * percentage;
        float newHue = Hue - delta;
        newHue = Math.Clamp(newHue, 0, 255);

        return new HsbColor(Alpha, newHue, Saturation, Brightness);
    }

    public HsbColor ShiftSaturation(float delta)
    {
        float newSaturation = Saturation + delta;
        newSaturation = Math.Clamp(newSaturation, 0, 255);

        return new HsbColor(Alpha, Hue, newSaturation, Brightness);
    }

    public HsbColor IncreaseSaturation(Percentage percentage)
    {
        float delta = (255 - Saturation) * percentage;
        float newSaturation = Saturation + delta;
        newSaturation = Math.Clamp(newSaturation, 0, 255);

        return new HsbColor(Alpha, Hue, newSaturation, Brightness);
    }

    public HsbColor DecreaseSaturation(Percentage percentage)
    {
        float delta = Saturation * percentage;
        float newSaturation = Saturation - delta;
        newSaturation = Math.Clamp(newSaturation, 0, 255);

        return new HsbColor(Alpha, Hue, newSaturation, Brightness);
    }

    public HsbColor ShiftBrighness(float delta)
    {
        float newBrightness = Brightness + delta;
        newBrightness = Math.Clamp(newBrightness, 0, 255);

        return new HsbColor(Alpha, Hue, Saturation, newBrightness);
    }

    public HsbColor IncreaseBrighness(Percentage percentage)
    {
        float delta = (255 - Brightness) * percentage;
        float newBrightness = Brightness + delta;
        newBrightness = Math.Clamp(newBrightness, 0, 255);

        return new HsbColor(Alpha, Hue, Saturation, newBrightness);
    }

    public HsbColor DecreaseBrighness(Percentage percentage)
    {
        float delta = Brightness * percentage;
        float newBrightness = Brightness - delta;
        newBrightness = Math.Clamp(newBrightness, 0, 255);

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
            float dif = Brightness * Saturation / 255f;
            float min = Brightness - dif;

            float h = Hue * 360f / 255f;

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
        byte red = (byte)Math.Round(Math.Clamp(r, 0, 255));
        byte green = (byte)Math.Round(Math.Clamp(g, 0, 255));
        byte blue = (byte)Math.Round(Math.Clamp(b, 0, 255));

        return Color.FromArgb(alpha, red, green, blue);
    }

    public static HsbColor FromColor(Color color)
    {
        byte alpha = color.A;
        float hue = 0f;
        float saturation = 0f;
        float brightness = 0f;

        float r = color.R;
        float g = color.G;
        float b = color.B;

        float max = Math.Max(r, Math.Max(g, b));

        if (max > 0)
        {
            float min = Math.Min(r, Math.Min(g, b));
            float dif = max - min;

            if (max > min)
            {
                if (g == max)
                {
                    hue = (b - r) / dif * 60f + 120f;
                }
                else if (b == max)
                {
                    hue = (r - g) / dif * 60f + 240f;
                }
                else if (b > g)
                {
                    hue = (g - b) / dif * 60f + 360f;
                }
                else
                {
                    hue = (g - b) / dif * 60f;
                }
                if (hue < 0)
                {
                    hue = hue + 360f;
                }
            }
            else
            {
                hue = 0;
            }

            hue *= 255f / 360f;
            saturation = (dif / max) * 255f;
            brightness = max;
        }

        return new HsbColor(alpha, hue, saturation, brightness);
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
