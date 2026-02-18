using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Utils;

namespace DustInTheWind.ClockWpf.Shapes;

public class Ticks : RimBase
{
    #region Length DependencyProperty

    public static readonly DependencyProperty LengthProperty = DependencyProperty.Register(
        nameof(Length),
        typeof(double),
        typeof(Ticks),
        new FrameworkPropertyMetadata(5.0, HandleLengthChanged));

    private static void HandleLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Ticks ticks)
        {
            ticks.InvalidateCache();
            ticks.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Appearance")]
    [DefaultValue(5.0)]
    [Description("The length of the ticks, calculated as percentage from the clock's radius.")]
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
        new FrameworkPropertyMetadata(false, HandleRoundEndsChanged));

    private static void HandleRoundEndsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Ticks ticks)
        {
            ticks.InvalidateCache();
            ticks.OnChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ends of the clock ticks are rendered with rounded caps.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Indicates whether the ends of the clock ticks are rendered with rounded caps.")]
    public bool RoundEnds
    {
        get => (bool)GetValue(RoundEndsProperty);
        set => SetValue(RoundEndsProperty, value);
    }

    #endregion

    static Ticks()
    {
        AngleProperty.OverrideMetadata(typeof(Ticks), new FrameworkPropertyMetadata(6.0));
        OffsetAngleProperty.OverrideMetadata(typeof(Ticks), new FrameworkPropertyMetadata(6.0));
        DistanceFromEdgeProperty.OverrideMetadata(typeof(Ticks), new FrameworkPropertyMetadata(6.0));
        OrientationProperty.OverrideMetadata(typeof(Ticks), new FrameworkPropertyMetadata(RimItemOrientation.FaceIn));
    }

    private Pen strokePen;
    private Point startPoint;
    private Point endPoint;

    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        strokePen = CreateStrokePen(context);

        double actualLength = Length.RelativeTo(context.ClockRadius);
        double actualTipLength = RoundEnds
            ? StrokeThickness.RelativeTo(context.ClockRadius) / 2
            : 0;

        actualLength -= actualTipLength * 2;

        startPoint = new(0, -actualLength / 2);
        endPoint = new(0, actualLength / 2);
    }

    protected override void OnCreateStrokePen(CreateStrokePenEventArgs e)
    {
        base.OnCreateStrokePen(e);

        if (RoundEnds)
        {
            Pen pen = e.StrokePen;

            pen.StartLineCap = PenLineCap.Round;
            pen.EndLineCap = PenLineCap.Round;
        }
    }

    protected override void RenderItem(ClockDrawingContext context, int index)
    {
        context.DrawingContext.DrawLine(strokePen, startPoint, endPoint);
    }
}
