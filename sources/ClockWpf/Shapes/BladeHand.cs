using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
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

    [Category("Appearance")]
    [DefaultValue(20.0)]
    [Description("The distance from the origin where the hip (most wide part of the hand) should be located.")]
    public double HipDistance
    {
        get => (double)GetValue(HipDistanceProperty);
        set => SetValue(HipDistanceProperty, value);
    }

    #endregion

    static BladeHand()
    {
        StrokeThicknessProperty.OverrideMetadata(typeof(BladeHand), new FrameworkPropertyMetadata(0.0));
    }

    protected override void DoRenderHand(ClockDrawingContext context)
    {
        double calculatedLength = Length.RelativeTo(context.ClockRadius);
        double calculatedWidth = Width.RelativeTo(context.ClockRadius);
        double calculatedHalfWidth = calculatedWidth / 2;
        double hipDistance = HipDistance.RelativeTo(context.ClockRadius);

        Point pointA1 = new(0, 0);
        Point pointA2 = new(-calculatedHalfWidth, -hipDistance);
        Point pointA3 = new(0, -calculatedLength);
        Point pointA4 = new(calculatedHalfWidth, -hipDistance);

        // Background

        StreamGeometry geometry1 = new();

        StreamGeometryContext streamGeometryContext1 = geometry1.Open();

        streamGeometryContext1.BeginFigure(pointA1, true, true);

        streamGeometryContext1.LineTo(pointA2, true, true);
        streamGeometryContext1.LineTo(pointA3, true, true);
        streamGeometryContext1.LineTo(pointA4, true, true);

        streamGeometryContext1.Close();

        if (geometry1.CanFreeze)
            geometry1.Freeze();

        Pen strokePen1 = new(StrokeBrush, StrokeThickness);
        strokePen1.Freeze();

        context.DrawingContext.DrawGeometry(FillBrush, strokePen1, geometry1);

        // Shadow

        double shadowMargin = 2.RelativeTo(context.ClockRadius);

        Point pointB1 = new(0, -shadowMargin * 2);
        Point pointB2 = new(-calculatedHalfWidth + shadowMargin, -hipDistance);
        Point pointB3 = new(0, -calculatedLength + shadowMargin * 4);

        StreamGeometry geometry2 = new();

        StreamGeometryContext streamGeometryContext2 = geometry2.Open();

        streamGeometryContext2.BeginFigure(pointB1, true, true);

        streamGeometryContext2.LineTo(pointB2, true, true);
        streamGeometryContext2.LineTo(pointB3, true, true);

        streamGeometryContext2.Close();

        if (geometry2.CanFreeze)
            geometry2.Freeze();

        context.DrawingContext.DrawGeometry(Brushes.Gray, null, geometry2);
    }
}
