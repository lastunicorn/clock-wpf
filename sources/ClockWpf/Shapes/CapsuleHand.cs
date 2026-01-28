using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// A capsule shaped clock hand with rounded ends, customizable width and tail length.
/// </summary>
public class CapsuleHand : HandBase
{
    #region Width DependencyProperty

    public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
        nameof(Width),
        typeof(double),
        typeof(CapsuleHand),
        new FrameworkPropertyMetadata(4.0, HandleWidthChanged));

    private static void HandleWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CapsuleHand capsuleHand)
            capsuleHand.InvalidateLayout();
    }

    [Category("Appearance")]
    [DefaultValue(4.0)]
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
        typeof(CapsuleHand),
        new FrameworkPropertyMetadata(2.0, HandleTailLengthChange));

    private static void HandleTailLengthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CapsuleHand capsuleHand)
            capsuleHand.InvalidateLayout();
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

    private PathGeometry handGeometry;

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (Width <= 0)
            return false;

        return base.OnRendering(context);
    }

    protected override void CalculateLayout(ClockDrawingContext context)
    {
        base.CalculateLayout(context);

        handGeometry = CreateHandGeometry(context);
    }

    private PathGeometry CreateHandGeometry(ClockDrawingContext context)
    {
        double radius = context.ClockRadius;
        double calculatedLength = radius * (Length / 100.0);
        double calculatedTailLength = radius * (TailLength / 100.0);
        double calculatedWidth = radius * (Width / 100.0);
        double halfWidth = calculatedWidth / 2.0;

        double topY = -calculatedLength + halfWidth;
        double bottomY = calculatedTailLength - halfWidth;

        PathFigure capsuleFigure = new()
        {
            StartPoint = new Point(-halfWidth, topY),
            IsClosed = true
        };

        // Top semicircle (pointing upward)
        capsuleFigure.Segments.Add(new ArcSegment(
            new Point(halfWidth, topY),
            new Size(halfWidth, halfWidth),
            0,
            false,
            SweepDirection.Clockwise,
            true));

        // Right side of the rectangle
        capsuleFigure.Segments.Add(new LineSegment(new Point(halfWidth, bottomY), true));

        // Bottom semicircle (pointing downward)
        capsuleFigure.Segments.Add(new ArcSegment(
            new Point(-halfWidth, bottomY),
            new Size(halfWidth, halfWidth),
            0,
            false,
            SweepDirection.Clockwise,
            true));

        // Left side of the rectangle (closes back to start point)
        capsuleFigure.Segments.Add(new LineSegment(new Point(-halfWidth, topY), true));

        PathGeometry handGeometry = new();
        handGeometry.Figures.Add(capsuleFigure);

        if (handGeometry.CanFreeze)
            handGeometry.Freeze();

        return handGeometry;
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
                context.DrawingContext.DrawGeometry(FillBrush, StrokePen, handGeometry);
            });
    }
}
