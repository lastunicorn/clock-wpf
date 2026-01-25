using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using DustInTheWind.ClockAvalonia.Shapes;
using DustInTheWind.ClockAvalonia.TimeProviders;

namespace DustInTheWind.ClockAvalonia.ClearClock.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeClock();
    }

    private void InitializeClock()
    {
        AnalogClock clock = this.FindControl<AnalogClock>("AnalogClock");

        if (clock != null)
        {
            clock.Shapes.Add(new FlatBackground
            {
                FillBrush = Brushes.CornflowerBlue,
            });

            LocalTimeProvider timeProvider = new();
            timeProvider.Start();

            clock.TimeProvider = timeProvider;

            clock.PointerPressed += HandleClockPointerPressed;
        }
    }

    private void HandleClockPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        PointerPointProperties properties = e.GetCurrentPoint(this).Properties;

        if (properties.IsLeftButtonPressed)
        {
            BeginMoveDrag(e);
        }
    }
}