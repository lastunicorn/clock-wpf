using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Utils;

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

    private double calculatedOuterRimRadius;
    private double calculatedInnerRimRadius;
    private double calculatedFaceRadius;
    private Brush calculatedOuterRimBrush;
    private Brush calculatedInnerRimBrush;
    private Brush calculatedFaceBrush;

    protected override bool OnRendering(ClockDrawingContext context)
    {
        return true;
    }

    protected override void CalculateLayout(ClockDrawingContext context)
    {
        base.CalculateLayout(context);

        double clockRadius = context.ClockRadius;

        calculatedOuterRimRadius = clockRadius;

        double calculatedOuterRimWidth = clockRadius * (OuterRimWidth / 100);
        calculatedInnerRimRadius = clockRadius - calculatedOuterRimWidth;

        double calculatedInnerRimWidth = clockRadius * (InnerRimWidth / 100);
        calculatedFaceRadius = calculatedInnerRimRadius - calculatedInnerRimWidth;

        calculatedOuterRimBrush = OuterRimBrush ?? CreateDefaultOuterRimBrush(FillColor);
        calculatedInnerRimBrush = InnerRimBrush ?? CreateDefaultInnerRimBrush(FillColor);
        calculatedFaceBrush = FillBrush ?? CreateDefaultFaceBrush(FillColor);
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

    public override void DoRender(ClockDrawingContext context)
    {
        Point center = new(0, 0);

        context.DrawingContext.DrawEllipse(calculatedOuterRimBrush, null, center, calculatedOuterRimRadius, calculatedOuterRimRadius);
        context.DrawingContext.DrawEllipse(calculatedInnerRimBrush, null, center, calculatedInnerRimRadius, calculatedInnerRimRadius);
        context.DrawingContext.DrawEllipse(calculatedFaceBrush, null, center, calculatedFaceRadius, calculatedFaceRadius);
    }
}
