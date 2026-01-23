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
        new FrameworkPropertyMetadata(10.0));

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
        new FrameworkPropertyMetadata(2.0));

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
        typeof(FancyBackground),
        new FrameworkPropertyMetadata(CreateDefaultOuterRimBrush()));

    private static Brush CreateDefaultOuterRimBrush()
    {
        LinearGradientBrush brush = new()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1)
        };
        
        brush.GradientStops.Add(new GradientStop(Color.FromRgb(155, 219, 255), 0));
        brush.GradientStops.Add(new GradientStop(Color.FromRgb(0, 64, 128), 1));
        
        return brush;
    }

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
        typeof(FancyBackground),
        new FrameworkPropertyMetadata(CreateDefaultInnerRimBrush()));

    private static Brush CreateDefaultInnerRimBrush()
    {
        LinearGradientBrush brush = new()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1)
        };

        brush.GradientStops.Add(new GradientStop(Color.FromRgb(0, 64, 128), 0));
        brush.GradientStops.Add(new GradientStop(Color.FromRgb(155, 219, 255), 1));
        
        return brush;
    }

    public Brush InnerRimBrush
    {
        get => (Brush)GetValue(InnerRimBrushProperty);
        set => SetValue(InnerRimBrushProperty, value);
    }

    #endregion

    static FancyBackground()
    {
        FillBrushProperty.OverrideMetadata(typeof(FancyBackground), new FrameworkPropertyMetadata(CreateDefaultFaceBrush()));
        StrokeThicknessProperty.OverrideMetadata(typeof(FancyBackground), new FrameworkPropertyMetadata(0.0));
    }

    private static Brush CreateDefaultFaceBrush()
    {
        LinearGradientBrush brush = new()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1)
        };

        brush.GradientStops.Add(new GradientStop(Color.FromRgb(200, 230, 255), 0));
        brush.GradientStops.Add(new GradientStop(Color.FromRgb(50, 100, 150), 1));

        return brush;
    }

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (OuterRimBrush == null && InnerRimBrush == null && FillBrush == null)
            return false;

        return base.OnRendering(context);
    }

    public override void DoRender(ClockDrawingContext context)
    {
        Point center = new(0, 0);
        double outerRadius = context.ClockDiameter / 2;
        double innerRimRadius = outerRadius - OuterRimWidth;
        double faceRadius = innerRimRadius - InnerRimWidth;

        if (OuterRimBrush != null)
            context.DrawingContext.DrawEllipse(OuterRimBrush, null, center, outerRadius, outerRadius);

        if (InnerRimBrush != null)
            context.DrawingContext.DrawEllipse(InnerRimBrush, null, center, innerRimRadius, innerRimRadius);

        if (FillBrush != null)
            context.DrawingContext.DrawEllipse(FillBrush, StrokePen, center, faceRadius, faceRadius);
    }
}
