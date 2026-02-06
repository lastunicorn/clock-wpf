using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.Shapes;

/// <summary>
/// Provides an abstract base class for clock hands.
/// The hand displays a specific time component, such as hours, minutes, or
/// seconds, within a graphical user interface.
/// </summary>
/// <remarks>
/// Inherit from this class to implement custom clock hand visuals that represent a particular component
/// of time. The class exposes properties to control the hand's length, the time value it displays, and which time
/// component is visualized.</remarks>
public abstract class HandBase : Shape, IHand
{
    #region Length DependencyProperty

    public static readonly DependencyProperty LengthProperty = DependencyProperty.Register(
        nameof(Length),
        typeof(double),
        typeof(HandBase),
        new FrameworkPropertyMetadata(95.0, HandleLengthChanged));

    private static void HandleLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is HandBase hand)
        {
            hand.InvalidateCache();
            hand.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Appearance")]
    [DefaultValue(95.0)]
    [Description("The length of the hand from the pin to the its top as percentage from the radius of the clock.")]
    public double Length
    {
        get => (double)GetValue(LengthProperty);
        set => SetValue(LengthProperty, value);
    }

    #endregion

    #region TimeComponent DependencyProperty

    public static readonly DependencyProperty TimeComponentProperty = DependencyProperty.Register(
        nameof(TimeComponent),
        typeof(TimeComponent),
        typeof(HandBase),
        new FrameworkPropertyMetadata(TimeComponent.Second, HandleTimeComponentChanged));

    private static void HandleTimeComponentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is HandBase hand)
        {
            hand.InvalidateCache();
            hand.OnChanged(EventArgs.Empty);
        }
    }

    [DefaultValue(typeof(TimeComponent), "None")]
    [Category("Behavior")]
    [Description("Specifies the component that is displayed from the time value.")]
    public TimeComponent TimeComponent
    {
        get => (TimeComponent)GetValue(TimeComponentProperty);
        set => SetValue(TimeComponentProperty, value);
    }

    #endregion

    #region IntegralValue DependencyProperty

    public static readonly DependencyProperty IntegralValueProperty = DependencyProperty.Register(
        nameof(IntegralValue),
        typeof(bool),
        typeof(HandBase),
        new FrameworkPropertyMetadata(false, HandleIntegralValueChanged));

    private static void HandleIntegralValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is HandBase hand)
        {
            hand.InvalidateCache();
            hand.OnChanged(EventArgs.Empty);
        }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Specifies if the hand will display only the integral part of the value.")]
    public bool IntegralValue
    {
        get => (bool)GetValue(IntegralValueProperty);
        set => SetValue(IntegralValueProperty, value);
    }

    #endregion

    protected override bool OnRendering(ClockDrawingContext context)
    {
        if (Length <= 0)
            return false;

        if (TimeComponent == TimeComponent.None)
            return false;

        return base.OnRendering(context);
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
                DoRenderHand(context);
            });
    }

    protected abstract void DoRenderHand(ClockDrawingContext context);
}
