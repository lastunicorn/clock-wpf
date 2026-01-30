using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// A clock hand rendered as a dot (disk) at a specified distance from the center.
/// </summary>
public class DotHand : HandBase
{
    #region Radius DependencyProperty

    public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
        nameof(Radius),
        typeof(double),
        typeof(DotHand),
        new FrameworkPropertyMetadata(5.0, HandleRadiusChanged));

    private static void HandleRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DotHand dotHand)
            dotHand.InvalidateCache();
    }

    [Category("Appearance")]
    [DefaultValue(5.0)]
    [Description("The radius of the dot.")]
    public double Radius
    {
        get => (double)GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    #endregion

    private double calculatedDotRadius;
    private Point calculatedDotCenter;

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (Radius <= 0)
            return false;

        return base.OnRendering(context);
    }

    /// <remarks>
    /// The <see cref="Length"/> value, for the <see cref="DotHand"/>, is the distance from the
    /// center of the clock to the center of the hand's center.
    /// </remarks>
    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        double clockRadius = context.ClockRadius;
        
        double actualLength = clockRadius * (Length / 100.0);
        calculatedDotCenter = new Point(0, -actualLength);
        
        calculatedDotRadius = clockRadius * (Radius / 100.0);
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
                dc.DrawEllipse(FillBrush, StrokePen, calculatedDotCenter, calculatedDotRadius, calculatedDotRadius);
            });
    }
}
