using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// A simple line clock hand. It is ususally used for displaying seconds.
/// It has a customizable tail length, pin diameter and rounded edges.
/// </summary>
public class SimpleLineHand : HandBase
{
    #region TailLength DependencyProperty

    public static readonly DependencyProperty TailLengthProperty = DependencyProperty.Register(
        nameof(TailLength),
        typeof(double),
        typeof(SimpleLineHand),
        new FrameworkPropertyMetadata(0.0, HandleTailLengthChanged));

    private static void HandleTailLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is SimpleLineHand simpleHand)
            simpleHand.InvalidateCache();
    }

    [Category("Appearance")]
    [DefaultValue(0.0)]
    [Description("The hand's length of the tail as percentage from the clock's radius.")]
    public double TailLength
    {
        get => (double)GetValue(TailLengthProperty);
        set => SetValue(TailLengthProperty, value);
    }

    #endregion

    #region PinDiameter DependencyProperty

    public static readonly DependencyProperty PinDiameterProperty = DependencyProperty.Register(
        nameof(PinDiameter),
        typeof(double),
        typeof(SimpleLineHand),
        new FrameworkPropertyMetadata(4.0, HandlePinDiameterChanged));

    private static void HandlePinDiameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is SimpleLineHand simpleHand)
            simpleHand.InvalidateCache();
    }

    [Category("Appearance")]
    [DefaultValue(4.0)]
    [Description("The diameter of the pin as percentage from the clock's radius.")]
    public double PinDiameter
    {
        get => (double)GetValue(PinDiameterProperty);
        set => SetValue(PinDiameterProperty, value);
    }

    #endregion

    private Point startHandPoint;
    private Point endHandPoint;
    private bool hasPin;
    private double calculatedPinRadius;

    protected override Pen CreateStrokePen(bool freeze = true)
    {
        Pen pen = base.CreateStrokePen(false);

        if (pen != null)
        {
            pen.StartLineCap = PenLineCap.Round;
            pen.EndLineCap = PenLineCap.Round;

            if (freeze && pen.CanFreeze)
                pen.Freeze();
        }

        return pen;
    }

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (StrokeThickness <= 0 || StrokeBrush == null)
            return false;

        return base.OnRendering(context);
    }

    protected override void CalculateCache(ClockDrawingContext context)
    {
        base.CalculateCache(context);

        // Hand

        double radius = context.ClockRadius;
        double calculatedLength = radius * (Length / 100);
        double calculatedTailLength = radius * (TailLength / 100);
        double calculatedStrokeThickness = radius * (StrokeThickness / 100);
        double calculatedTipLength = StrokeThickness / 2;

        startHandPoint = new(0, calculatedTailLength - calculatedTipLength);
        endHandPoint = new(0, -calculatedLength + calculatedTipLength);

        // Pin

        hasPin = PinDiameter > 0;
        if (hasPin)
        {
            double calculatedPinDiameter = radius * (PinDiameter / 100.0);
            calculatedPinRadius = calculatedPinDiameter / 2;
        }
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
                dc.DrawLine(StrokePen, startHandPoint, endHandPoint);

                if (hasPin)
                {
                    Point center = new(0, 0);
                    dc.DrawEllipse(StrokeBrush, null, center, calculatedPinRadius, calculatedPinRadius);
                }
            });
    }
}
