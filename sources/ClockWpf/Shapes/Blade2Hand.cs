using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Utils;

namespace DustInTheWind.ClockWpf.Shapes;

public class Blade2Hand : HandBase
{
    #region Width Dependency Property

    public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
        nameof(Width),
        typeof(double),
        typeof(Blade2Hand),
        new FrameworkPropertyMetadata(20.0, HandleWidthChanged));

    private static void HandleWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Blade2Hand blade2Hand)
        {
            blade2Hand.InvalidateCache();
            blade2Hand.OnChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets the width of the hand, in device-independent units (pixels).
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(20.0)]
    [Description("The width of the hand.")]
    public double Width
    {
        get => (double)GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    #endregion

    #region HipDistance Dependency Property

    public static readonly DependencyProperty HipDistanceProperty = DependencyProperty.Register(
        nameof(HipDistance),
        typeof(double),
        typeof(Blade2Hand),
        new FrameworkPropertyMetadata(45.0, HandleHipDistanceChanged));

    private static void HandleHipDistanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Blade2Hand blade2Hand)
        {
            blade2Hand.InvalidateCache();
            blade2Hand.OnChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets the distance from the origin to the widest part of the hand, referred to as the hip.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(45.0)]
    [Description("The distance from the origin to the most wide part of the hand (the hip).")]
    public double HipDistance
    {
        get => (double)GetValue(HipDistanceProperty);
        set => SetValue(HipDistanceProperty, value);
    }

    #endregion

    #region TipLength Dependency Property

    public static readonly DependencyProperty TipLengthProperty = DependencyProperty.Register(
        nameof(TipLength),
        typeof(double),
        typeof(Blade2Hand),
        new FrameworkPropertyMetadata(15.0, HandleTipLengthChanged));

    private static void HandleTipLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Blade2Hand blade2Hand)
        {
            blade2Hand.InvalidateCache();
            blade2Hand.OnChanged(EventArgs.Empty);
        }
    }

    public double TipLength
    {
        get => (double)GetValue(TipLengthProperty);
        set => SetValue(TipLengthProperty, value);
    }

    #endregion

    static Blade2Hand()
    {
        StrokeThicknessProperty.OverrideMetadata(typeof(Blade2Hand), new FrameworkPropertyMetadata(0.0));
    }

    private StreamGeometry geometry;
    private Pen strokePen;

    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        double clockRadius = context.ClockRadius;
        double length = Length.RelativeTo(clockRadius);
        double width = Width.RelativeTo(clockRadius);
        double halfWidth = width / 2;
        double hipDistance = HipDistance.RelativeTo(clockRadius);
        double tipLength = TipLength.RelativeTo(clockRadius);
        double tipWidth = 0.8.RelativeTo(clockRadius);
        double tipBaseWidth = 2.RelativeTo(clockRadius);
        double baseWidth = 3.RelativeTo(clockRadius);

        // Background

        Point pointA1 = new(-baseWidth / 2, 0);
        Point pointA2 = new(-halfWidth, -hipDistance);
        Point pointA3 = new(-tipBaseWidth / 2, -length + tipLength);
        Point pointA4 = new(-tipWidth / 2, -length);
        Point pointA5 = new(tipWidth / 2, -length);
        Point pointA6 = new(tipBaseWidth / 2, -length + tipLength);
        Point pointA7 = new(halfWidth, -hipDistance);
        Point pointA8 = new(baseWidth / 2, 0);

        StreamGeometry geometry = new();

        StreamGeometryContext streamGeometryContext1 = geometry.Open();

        streamGeometryContext1.BeginFigure(pointA1, true, true);

        streamGeometryContext1.LineTo(pointA2, true, true);
        streamGeometryContext1.LineTo(pointA3, true, true);
        streamGeometryContext1.LineTo(pointA4, true, true);
        streamGeometryContext1.LineTo(pointA5, true, true);
        streamGeometryContext1.LineTo(pointA6, true, true);
        streamGeometryContext1.LineTo(pointA7, true, true);
        streamGeometryContext1.LineTo(pointA8, true, true);

        // Background - Finish

        streamGeometryContext1.Close();

        if (geometry.CanFreeze)
            geometry.Freeze();

        this.geometry = geometry;

        // Stroke Brush

        strokePen = CreateStrokePen(context);
    }

    protected override void DoRenderHand(ClockDrawingContext context)
    {
        context.DrawingContext.DrawGeometry(FillBrush, strokePen, geometry);
    }
}
