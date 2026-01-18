using System.ComponentModel;
using System.Windows;

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
        new FrameworkPropertyMetadata(95.0));


    [Category("Appearance")]
    [DefaultValue(95.0)]
    [Description("The length of the hand from the pin to the its top as percentage from the radius of the clock.")]
    public double Length
    {
        get => (double)GetValue(LengthProperty);
        set => SetValue(LengthProperty, value);
    }

    #endregion

    #region ComponentToDisplay DependencyProperty

    public static readonly DependencyProperty ComponentToDisplayProperty = DependencyProperty.Register(
        nameof(ComponentToDisplay),
        typeof(TimeComponent),
        typeof(HandBase),
        new FrameworkPropertyMetadata(TimeComponent.Second));

    [DefaultValue(typeof(TimeComponent), "None")]
    [Category("Behavior")]
    [Description("Specifies the component that is displayed from the time value.")]
    public TimeComponent ComponentToDisplay
    {
        get => (TimeComponent)GetValue(ComponentToDisplayProperty);
        set => SetValue(ComponentToDisplayProperty, value);
    }

    #endregion

    #region IntegralValue DependencyProperty

    public static readonly DependencyProperty IntegralValueProperty = DependencyProperty.Register(
        nameof(IntegralValue),
        typeof(bool),
        typeof(HandBase),
        new FrameworkPropertyMetadata(false));

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Specifies if the hand will display only the integral part of the value.")]
    public bool IntegralValue
    {
        get => (bool)GetValue(IntegralValueProperty);
        set => SetValue(IntegralValueProperty, value);
    }

    #endregion

    protected double CalculateHandAngle(TimeSpan time)
    {
        if (IntegralValue)
        {
            return ComponentToDisplay switch
            {
                TimeComponent.Hour => (time.Hours % 12) * 30.0,
                TimeComponent.Minute => time.Minutes * 6.0,
                TimeComponent.Second => time.Seconds * 6.0,
                _ => 0
            };
        }

        return ComponentToDisplay switch
        {
            TimeComponent.Hour => (time.TotalHours % 12 / 12) * 360.0,
            TimeComponent.Minute => (time.TotalMinutes % 60 / 60) * 360.0,
            TimeComponent.Second => (time.TotalSeconds % 60 / 60) * 360.0,
            _ => 0
        };
    }
}
