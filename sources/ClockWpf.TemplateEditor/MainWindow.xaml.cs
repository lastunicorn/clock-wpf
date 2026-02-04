using System.Windows;
using System.Windows.Input;

namespace DustInTheWind.ClockWpf.TemplateEditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const double ZoomIncrement = 0.1;
    private const double MinZoom = 0.1;
    private const double MaxZoom = 5.0;

    private bool isDragging = false;
    private Point lastMousePosition;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void AnalogClock_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (DataContext is not MainViewModel viewModel)
            return;

        double zoomDelta = e.Delta > 0 ? ZoomIncrement : -ZoomIncrement;
        double newZoom = viewModel.ZoomLevel + zoomDelta;

        newZoom = Math.Max(MinZoom, Math.Min(MaxZoom, newZoom));

        viewModel.ZoomLevel = newZoom;

        e.Handled = true;
    }

    private void ClockContainer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is not MainViewModel viewModel)
            return;

        isDragging = true;
        lastMousePosition = e.GetPosition(this);
        
        if (sender is UIElement element)
            element.CaptureMouse();

        e.Handled = true;
    }

    private void ClockContainer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (isDragging)
        {
            isDragging = false;
            
            if (sender is UIElement element)
                element.ReleaseMouseCapture();

            e.Handled = true;
        }
    }

    private void ClockContainer_MouseMove(object sender, MouseEventArgs e)
    {
        if (!isDragging || DataContext is not MainViewModel viewModel)
            return;

        Point currentPosition = e.GetPosition(this);
        double deltaX = currentPosition.X - lastMousePosition.X;
        double deltaY = currentPosition.Y - lastMousePosition.Y;

        viewModel.TranslateX += deltaX;
        viewModel.TranslateY += deltaY;

        lastMousePosition = currentPosition;

        e.Handled = true;
    }

    private void ResetClockView_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is not MainViewModel viewModel)
            return;

        viewModel.ZoomLevel = 1.0;
        viewModel.TranslateX = 0.0;
        viewModel.TranslateY = 0.0;
    }
}
