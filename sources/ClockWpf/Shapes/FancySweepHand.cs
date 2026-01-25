using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// Draws a sweep hand as a line with a circle near its top.
/// </summary>
public class FancySweepHand : HandBase
{
    #region CircleRadius DependencyProperty

    public static readonly DependencyProperty CircleRadiusProperty = DependencyProperty.Register(
        nameof(CircleRadius),
        typeof(double),
        typeof(FancySweepHand),
        new FrameworkPropertyMetadata(7.0, HandleCircleRadiusChanged));

    private static void HandleCircleRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FancySweepHand fancySweepHand)
            fancySweepHand.InvalidateLayout();
    }

    [Category("Appearance")]
    [DefaultValue(7.0)]
    [Description("The radius of the circle from the middle (or not so middle) of the hand.")]
    public double CircleRadius
    {
        get => (double)GetValue(CircleRadiusProperty);
        set => SetValue(CircleRadiusProperty, value);
    }

    #endregion

    #region CircleOffset DependencyProperty

    public static readonly DependencyProperty CircleOffsetProperty = DependencyProperty.Register(
        nameof(CircleOffset),
        typeof(double),
        typeof(FancySweepHand),
        new FrameworkPropertyMetadata(24.0, HandleCircleOffsetChanged));

    private static void HandleCircleOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FancySweepHand fancySweepHand)
            fancySweepHand.InvalidateLayout();
    }

    [Category("Appearance")]
    [DefaultValue(24.0)]
    [Description("The offset position of the center of the circle from the top of the hand.")]
    public double CircleOffset
    {
        get => (double)GetValue(CircleOffsetProperty);
        set => SetValue(CircleOffsetProperty, value);
    }

    #endregion

    #region TailLength DependencyProperty

    public static readonly DependencyProperty TailLengthProperty = DependencyProperty.Register(
        nameof(TailLength),
        typeof(double),
        typeof(FancySweepHand),
        new FrameworkPropertyMetadata(14.0, HandleTailLengthChanged));

    private static void HandleTailLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FancySweepHand fancySweepHand)
            fancySweepHand.InvalidateLayout();
    }

    [Category("Appearance")]
    [DefaultValue(14.0)]
    [Description("The length of the tail of the hand.")]
    public double TailLength
    {
        get => (double)GetValue(TailLengthProperty);
        set => SetValue(TailLengthProperty, value);
    }

    #endregion

    private Point mainLineStartPoint;
    private Point mainLineEndPoint;
    private Point circleCenter;
    private double circleRadius;
    private Point tipLineStartPoint;
    private Point tipLineEndPoint;

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (StrokePen == null)
            return false;

        return base.OnRendering(context);
    }

    protected override void CalculateLayout(ClockDrawingContext context)
    {
        base.CalculateLayout(context);

        double radius = context.ClockRadius;
        double calculatedLength = radius * (Length / 100.0);
        double calculatedCircleOffset = radius * (CircleOffset / 100.0);
        double calculatedCircleRadius = radius * (CircleRadius / 100.0);
        double calculatedTailLength = radius * (TailLength / 100.0);

        double calculatedCircleCenterY = -calculatedLength + calculatedCircleOffset;

        // Main Line

        mainLineStartPoint = new Point(0, calculatedTailLength);
        mainLineEndPoint = new Point(0, calculatedCircleCenterY + calculatedCircleRadius);

        // Circle

        circleCenter = new Point(0, calculatedCircleCenterY);
        circleRadius = calculatedCircleRadius;

        // Tip Line

        tipLineStartPoint = new Point(0, calculatedCircleCenterY - calculatedCircleRadius);
        tipLineEndPoint = new Point(0, -calculatedLength);
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
                dc.DrawLine(StrokePen, mainLineStartPoint, mainLineEndPoint);
                dc.DrawEllipse(null, StrokePen, circleCenter, circleRadius, circleRadius);
                dc.DrawLine(StrokePen, tipLineStartPoint, tipLineEndPoint);
            });
    }
}
