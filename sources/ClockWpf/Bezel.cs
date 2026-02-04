using System.Windows;
using System.Windows.Controls;

namespace DustInTheWind.ClockWpf;

public class Bezel : ContentControl
{
    #region ClockRadius DependencyProperty

    public static readonly DependencyProperty ClockRadiusProperty = DependencyProperty.Register(
        nameof(ClockRadius),
        typeof(double),
        typeof(Bezel),
        new PropertyMetadata(50.0, HandleClockRadiusChanged));

    private static void HandleClockRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Bezel bezel)
            bezel.InvalidateVisual();
    }

    /// <summary>
    /// Gets or sets the radius of the clock face, in device-independent units (DIPs).
    /// </summary>
    public double ClockRadius
    {
        get => (double)GetValue(ClockRadiusProperty);
        set => SetValue(ClockRadiusProperty, value);
    }

    #endregion

    #region ClockLocation DependencyProperty

    public static readonly DependencyProperty ClockLocationProperty = DependencyProperty.Register(
        nameof(ClockLocation),
        typeof(Point),
        typeof(Bezel),
        new PropertyMetadata(new Point(50, 50), HandleClockLocationChanged));

    private static void HandleClockLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Bezel bezel)
            bezel.InvalidateVisual();
    }

    /// <summary>
    /// Gets or sets the location of the clock's center within the Bezel control.
    /// </summary>
    public Point ClockLocation
    {
        get => (Point)GetValue(ClockLocationProperty);
        set => SetValue(ClockLocationProperty, value);
    }

    #endregion

    static Bezel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Bezel), new FrameworkPropertyMetadata(typeof(Bezel)));
    }
}
