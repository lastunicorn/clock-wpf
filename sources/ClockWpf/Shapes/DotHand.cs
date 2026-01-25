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
        new FrameworkPropertyMetadata(5.0));

    [Category("Appearance")]
    [DefaultValue(5.0)]
    [Description("The radius of the dot.")]
    public double Radius
    {
        get => (double)GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    #endregion

    private double actualRadius;
    private Point center;

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (Radius <= 0)
            return false;

        return base.OnRendering(context);
    }

    protected override void CalculateLayout(ClockDrawingContext context)
    {
        base.CalculateLayout(context);

        double clockRadius = context.ClockRadius;
        double actualLength = clockRadius * (Length / 100.0);
        actualRadius = clockRadius * (Radius / 100.0);

        center = new Point(0, -actualLength);
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
                dc.DrawEllipse(FillBrush, StrokePen, center, actualRadius, actualRadius);
            });
    }
}
