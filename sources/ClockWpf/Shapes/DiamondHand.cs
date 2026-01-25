using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// A diamond shaped clock hand, with customizable width and tail length.
/// </summary>
public class DiamondHand : HandBase
{
    #region Width DependencyProperty

    public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
        nameof(Width),
        typeof(double),
        typeof(DiamondHand),
        new FrameworkPropertyMetadata(5.0, HandleWidthChanged));

    private static void HandleWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DiamondHand diamondHand)
            diamondHand.InvalidateLayout();
    }

    [Category("Appearance")]
    [DefaultValue(5.0)]
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
        typeof(DiamondHand),
        new FrameworkPropertyMetadata(6.0, HandleTailLengthChanged));

    private static void HandleTailLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DiamondHand diamondHand)
            diamondHand.InvalidateLayout();
    }

    [Category("Appearance")]
    [DefaultValue(6.0)]
    [Description("The hand's length of the tail as percentage from the clock's radius.")]
    public double TailLength
    {
        get => (double)GetValue(TailLengthProperty);
        set => SetValue(TailLengthProperty, value);
    }

    #endregion

    private PathGeometry diamondGeometry;

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (Width <= 0)
            return false;

        return base.OnRendering(context);
    }

    protected override void CalculateLayout(ClockDrawingContext context)
    {
        base.CalculateLayout(context);

        diamondGeometry = CreateDiamondGeometry(context);
    }

    private PathGeometry CreateDiamondGeometry(ClockDrawingContext context)
    {
        double radius = context.ClockRadius;
        double handLength = radius * (Length / 100.0);
        double tailLength = radius * (TailLength / 100.0);
        double halfWidth = radius * (Width / 100.0) / 2.0;

        PathFigure diamondFigure = new()
        {
            StartPoint = new Point(0, tailLength),
            IsClosed = true
        };

        diamondFigure.Segments.Add(new LineSegment(new Point(-halfWidth, 0), true));
        diamondFigure.Segments.Add(new LineSegment(new Point(0, -handLength), true));
        diamondFigure.Segments.Add(new LineSegment(new Point(halfWidth, 0), true));

        PathGeometry diamondGeometry = new();
        diamondGeometry.Figures.Add(diamondFigure);

        if (diamondGeometry.CanFreeze)
            diamondGeometry.Freeze();

        return diamondGeometry;
    }

    public override void DoRender(ClockDrawingContext context)
    {
        DrawingPlan.Create(context.DrawingContext)
            .WithTransform(() =>
            {
                double angleDegrees = CalculateHandAngle(context.Time);
                return new RotateTransform(angleDegrees, 0, 0);
            })
            .Draw(dc =>
            {
                context.DrawingContext.DrawGeometry(FillBrush, StrokePen, diamondGeometry);
            });
    }
}
