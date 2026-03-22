using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Templates.Shapes;
using DustInTheWind.ClockWpf.Utils;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// A bar shaped clock hand. It has optional rounded ends and customizable width and tail length.
/// </summary>
public class BarHand : HandBase
{
    #region Width DependencyProperty

    public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
        nameof(Width),
        typeof(double),
        typeof(BarHand),
        new FrameworkPropertyMetadata(10.0, HandleWidthChanged));

    private static void HandleWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is BarHand barHand)
        {
            barHand.InvalidateCache();
            barHand.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Appearance")]
    [DefaultValue(10.0)]
    [Description("The width of the hand.")]
    public double Width
    {
        get => (double)GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    #endregion

    #region TailLength DependencyProperty

    public static readonly DependencyProperty TailLengthProperty = DependencyProperty.Register(
        nameof(TailLength),
        typeof(double),
        typeof(BarHand),
        new FrameworkPropertyMetadata(2.0, HandleTailLengthChange));

    private static void HandleTailLengthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is BarHand barHand)
        {
            barHand.InvalidateCache();
            barHand.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Appearance")]
    [DefaultValue(2.0)]
    [Description("The hand's length of the tail as percentage from the clock's radius.")]
    public double TailLength
    {
        get => (double)GetValue(TailLengthProperty);
        set => SetValue(TailLengthProperty, value);
    }

    #endregion

    #region RoundEnds DependencyProperty

    public static readonly DependencyProperty RoundEndsProperty = DependencyProperty.Register(
        nameof(RoundEnds),
        typeof(bool),
        typeof(BarHand),
        new FrameworkPropertyMetadata(true, HandleRoundEndsChanged));

    private static void HandleRoundEndsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is BarHand barHand)
        {
            barHand.InvalidateCache();
            barHand.OnChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ends of the clock hand are rendered with rounded caps.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Indicates whether the ends of the clock hand are rendered with rounded caps.")]
    public bool RoundEnds
    {
        get => (bool)GetValue(RoundEndsProperty);
        set => SetValue(RoundEndsProperty, value);
    }

    #endregion

    private StreamGeometry geometry;
    private Pen strokePen;

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (Width <= 0)
            return false;

        return base.OnRendering(context);
    }

    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        geometry = CreateHandGeometry(context);
        strokePen = CreateStrokePen(context);
    }

    private StreamGeometry CreateHandGeometry(ClockDrawingContext context)
    {
        double radius = context.ClockRadius;
        double actualLength = Length.RelativeTo(radius);
        double actualTailLength = TailLength.RelativeTo(radius);
        double actualWidth = Width.RelativeTo(radius);
        double actualHalfWidth = actualWidth / 2.0;

        double topY = RoundEnds
            ? -actualLength + actualHalfWidth
            : -actualLength;

        double bottomY = RoundEnds
            ? actualTailLength - actualHalfWidth
            : actualTailLength;

        double leftX = -actualHalfWidth;
        double rightX = actualHalfWidth;

        Point point1 = new(leftX, topY);
        Point point2 = new(rightX, topY);
        Point point3 = new(rightX, bottomY);
        Point point4 = new(leftX, bottomY);

        StreamGeometry streamGeometry = new();

        using (StreamGeometryContext ctx = streamGeometry.Open())
        {
            ctx.BeginFigure(point1, true, true);

            if (RoundEnds)
            {
                // Top semicircle (pointing upward)
                ctx.ArcTo(
                    point2,
                    new Size(actualHalfWidth, actualHalfWidth),
                    0,
                    false,
                    SweepDirection.Clockwise,
                    true,
                    false);
            }
            else
            {
                // Straight line to the right side of the rectangle
                ctx.LineTo(point2, true, false);
            }

            // Right side of the rectangle
            ctx.LineTo(point3, true, false);

            if (RoundEnds)
            {
                // Bottom semicircle (pointing downward)
                ctx.ArcTo(
                    point4,
                    new Size(actualHalfWidth, actualHalfWidth),
                    0,
                    false,
                    SweepDirection.Clockwise,
                    true,
                    false);
            }
            else
            {
                // Straight line to the left side of the rectangle
                ctx.LineTo(point4, true, false);
            }

            // Left side of the rectangle (closes back to start point)
            ctx.LineTo(point1, true, false);
        }

        if (streamGeometry.CanFreeze)
            streamGeometry.Freeze();

        return streamGeometry;
    }

    protected override void DoRenderHand(ClockDrawingContext context)
    {
        context.DrawingContext.DrawGeometry(FillBrush, strokePen, geometry);
    }

    public override void Import(ShapeT shapeT)
    {
        base.Import(shapeT);

        if (shapeT is not BarHandT barHandT)
            return;

        Width = barHandT.Width;
        TailLength = barHandT.TailLength;
        RoundEnds = barHandT.RoundEnds;
    }
}
