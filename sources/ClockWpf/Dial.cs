using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DustInTheWind.ClockWpf.Performance;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf;

public class Dial : Canvas
{
    public PerformanceMeter PerformanceInfo { get; set; }

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

    #region KeepProportions DependencyProperty

    public static readonly DependencyProperty KeepProportionsProperty = DependencyProperty.Register(
        nameof(KeepProportions),
        typeof(bool),
        typeof(Dial),
        new PropertyMetadata(false, OnKeepProportionsChanged));

    private static void OnKeepProportionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Dial canvas)
            canvas.InvalidateVisual();
    }

    public bool KeepProportions
    {
        get => (bool)GetValue(KeepProportionsProperty);
        set => SetValue(KeepProportionsProperty, value);
    }

    #endregion

    #region Time DependencyProperty

    public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
        nameof(Time),
        typeof(TimeSpan),
        typeof(Dial),
        new FrameworkPropertyMetadata(TimeSpan.Zero, OnTimeChanged));

    private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Dial canvas)
            canvas.InvalidateVisual();
    }

    public TimeSpan Time
    {
        get => (TimeSpan)GetValue(TimeProperty);
        set => SetValue(TimeProperty, value);
    }

    #endregion

    protected override void OnRender(DrawingContext drawingContext)
    {
        PerformanceMeter performanceInfo = PerformanceInfo;
        performanceInfo?.Start();

        try
        {
            base.OnRender(drawingContext);

            if (Shapes == null || Shapes.Count == 0)
                return;

            double diameter = Math.Min(ActualWidth, ActualHeight);

            DrawingPlan.Create(drawingContext)
                .WithTransform(() =>
                {
                    double offsetX = ActualWidth / 2;
                    double offsetY = ActualHeight / 2;

                    return new TranslateTransform(offsetX, offsetY);
                })
                .WithTransform(() =>
                {
                    return KeepProportions
                        ? null
                        : CreateScaleTransform(diameter);
                })
                .Draw(dc => RenderShapes(dc, diameter));
        }
        finally
        {
            performanceInfo?.Stop();
        }
    }

    private ScaleTransform CreateScaleTransform(double diameter)
    {
        double scaleX = ActualWidth / diameter;
        double scaleY = ActualHeight / diameter;

        double centerX = diameter / 2;
        double centerY = diameter / 2;

        return new ScaleTransform(scaleX, scaleY, centerX, centerY);
    }

    private void RenderShapes(DrawingContext drawingContext, double diameter)
    {
        ClockDrawingContext clockDrawingContext = new()
        {
            DrawingContext = drawingContext,
            ClockDiameter = diameter,
            Time = Time
        };

        foreach (Shape shape in Shapes)
            shape?.Render(clockDrawingContext);
    }
}
