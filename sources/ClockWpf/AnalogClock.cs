using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.Performance;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf;

public class AnalogClock : Control
{
    private Dial dial;

    #region PerformanceMeter DependencyProperty

    public static readonly DependencyProperty PerformanceMeterProperty = DependencyProperty.Register(
        nameof(PerformanceMeter),
        typeof(PerformanceMeter),
        typeof(AnalogClock),
        new PropertyMetadata(null, HandlePerformanceMeterChanged));

    private static void HandlePerformanceMeterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AnalogClock analogClock && analogClock.dial != null)
            analogClock.dial.PerformanceInfo = (PerformanceMeter)e.NewValue;
    }

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
        typeof(AnalogClock),
        new PropertyMetadata(null, HandleShapesChanged));

    private static void HandleShapesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
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
        new PropertyMetadata(true, HandleKeepProportionsChanged));

    private static void HandleKeepProportionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AnalogClock analogClock)
            return;

        analogClock.dial?.InvalidateVisual();
    }

    public bool KeepProportions
    {
        get => (bool)GetValue(KeepProportionsProperty);
        set => SetValue(KeepProportionsProperty, value);
    }

    #endregion

    #region Movement DependencyProperty

    public static readonly DependencyProperty MovementProperty = DependencyProperty.Register(
        nameof(Movement),
        typeof(IMovement),
        typeof(AnalogClock),
        new PropertyMetadata(null, HandleMovementChanged));

    private static void HandleMovementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AnalogClock analogClock)
            return;

        if (e.OldValue is IMovement oldMovement)
            oldMovement.Tick -= analogClock.HandleTick;

        if (e.NewValue is IMovement newMovement)
        {
            newMovement.Tick += analogClock.HandleTick;
            analogClock.UpdateDisplayedTime(newMovement.LastTick);
        }
    }

    private void HandleTick(object sender, TickEventArgs e)
    {
        UpdateDisplayedTime(e.Time);
    }

    private void UpdateDisplayedTime(TimeSpan time)
    {
        if (Dispatcher.HasShutdownStarted || Dispatcher.HasShutdownFinished)
            return;

        try
        {
            Dispatcher.Invoke(() =>
            {
                dial?.Time = time;
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

    #region ClockTemplate DependencyProperty

    public static readonly DependencyProperty ClockTemplateProperty = DependencyProperty.Register(
        nameof(ClockTemplate),
        typeof(ClockTemplate),
        typeof(AnalogClock),
        new PropertyMetadata(null, HandleClockTemplateChanged));

    private static void HandleClockTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
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

    #region RotationDirection DependencyProperty

    public static readonly DependencyProperty RotationDirectionProperty = DependencyProperty.Register(
        nameof(RotationDirection),
        typeof(RotationDirection),
        typeof(AnalogClock),
        new FrameworkPropertyMetadata(RotationDirection.Clockwise, HandleRotationDirectionChanged));

    private static void HandleRotationDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AnalogClock analogClock)
            return;

        analogClock.dial?.InvalidateVisual();
    }

    [Category("Behavior")]
    [DefaultValue(RotationDirection.Clockwise)]
    [Description("Specifies the direction of rotation for the hands (clockwise or counter clockwise).")]
    public RotationDirection RotationDirection
    {
        get => (RotationDirection)GetValue(RotationDirectionProperty);
        set => SetValue(RotationDirectionProperty, value);
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

        dial = GetTemplateChild("PART_Dial") as Dial;

        IMovement movement = Movement;
        if (movement != null)
            UpdateDisplayedTime(movement.LastTick);

        dial?.PerformanceInfo = PerformanceMeter;
    }
}
