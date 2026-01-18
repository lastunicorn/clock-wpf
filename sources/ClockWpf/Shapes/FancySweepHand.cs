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
        new FrameworkPropertyMetadata(7.0));

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
        new FrameworkPropertyMetadata(24.0));

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
        new FrameworkPropertyMetadata(14.0));

    [Category("Appearance")]
    [DefaultValue(14.0)]
    [Description("The length of the tail of the hand.")]
    public double TailLength
    {
        get => (double)GetValue(TailLengthProperty);
        set => SetValue(TailLengthProperty, value);
    }

    #endregion

    public override void DoRender(ClockDrawingContext context)
    {
        context.DrawingContext.CreateDrawingPlan()
            .WithTransform(() =>
            {
                double angleDegrees = CalculateHandAngle(context.Time);
                return new RotateTransform(angleDegrees, 0, 0);
            })
            .Draw(dc =>
            {
                if (StrokePen == null)
                    return;

                double radius = context.ClockRadius;
                DrawHandParts(dc, radius);
            });
    }

    private void DrawHandParts(DrawingContext drawingContext, double radius)
    {
        double actualLength = radius * (Length / 100.0);
        double actualCircleOffset = radius * (CircleOffset / 100.0);
        double actualCircleRadius = radius * (CircleRadius / 100.0);
        double actualTailLength = radius * (TailLength / 100.0);

        double actualCircleCenterY = -actualLength + actualCircleOffset;

        // Base Line

        Point baseLineStartPoint = new(0, actualTailLength);
        Point baseLineEndPoint = new(0, actualCircleCenterY + actualCircleRadius);
        drawingContext.DrawLine(StrokePen, baseLineStartPoint, baseLineEndPoint);

        // Circle

        Point circleCenter = new(0, actualCircleCenterY);
        drawingContext.DrawEllipse(null, StrokePen, circleCenter, actualCircleRadius, actualCircleRadius);

        Point tipLineStartPoint = new(0, actualCircleCenterY - actualCircleRadius);
        Point tipLineEndPoint = new(0, -actualLength);
        drawingContext.DrawLine(StrokePen, tipLineStartPoint, tipLineEndPoint);
    }
}
