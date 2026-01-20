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

    #region RoundEnds DependencyProperty

    public static readonly DependencyProperty RoundEndsProperty = DependencyProperty.Register(
        nameof(RoundEnds),
        typeof(bool),
        typeof(Ticks),
        new FrameworkPropertyMetadata(false));

    public bool RoundEnds
    {
        get => (bool)GetValue(RoundEndsProperty);
        set => SetValue(RoundEndsProperty, value);
    }

    #endregion

    private double radius;

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (StrokePen == null)
            return false;

        radius = context.ClockRadius;
        return base.OnRendering(context);
    }

    protected override Pen CreateStrokePen()
    {
        Pen pen = base.CreateStrokePen();

        if (RoundEnds)
        {
            pen.StartLineCap = PenLineCap.Round;
            pen.EndLineCap = PenLineCap.Round;
        }

        return pen;
    }

    protected override void RenderItem(DrawingContext drawingContext, int index)
    {
        double actualLength = radius * Length / 100.0;

        Point startPoint = new(0, -actualLength / 2);
        Point endPoint = new(0, actualLength / 2);

        drawingContext.DrawLine(StrokePen, startPoint, endPoint);
    }
}
