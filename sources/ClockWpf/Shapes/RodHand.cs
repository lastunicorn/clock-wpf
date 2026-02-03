using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Utils;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// A rod shaped clock hand. It has rounded ends and customizable width and tail length.
/// </summary>
public class RodHand : HandBase
{
    #region Width DependencyProperty

    public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
        nameof(Width),
        typeof(double),
        typeof(RodHand),
        new FrameworkPropertyMetadata(10.0, HandleWidthChanged));

    private static void HandleWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RodHand capsuleHand)
        {
            capsuleHand.InvalidateCache();
            capsuleHand.OnChanged(EventArgs.Empty);
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
        typeof(RodHand),
        new FrameworkPropertyMetadata(2.0, HandleTailLengthChange));

    private static void HandleTailLengthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RodHand capsuleHand)
        {
            capsuleHand.InvalidateCache();
            capsuleHand.OnChanged(EventArgs.Empty);
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

    private PathGeometry handGeometry;
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

        handGeometry = CreateHandGeometry(context);
        strokePen = CreateStrokePen(true);
    }

    private PathGeometry CreateHandGeometry(ClockDrawingContext context)
    {
        double radius = context.ClockRadius;
        double calculatedLength = Length.RelativeTo(radius);
        double calculatedTailLength = TailLength.RelativeTo(radius);
        double calculatedWidth = Width.RelativeTo(radius);
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
                HandAngle handAngle = new()
                {
                    Time = context.Time,
                    TimeComponent = TimeComponent,
                    ClockDirection = context.ClockDirection,
                    IntegralValue = IntegralValue
                };

                return new RotateTransform((double)handAngle, 0, 0);
            })
            .Draw(dc =>
            {
                context.DrawingContext.DrawGeometry(FillBrush, strokePen, handGeometry);
            });
    }
}
