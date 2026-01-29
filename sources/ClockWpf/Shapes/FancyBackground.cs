using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

public class FancyBackground : Shape
{
    #region OuterRimWidth DependencyProperty

    public static readonly DependencyProperty OuterRimWidthProperty = DependencyProperty.Register(
        nameof(OuterRimWidth),
        typeof(double),
        typeof(FancyBackground),
        new FrameworkPropertyMetadata(10.0, HandleOuterRimWidthChanged));

    private static void HandleOuterRimWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FancyBackground fancyBackground)
            fancyBackground.InvalidateLayout();
    }

    public double OuterRimWidth
    {
        get => (double)GetValue(OuterRimWidthProperty);
        set => SetValue(OuterRimWidthProperty, value);
    }

    #endregion

    #region InnerRimWidth DependencyProperty

    public static readonly DependencyProperty InnerRimWidthProperty = DependencyProperty.Register(
        nameof(InnerRimWidth),
        typeof(double),
        typeof(FancyBackground),
        new FrameworkPropertyMetadata(2.0, HandleInnerRimWidthChanged));

    private static void HandleInnerRimWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FancyBackground fancyBackground)
            fancyBackground.InvalidateLayout();
    }

    public double InnerRimWidth
    {
        get => (double)GetValue(InnerRimWidthProperty);
        set => SetValue(InnerRimWidthProperty, value);
    }

    #endregion

    #region OuterRimBrush DependencyProperty

    public static readonly DependencyProperty OuterRimBrushProperty = DependencyProperty.Register(
        nameof(OuterRimBrush),
        typeof(Brush),
        typeof(FancyBackground));

    [Category("Appearance")]
    public Brush OuterRimBrush
    {
        get => (Brush)GetValue(OuterRimBrushProperty);
        set => SetValue(OuterRimBrushProperty, value);
    }

    #endregion

    #region InnerRimBrush DependencyProperty

    public static readonly DependencyProperty InnerRimBrushProperty = DependencyProperty.Register(
        nameof(InnerRimBrush),
        typeof(Brush),
        typeof(FancyBackground));

    [Category("Appearance")]
    public Brush InnerRimBrush
    {
        get => (Brush)GetValue(InnerRimBrushProperty);
        set => SetValue(InnerRimBrushProperty, value);
    }

    #endregion

    #region FillColor DependencyProperty

    public static readonly DependencyProperty FillColorProperty = DependencyProperty.Register(
        nameof(FillColor),
        typeof(Color),
        typeof(FancyBackground),
        new FrameworkPropertyMetadata(Colors.Black, HandleFillColorChanged));

    private static void HandleFillColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FancyBackground fancyBackground)
            fancyBackground.InvalidateLayout();
    }

    [Category("Appearance")]
    public Color FillColor
    {
        get => (Color)GetValue(FillColorProperty);
        set => SetValue(FillColorProperty, value);
    }

    #endregion

    static FancyBackground()
    {
        FillBrushProperty.OverrideMetadata(typeof(FancyBackground), new FrameworkPropertyMetadata((Brush)null));
        StrokeThicknessProperty.OverrideMetadata(typeof(FancyBackground), new FrameworkPropertyMetadata(0.0));
    }

    protected override bool OnRendering(ClockDrawingContext context)
    {
        return true;
    }

    private double calculatedOuterRimRadius;
    private double calculatedInnerRimRadius;
    private double calculatedFaceRadius;
    private Brush calculatedOuterRimBrush;
    private Brush calculatedInnerRimBrush;
    private Brush calculatedFaceBrush;

    protected override void CalculateLayout(ClockDrawingContext context)
    {
        base.CalculateLayout(context);

        double clockRadius = context.ClockRadius;

        calculatedOuterRimRadius = clockRadius;

        double calculatedOuterRimWidth = clockRadius * (OuterRimWidth / 100);
        calculatedInnerRimRadius = clockRadius - calculatedOuterRimWidth;

        double calculatedInnerRimWidth = clockRadius * (InnerRimWidth / 100);
        calculatedFaceRadius = calculatedInnerRimRadius - calculatedInnerRimWidth;

        calculatedOuterRimBrush = OuterRimBrush == null
            ? CreateDefaultOuterRimBrush(FillColor)
            : OuterRimBrush;

        calculatedInnerRimBrush = InnerRimBrush == null
            ? CreateDefaultInnerRimBrush(FillColor)
            : InnerRimBrush;

        calculatedFaceBrush = FillBrush == null
            ? CreateDefaultFaceBrush(FillColor)
            : FillBrush;
    }

    private static Brush CreateDefaultOuterRimBrush(Color color)
    {
        LinearGradientBrush brush = new()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1)
        };

        brush.GradientStops.Add(new GradientStop(color.ShiftBrighness(100f), 0));
        brush.GradientStops.Add(new GradientStop(color.ShiftBrighness(-100f), 1));

        brush.Freeze();
        return brush;
    }

    private static Brush CreateDefaultInnerRimBrush(Color color)
    {
        LinearGradientBrush brush = new()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1)
        };

        brush.GradientStops.Add(new GradientStop(color.ShiftBrighness(-100f), 0));
        brush.GradientStops.Add(new GradientStop(color.ShiftBrighness(100f), 1));

        return brush;
    }

    private static Brush CreateDefaultFaceBrush(Color color)
    {
        LinearGradientBrush brush = new()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1)
        };

        brush.GradientStops.Add(new GradientStop(color.ShiftBrighness(100f), 0));
        brush.GradientStops.Add(new GradientStop(color.ShiftBrighness(-100f), 1));

        if (brush.CanFreeze)
            brush.Freeze();

        return brush;
    }

    //private static Brush CreateDefaultOuterRimBrush()
    //{
    //    LinearGradientBrush brush = new()
    //    {
    //        StartPoint = new Point(0, 0),
    //        EndPoint = new Point(1, 1)
    //    };

    //    brush.GradientStops.Add(new GradientStop(Color.FromRgb(155, 219, 255), 0));
    //    brush.GradientStops.Add(new GradientStop(Color.FromRgb(0, 64, 128), 1));

    //    brush.Freeze();
    //    return brush;
    //}

    //private static Brush CreateDefaultInnerRimBrush()
    //{
    //    LinearGradientBrush brush = new()
    //    {
    //        StartPoint = new Point(0, 0),
    //        EndPoint = new Point(1, 1)
    //    };

    //    brush.GradientStops.Add(new GradientStop(Color.FromRgb(0, 64, 128), 0));
    //    brush.GradientStops.Add(new GradientStop(Color.FromRgb(155, 219, 255), 1));

    //    return brush;
    //}

    //private static Brush CreateDefaultFaceBrush()
    //{
    //    LinearGradientBrush brush = new()
    //    {
    //        StartPoint = new Point(0, 0),
    //        EndPoint = new Point(1, 1)
    //    };

    //    brush.GradientStops.Add(new GradientStop(Color.FromRgb(200, 230, 255), 0));
    //    brush.GradientStops.Add(new GradientStop(Color.FromRgb(50, 100, 150), 1));

    //    if (brush.CanFreeze)
    //        brush.Freeze();

    //    return brush;
    //}

    public override void DoRender(ClockDrawingContext context)
    {
        Point center = new(0, 0);

        if (calculatedOuterRimBrush != null)
            context.DrawingContext.DrawEllipse(calculatedOuterRimBrush, null, center, calculatedOuterRimRadius, calculatedOuterRimRadius);

        if (calculatedInnerRimBrush != null)
            context.DrawingContext.DrawEllipse(calculatedInnerRimBrush, null, center, calculatedInnerRimRadius, calculatedInnerRimRadius);

        if (calculatedFaceBrush != null)
            context.DrawingContext.DrawEllipse(calculatedFaceBrush, StrokePen, center, calculatedFaceRadius, calculatedFaceRadius);
    }
}

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
}


/// <summary>
/// Provides Round-trip conversion from RGB to HSB and back
/// </summary>
/// <remarks>
/// Based on an original script developed by Vladimir Yangurskiy
/// </remarks>
public struct HsbColor
{
    public byte Alpha { get; }

    public float Hue { get; }

    public float Saturation { get; }

    public float Brightness { get; }

    public HsbColor(float hue, float saturation, float brightness)
    {
        Alpha = 0xff;
        Hue = Math.Min(Math.Max(hue, 0), 255);
        Saturation = Math.Min(Math.Max(saturation, 0), 255);
        Brightness = Math.Min(Math.Max(brightness, 0), 255);
    }

    public HsbColor(byte alpha, float hue, float saturation, float brightness)
    {
        Alpha = alpha;
        Hue = Math.Min(Math.Max(hue, 0), 255);
        Saturation = Math.Min(Math.Max(saturation, 0), 255);
        Brightness = Math.Min(Math.Max(brightness, 0), 255);
    }

    public HsbColor ShiftHue(float hueDelta)
    {
        float hue = Hue + hueDelta;
        hue = Math.Min(Math.Max(hue, 0), 255);

        return new HsbColor(Alpha, hue, Saturation, Brightness);
    }

    public HsbColor ShiftSaturation(float saturationDelta)
    {
        float saturation = Saturation + saturationDelta;
        saturation = Math.Min(Math.Max(saturation, 0), 255);

        return new HsbColor(Alpha, Hue, saturation, Brightness);
    }


    public HsbColor ShiftBrighness(float brightnessDelta)
    {
        float brightness = Brightness + brightnessDelta;
        brightness = Math.Min(Math.Max(brightness, 0), 255);

        return new HsbColor(Alpha, Hue, Saturation, brightness);
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
        byte red = (byte)Math.Round(Math.Min(Math.Max(r, 0), 255));
        byte green = (byte)Math.Round(Math.Min(Math.Max(g, 0), 255));
        byte blue = (byte)Math.Round(Math.Min(Math.Max(b, 0), 255));

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