using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using DustInTheWind.ClockWpf.TimeProviders;

namespace DustInTheWind.ClockWpf.ClearClock;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool areControlsVisible = false;
    private readonly ApplicationState applicationState;

    public MainWindow(ApplicationState applicationState)
    {
        ArgumentNullException.ThrowIfNull(applicationState);

        InitializeComponent();

        DataContext = new MainViewModel(applicationState);

        this.applicationState = applicationState;
        applicationState.ClockTemplateChanged += HandleClockTemplateChanged;

        if (applicationState.ClockTemplate != null)
            AnalogClock1.ApplyClockTemplate(applicationState.ClockTemplate);

        LocalTimeProvider timeProvider = new();
        timeProvider.Start();

        AnalogClock1.TimeProvider = timeProvider;
    }

    private void HandleClockTemplateChanged(object sender, EventArgs e)
    {
        AnalogClock1.ApplyClockTemplate(applicationState.ClockTemplate);
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        DragMove();
    }

    private void AnalogClock_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        areControlsVisible = !areControlsVisible;

        Visibility newVisibility = areControlsVisible
            ? Visibility.Visible
            : Visibility.Collapsed;

        CloseButton.Visibility = newVisibility;
        ResizeGrip.Visibility = newVisibility;
        SettingsButton.Visibility = newVisibility;

        e.Handled = true;
    }

    private void SettingsButton_Click(object sender, RoutedEventArgs e)
    {
        SettingsPage.Visibility = Visibility.Visible;
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ResizeGrip_DragDelta(object sender, DragDeltaEventArgs e)
    {
        double minSize = 100;

        if (MainContainer.Width == minSize && e.HorizontalChange <= 0 &&
            MainContainer.Height == minSize && e.VerticalChange <= 0)
            return;

        double newWidth = MainContainer.Width + e.HorizontalChange;
        double newHeight = MainContainer.Height + e.VerticalChange;

        double size = Math.Min(newWidth, newHeight);
        size = Math.Max(size, minSize);

        MainContainer.Width = size;
        MainContainer.Height = size;
    }
}