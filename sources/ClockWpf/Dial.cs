using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.Performance;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf;

public class Dial : Canvas
{
    #region PerformanceMeter DependencyProperty

    public static readonly DependencyProperty PerformanceMeterProperty = DependencyProperty.Register(
        nameof(PerformanceMeter),
        typeof(PerformanceMeter),
        typeof(Dial));

    public PerformanceMeter PerformanceMeter
    {
        get => (PerformanceMeter)GetValue(PerformanceMeterProperty);
        set => SetValue(PerformanceMeterProperty, value);
    }

    #endregion

    #region Shapes DependencyProperty

    public static readonly DependencyProperty ShapesProperty = DependencyProperty.Register(
        nameof(Shapes),
        typeof(ObservableCollection<Shape>),
        typeof(Dial),
        new PropertyMetadata(null, HandleShapesChanged));

    private static void HandleShapesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Dial dial)
            return;

        if (e.OldValue is ObservableCollection<Shape> oldCollection)
        {
            oldCollection.CollectionChanged -= dial.HandleCollectionChanged;

            foreach (Shape shape in oldCollection)
                shape.Changed -= dial.HandleShapeChanged;
        }

        if (e.NewValue is ObservableCollection<Shape> newCollection)
        {
            newCollection.CollectionChanged += dial.HandleCollectionChanged;

            foreach (Shape shape in newCollection)
                shape.Changed += dial.HandleShapeChanged;
        }

        dial.InvalidateVisual();
    }

    private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                foreach (Shape shape in e.NewItems)
                    shape.Changed += HandleShapeChanged;
                break;

            case NotifyCollectionChangedAction.Remove:
                foreach (Shape shape in e.OldItems)
                    shape.Changed -= HandleShapeChanged;
                break;

            case NotifyCollectionChangedAction.Replace:
                foreach (Shape shape in e.OldItems)
                    shape.Changed -= HandleShapeChanged;
                foreach (Shape shape in e.NewItems)
                    shape.Changed += HandleShapeChanged;
                break;

            case NotifyCollectionChangedAction.Reset:
                foreach (Shape shape in Shapes)
                    shape.Changed -= HandleShapeChanged;
                break;

            default:
                break;
        }

        InvalidateVisual();
    }

    private void HandleShapeChanged(object sender, EventArgs e)
    {
        InvalidateVisual();
    }

    public ObservableCollection<Shape> Shapes
    {
        get => (ObservableCollection<Shape>)GetValue(ShapesProperty);
        set => SetValue(ShapesProperty, value);
    }

    #endregion

    #region Movement DependencyProperty

    public static readonly DependencyProperty MovementProperty = DependencyProperty.Register(
        nameof(Movement),
        typeof(IMovement),
        typeof(Dial),
        new PropertyMetadata(null, HandleMovementChanged));

    private static void HandleMovementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Dial dial)
            return;

        if (e.OldValue is IMovement oldMovement)
            oldMovement.Tick -= dial.HandleTick;

        if (e.NewValue is IMovement newMovement)
        {
            newMovement.Tick += dial.HandleTick;
            dial.InvalidateVisual();
        }
    }

    private void HandleTick(object sender, TickEventArgs e)
    {
        if (Dispatcher.HasShutdownStarted || Dispatcher.HasShutdownFinished)
            return;

        try
        {
            Dispatcher.Invoke(() =>
            {
                InvalidateVisual();
            });
        }
        catch (TaskCanceledException)
        {
            // Ignore
        }
    }

    public IMovement Movement
    {
        get => (IMovement)GetValue(MovementProperty);
        set => SetValue(MovementProperty, value);
    }

    #endregion

    #region RotationDirection DependencyProperty

    public static readonly DependencyProperty RotationDirectionProperty = DependencyProperty.Register(
        nameof(RotationDirection),
        typeof(RotationDirection),
        typeof(Dial),
        new FrameworkPropertyMetadata(RotationDirection.Clockwise, HandleRotationDirectionChanged));

    private static void HandleRotationDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Dial dial)
            dial.InvalidateVisual();
    }

    [Category("Behavior")]
    [DefaultValue(RotationDirection.Clockwise)]
    [Description("Specifies the direction of rotation for the hands (clockwise or counterclockwise).")]
    public RotationDirection RotationDirection
    {
        get => (RotationDirection)GetValue(RotationDirectionProperty);
        set => SetValue(RotationDirectionProperty, value);
    }

    #endregion

    protected override void OnRender(DrawingContext drawingContext)
    {
        PerformanceMeter performanceInfo = PerformanceMeter;
        performanceInfo?.StartMeasurement();

        try
        {
            base.OnRender(drawingContext);

            if (Shapes == null || Shapes.Count == 0)
                return;

            DrawingPlan.Create(drawingContext)
                .WithTransform(() =>
                {
                    double offsetX = ActualWidth / 2;
                    double offsetY = ActualHeight / 2;

                    return new TranslateTransform(offsetX, offsetY);
                })
                .Draw(dc => RenderShapes(dc));
        }
        finally
        {
            performanceInfo?.EndMeasurement();
        }
    }

    private void RenderShapes(DrawingContext drawingContext)
    {
        ClockDrawingContext clockDrawingContext = new()
        {
            DrawingContext = drawingContext,
            ClockDiameter = Math.Min(ActualWidth, ActualHeight),
            Time = Movement?.LastTick ?? TimeSpan.Zero,
            ClockDirection = RotationDirection
        };

        foreach (Shape shape in Shapes)
            shape?.Render(clockDrawingContext);
    }
}
