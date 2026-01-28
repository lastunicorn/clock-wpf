using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.TimeProviders;

namespace DustInTheWind.ClockWpf;

public class AnalogClock : Control
{
    private ShapeCanvas shapeCanvas;

    #region PerformanceInfo DependencyProperty

    public static readonly DependencyProperty PerformanceInfoProperty = DependencyProperty.Register(
        nameof(PerformanceInfo),
        typeof(PerformanceInfo),
        typeof(AnalogClock),
        new PropertyMetadata(null, HandlePerformanceInfoChanged));

    private static void HandlePerformanceInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AnalogClock analogClock && analogClock.shapeCanvas != null)
            analogClock.shapeCanvas.PerformanceInfo = (PerformanceInfo)e.NewValue;
    }

    public PerformanceInfo PerformanceInfo
    {
        get => (PerformanceInfo)GetValue(PerformanceInfoProperty);
        set => SetValue(PerformanceInfoProperty, value);
    }

    #endregion

    #region Shapes DependencyProperty

    public static readonly DependencyProperty ShapesProperty = DependencyProperty.Register(
        nameof(Shapes),
        typeof(ObservableCollection<Shape>),
        typeof(AnalogClock),
        new PropertyMetadata(null, OnShapesChanged));

    private static void OnShapesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AnalogClock analogClock)
            return;

        if (e.OldValue is ObservableCollection<Shape> oldShapes)
            oldShapes.CollectionChanged -= analogClock.HandleShapesCollectionChanged;

        if (e.NewValue is ObservableCollection<Shape> newShapes)
        {
            newShapes.CollectionChanged += analogClock.HandleShapesCollectionChanged;
            analogClock.UpdateIsEmpty();
        }
    }

    private void HandleShapesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        UpdateIsEmpty();
    }

    public ObservableCollection<Shape> Shapes
    {
        get => (ObservableCollection<Shape>)GetValue(ShapesProperty);
        set => SetValue(ShapesProperty, value);
    }

    #endregion

    #region IsEmpty DependencyProperty

    public static readonly DependencyProperty IsEmptyProperty = DependencyProperty.Register(
        nameof(IsEmpty),
        typeof(bool),
        typeof(AnalogClock),
        new PropertyMetadata(true));

    public bool IsEmpty
    {
        get => (bool)GetValue(IsEmptyProperty);
        private set => SetValue(IsEmptyProperty, value);
    }

    #endregion

    #region KeepProportions DependencyProperty

    public static readonly DependencyProperty KeepProportionsProperty = DependencyProperty.Register(
        nameof(KeepProportions),
        typeof(bool),
        typeof(AnalogClock),
        new PropertyMetadata(true, OnKeepProportionsChanged));

    private static void OnKeepProportionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AnalogClock analogClock)
            return;

        analogClock.shapeCanvas?.InvalidateVisual();
    }

    public bool KeepProportions
    {
        get => (bool)GetValue(KeepProportionsProperty);
        set => SetValue(KeepProportionsProperty, value);
    }

    #endregion

    #region TimeProvider DependencyProperty

    public static readonly DependencyProperty TimeProviderProperty = DependencyProperty.Register(
        nameof(TimeProvider),
        typeof(ITimeProvider),
        typeof(AnalogClock),
        new PropertyMetadata(null, OnTimeProviderChanged));

    private static void OnTimeProviderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AnalogClock analogClock)
            return;

        if (e.OldValue is ITimeProvider oldTimeProvider)
            oldTimeProvider.Tick -= analogClock.HandleTimeChanged;

        if (e.NewValue is ITimeProvider newTimeProvider)
        {
            newTimeProvider.Tick += analogClock.HandleTimeChanged;
            analogClock.UpdateDisplayedTime(newTimeProvider.LastValue);
        }
    }

    private void HandleTimeChanged(object sender, TickEventArgs e)
    {
        UpdateDisplayedTime(e.Time);
    }

    private void UpdateDisplayedTime(TimeSpan time)
    {
        if (Dispatcher.HasShutdownStarted || Dispatcher.HasShutdownFinished)
            return;

        try
        {
            if (shapeCanvas != null)
            {
                Dispatcher.Invoke(() =>
                {
                    shapeCanvas.Time = time;
                });
            }
        }
        catch (TaskCanceledException)
        {
            // Ignore
        }
    }

    public ITimeProvider TimeProvider
    {
        get => (ITimeProvider)GetValue(TimeProviderProperty);
        set => SetValue(TimeProviderProperty, value);
    }

    #endregion

    #region ClockTemplate DependencyProperty

    public static readonly DependencyProperty ClockTemplateProperty = DependencyProperty.Register(
        nameof(ClockTemplate),
        typeof(ClockTemplate),
        typeof(AnalogClock),
        new PropertyMetadata(null, OnClockTemplateChanged));

    private static void OnClockTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AnalogClock analogClock)
        {
            analogClock.Shapes.Clear();

            if (e.NewValue is ClockTemplate clockTemplate)
            {
                if (clockTemplate == null)
                    return;

                foreach (Shape shape in clockTemplate)
                    analogClock.Shapes.Add(shape);
            }
        }
    }

    public ClockTemplate ClockTemplate
    {
        get => (ClockTemplate)GetValue(ClockTemplateProperty);
        set => SetValue(ClockTemplateProperty, value);
    }

    #endregion

    static AnalogClock()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogClock), new FrameworkPropertyMetadata(typeof(AnalogClock)));
    }

    public AnalogClock()
    {
        Shapes = [];
    }

    private void UpdateIsEmpty()
    {
        IsEmpty = Shapes == null || Shapes.Count == 0;
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        shapeCanvas = GetTemplateChild("PART_ShapeCanvas") as ShapeCanvas;

        ITimeProvider currentTimeProvider = TimeProvider;
        if (currentTimeProvider != null)
            UpdateDisplayedTime(currentTimeProvider.LastValue);

        if (shapeCanvas != null)
            shapeCanvas.PerformanceInfo = PerformanceInfo;
    }
}
