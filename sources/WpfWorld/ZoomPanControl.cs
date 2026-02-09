using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DustInTheWind.WpfWorld;

public class ZoomPanControl : ContentControl
{
    #region ResetViewCommand DependencyProperty

    public static readonly DependencyProperty ResetViewCommandProperty = DependencyProperty.Register(
        nameof(ResetViewCommand),
        typeof(ICommand),
        typeof(ZoomPanControl));

    internal ICommand ResetViewCommand
    {
        get => (ICommand)GetValue(ResetViewCommandProperty);
        private set => SetValue(ResetViewCommandProperty, value);
    }

    #endregion

    #region ZoomValue DependencyProperty

    public static readonly DependencyProperty ZoomValueProperty = DependencyProperty.Register(
        nameof(ZoomValue),
        typeof(double),
        typeof(ZoomPanControl),
        new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, HandleZoomValueChanged));

    private static void HandleZoomValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ZoomPanControl zoomPanControl)
        {
            double newZoomValue = (double)e.NewValue;

            if (zoomPanControl.scaleTransform != null)
            {
                zoomPanControl.scaleTransform.ScaleX = newZoomValue;
                zoomPanControl.scaleTransform.ScaleY = newZoomValue;
            }
        }
    }

    public double ZoomValue
    {
        get => (double)GetValue(ZoomValueProperty);
        set => SetValue(ZoomValueProperty, value);
    }

    #endregion

    #region Location DependencyProperty

    public static readonly DependencyProperty LocationProperty = DependencyProperty.Register(
        nameof(Location),
        typeof(Point),
        typeof(ZoomPanControl),
        new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, HandleLocationChanged));

    private static void HandleLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ZoomPanControl zoomPanControl)
        {
            Point newLocation = (Point)e.NewValue;

            if (zoomPanControl.translateTransform != null)
            {
                zoomPanControl.translateTransform.X = newLocation.X;
                zoomPanControl.translateTransform.Y = newLocation.Y;
            }
        }
    }

    public Point Location
    {
        get => (Point)GetValue(LocationProperty);
        set => SetValue(LocationProperty, value);
    }

    #endregion

    static ZoomPanControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoomPanControl), new FrameworkPropertyMetadata(typeof(ZoomPanControl)));
    }

    private const double ZoomIncrement = 0.1;
    private const double MinZoom = 0.1;
    private const double MaxZoom = 5.0;

    private bool isDragging;
    private Point lastMousePosition;
    private FrameworkElement containerElement;
    private FrameworkElement contentElement;
    private ScaleTransform scaleTransform;
    private TranslateTransform translateTransform;

    public ZoomPanControl()
    {
        ResetViewCommand = new ResetViewCommand(this);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        isDragging = false;
        lastMousePosition = new Point(0, 0);
        containerElement = null;
        contentElement = null;
        scaleTransform = null;
        translateTransform = null;
        ZoomValue = 1.0;

        if (GetTemplateChild("PART_Content") is FrameworkElement newContentElement)
        {
            scaleTransform = new ScaleTransform(ZoomValue, ZoomValue);
            translateTransform = new TranslateTransform(0.0, 0.0);

            TransformGroup transformGroup = new();

            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);

            newContentElement.RenderTransform = transformGroup;

            contentElement = newContentElement;
        }

        if (containerElement != null)
        {
            containerElement.MouseWheel -= HandleMouseWheel;
            containerElement.MouseLeftButtonDown -= HandleMouseLeftButtonDown;
            containerElement.MouseLeftButtonUp -= HandleMouseLeftButtonUp;
            containerElement.MouseMove -= HandleMouseMove;
        }

        if (GetTemplateChild("PART_Container") is FrameworkElement newContainerElement)
        {
            newContainerElement.MouseWheel += HandleMouseWheel;
            newContainerElement.MouseLeftButtonDown += HandleMouseLeftButtonDown;
            newContainerElement.MouseLeftButtonUp += HandleMouseLeftButtonUp;
            newContainerElement.MouseMove += HandleMouseMove;

            containerElement = newContainerElement;
        }
    }

    private void HandleMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (sender is not FrameworkElement element)
            return;

        if (scaleTransform == null || translateTransform == null)
            return;

        Point oldLocation = Location;

        double oldZoom = ZoomValue;
        double zoomDelta = e.Delta > 0 ? ZoomIncrement : -ZoomIncrement;
        double newZoom = oldZoom + zoomDelta;

        newZoom = Math.Max(MinZoom, Math.Min(MaxZoom, newZoom));

        Point mousePosition = e.GetPosition(element);

        double offsetX = (element.ActualWidth - contentElement.ActualWidth) / 2;
        double offsetY = (element.ActualHeight - contentElement.ActualHeight) / 2;

        double mouseRelativeToTargetX = mousePosition.X - offsetX;
        double mouseRelativeToTargetY = mousePosition.Y - offsetY;

        double zoomFactor = newZoom / oldZoom;

        double x = mouseRelativeToTargetX - (mouseRelativeToTargetX - oldLocation.X) * zoomFactor;
        double y = mouseRelativeToTargetY - (mouseRelativeToTargetY - oldLocation.Y) * zoomFactor;

        Location = new Point(x, y);
        ZoomValue = newZoom;

        e.Handled = true;
    }

    private void HandleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not FrameworkElement element)
            return;

        isDragging = true;
        lastMousePosition = e.GetPosition(element);

        element.CaptureMouse();

        e.Handled = true;
    }

    private void HandleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (sender is not FrameworkElement element)
            return;

        if (isDragging)
        {
            isDragging = false;
            element.ReleaseMouseCapture();
            e.Handled = true;
        }
    }

    private void HandleMouseMove(object sender, MouseEventArgs e)
    {
        if (sender is not FrameworkElement element)
            return;

        if (!isDragging)
            return;

        if (translateTransform == null)
            return;

        Point oldLocation = Location;

        Point currentPosition = e.GetPosition(element);
        double deltaX = currentPosition.X - lastMousePosition.X;
        double deltaY = currentPosition.Y - lastMousePosition.Y;

        double x = oldLocation.X + deltaX;
        double y = oldLocation.Y + deltaY;

        Location = new Point(x, y);

        lastMousePosition = currentPosition;

        e.Handled = true;
    }

    internal void Reset()
    {
        ZoomValue = 1.0;
        Location = new Point(0, 0);
    }
}
