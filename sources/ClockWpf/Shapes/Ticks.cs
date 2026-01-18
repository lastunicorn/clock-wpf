using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

public class Ticks : RimBase
{
    static Ticks()
    {
        AngleProperty.OverrideMetadata(typeof(Ticks), new FrameworkPropertyMetadata(6.0));
        OffsetAngleProperty.OverrideMetadata(typeof(Ticks), new FrameworkPropertyMetadata(6.0));
        DistanceFromEdgeProperty.OverrideMetadata(typeof(Ticks), new FrameworkPropertyMetadata(6.0));
        OrientationProperty.OverrideMetadata(typeof(Ticks), new FrameworkPropertyMetadata(RimItemOrientation.FaceCenter));
    }

    #region Length DependencyProperty

    public static readonly DependencyProperty LengthProperty = DependencyProperty.Register(
        nameof(Length),
        typeof(double),
        typeof(Ticks),
        new FrameworkPropertyMetadata(5.0));

    [Category("Appearance")]
    [DefaultValue(5.0)]
    [Description("The length of the ticks as a percentage from the clock's radius.")]
    public double Length
    {
        get => (double)GetValue(LengthProperty);
        set => SetValue(LengthProperty, value);
    }

    #endregion

    private double radius;

    protected override bool OnRendering(ClockDrawingContext context)
    {
        radius = context.ClockRadius;
        return base.OnRendering(context);
    }

    protected override void RenderItem(DrawingContext drawingContext, int index)
    {
        if (StrokePen == null)
            return;

        double actualLength = radius * Length / 100.0;

        Point startPoint = new(0, -actualLength / 2);
        Point endPoint = new(0, actualLength / 2);

        drawingContext.DrawLine(StrokePen, startPoint, endPoint);
    }
}
