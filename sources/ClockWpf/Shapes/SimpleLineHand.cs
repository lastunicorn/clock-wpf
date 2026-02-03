using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Utils;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// A simple line clock hand. It is ususally used for displaying seconds.
/// It has a customizable tail length, pin diameter and rounded edges.
/// </summary>
public class SimpleLineHand : HandBase
{
    #region RoundEnds DependencyProperty

    public static readonly DependencyProperty RoundEndsProperty = DependencyProperty.Register(
        nameof(RoundEnds),
        typeof(bool),
        typeof(SimpleLineHand),
        new FrameworkPropertyMetadata(false, HandleRoundEndsChanged));

    private static void HandleRoundEndsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is SimpleLineHand simpleLineHand)
        {
            simpleLineHand.InvalidateCache();
            simpleLineHand.OnChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ends of the clock's hands are rendered with rounded caps.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Indicates whether the ends of the clock's hands are rendered with rounded caps.")]
    public bool RoundEnds
    {
        get => (bool)GetValue(RoundEndsProperty);
        set => SetValue(RoundEndsProperty, value);
    }

    #endregion

    #region TailLength DependencyProperty

    public static readonly DependencyProperty TailLengthProperty = DependencyProperty.Register(
        nameof(TailLength),
        typeof(double),
        typeof(SimpleLineHand),
        new FrameworkPropertyMetadata(0.0, HandleTailLengthChanged));

    private static void HandleTailLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is SimpleLineHand simpleHand)
        {
            simpleHand.InvalidateCache();
            simpleHand.OnChanged(EventArgs.Empty);
        }
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
        {
            simpleHand.InvalidateCache();
            simpleHand.OnChanged(EventArgs.Empty);
        }
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

    private Point startPoint;
    private Point endPoint;
    private bool hasPin;
    private double calculatedPinRadius;
    private Pen strokePen;

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
        double calculatedLength = Length.RelativeTo(radius);
        double calculatedTailLength = TailLength.RelativeTo(radius);
        double calculatedTipLength = RoundEnds
            ? StrokeThickness / 2
            : 0;

        startPoint = new(0, calculatedTailLength - calculatedTipLength);
        endPoint = new(0, -calculatedLength + calculatedTipLength);

        // Pin

        hasPin = PinDiameter > 0;
        if (hasPin)
        {
            double calculatedPinDiameter = PinDiameter.RelativeTo(radius);
            calculatedPinRadius = calculatedPinDiameter / 2;
        }

        // Pen
        strokePen = CreateStrokePen();
    }

    private Pen CreateStrokePen()
    {
        if (RoundEnds)
        {
            Pen pen = CreateStrokePen(false);

            if (pen != null)
            {
                pen.StartLineCap = PenLineCap.Round;
                pen.EndLineCap = PenLineCap.Round;

                if (pen.CanFreeze)
                    pen.Freeze();
            }

            return pen;
        }
        else
        {
            return CreateStrokePen(true);
        }
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
                dc.DrawLine(strokePen, startPoint, endPoint);

                if (hasPin)
                {
                    Point center = new(0, 0);
                    dc.DrawEllipse(StrokeBrush, null, center, calculatedPinRadius, calculatedPinRadius);
                }
            });
    }
}
