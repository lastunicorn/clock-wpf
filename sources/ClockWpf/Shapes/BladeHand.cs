using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Templates.Shapes;
using DustInTheWind.ClockWpf.Utils;

namespace DustInTheWind.ClockWpf.Shapes;

public class BladeHand : HandBase
{
    #region Width Dependency Property

    public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
        nameof(Width),
        typeof(double),
        typeof(BladeHand),
        new FrameworkPropertyMetadata(20.0, HandleWidthChanged));

    private static void HandleWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is BladeHand bladeHand)
        {
            bladeHand.InvalidateCache();
            bladeHand.OnChanged(EventArgs.Empty);
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
        typeof(BladeHand),
        new FrameworkPropertyMetadata(20.0, HandleHipDistanceChanged));

    private static void HandleHipDistanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is BladeHand bladeHand)
        {
            bladeHand.InvalidateCache();
            bladeHand.OnChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets the distance from the origin to the widest part of the hand, referred to as the hip.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(20.0)]
    [Description("The distance from the origin to the most wide part of the hand (the hip).")]
    public double HipDistance
    {
        get => (double)GetValue(HipDistanceProperty);
        set => SetValue(HipDistanceProperty, value);
    }

    #endregion

    #region ShadowMargin Dependency Property

    public static readonly DependencyProperty ShadowMarginProperty = DependencyProperty.Register(
        nameof(ShadowMargin),
        typeof(double),
        typeof(BladeHand),
        new FrameworkPropertyMetadata(2.0, HandleShadowMarginChanged));

    private static void HandleShadowMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is BladeHand bladeHand)
        {
            bladeHand.InvalidateCache();
            bladeHand.OnChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets the distance, in pixels, between the margin of the hand and its inner shadow.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(2.0)]
    [Description("The space betwwen the margin of the hand and the inner shadow.")]
    public double ShadowMargin
    {
        get => (double)GetValue(ShadowMarginProperty);
        set => SetValue(ShadowMarginProperty, value);
    }

    #endregion

    static BladeHand()
    {
        StrokeThicknessProperty.OverrideMetadata(typeof(BladeHand), new FrameworkPropertyMetadata(0.0));
    }

    private StreamGeometry geometryBackground;
    private StreamGeometry geometryShade;
    private Pen strokePen;

    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        double clockRadius = context.ClockRadius;
        double calculatedLength = Length.RelativeTo(clockRadius);
        double calculatedWidth = Width.RelativeTo(clockRadius);
        double calculatedHalfWidth = calculatedWidth / 2;
        double hipDistance = HipDistance.RelativeTo(clockRadius);

        // Background

        Point pointA1 = new(0, 0);
        Point pointA2 = new(-calculatedHalfWidth, -hipDistance);
        Point pointA3 = new(0, -calculatedLength);
        Point pointA4 = new(calculatedHalfWidth, -hipDistance);

        StreamGeometry geometryBackground = new();

        StreamGeometryContext streamGeometryContext1 = geometryBackground.Open();

        streamGeometryContext1.BeginFigure(pointA1, true, true);

        streamGeometryContext1.LineTo(pointA2, true, true);
        streamGeometryContext1.LineTo(pointA3, true, true);
        streamGeometryContext1.LineTo(pointA4, true, true);

        streamGeometryContext1.Close();

        if (geometryBackground.CanFreeze)
            geometryBackground.Freeze();

        this.geometryBackground = geometryBackground;

        // Background - Stroke

        strokePen = CreateStrokePen(context);

        // Shadow

        double calculatedShadowMargin = ShadowMargin.RelativeTo(clockRadius);

        if (calculatedShadowMargin < 0)
            calculatedShadowMargin = 0;
        else if (calculatedShadowMargin > calculatedHalfWidth)
            calculatedShadowMargin = calculatedHalfWidth;

        Point pointB1 = new(0, -calculatedShadowMargin * 1.5);
        Point pointB2 = new(-calculatedHalfWidth + calculatedShadowMargin, -hipDistance);
        Point pointB3 = new(0, -calculatedLength + calculatedShadowMargin * 4);

        StreamGeometry geometryShade = new();

        StreamGeometryContext streamGeometryContext2 = geometryShade.Open();

        streamGeometryContext2.BeginFigure(pointB1, true, true);

        streamGeometryContext2.LineTo(pointB2, true, true);
        streamGeometryContext2.LineTo(pointB3, true, true);

        streamGeometryContext2.Close();

        if (geometryShade.CanFreeze)
            geometryShade.Freeze();

        this.geometryShade = geometryShade;
    }

    protected override void DoRenderHand(ClockDrawingContext context)
    {
        context.DrawingContext.DrawGeometry(FillBrush, strokePen, geometryBackground);
        context.DrawingContext.DrawGeometry(Brushes.Gray, null, geometryShade);
    }

    public override void Import(ShapeT shapeT)
    {
        base.Import(shapeT);

        if (shapeT is not BladeHandT bladeHandT)
            return;

        Width = bladeHandT.Width;
        HipDistance = bladeHandT.HipDistance;
        ShadowMargin = bladeHandT.ShadowMargin;
    }
}
