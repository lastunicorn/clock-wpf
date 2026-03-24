using System.Collections.Specialized;
using System.Windows;
using DustInTheWind.ClockWpf.TemplateEditor.State;
using Microsoft.Xaml.Behaviors;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Behaviors;

/// <summary>
/// Synchronizes the AnalogClock.Shapes collection with the ApplicationState.ClockShapes property
/// and the current WorkContext.Shapes list.
/// </summary>
public class ClockShapesSyncBehavior : Behavior<AnalogClock>
{
    public static readonly DependencyProperty WorkContextProperty = DependencyProperty.Register(
        nameof(WorkContext),
        typeof(WorkContext),
        typeof(ClockShapesSyncBehavior),
        new PropertyMetadata(null, HandleWorkContextChanged));

    private static void HandleWorkContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ClockShapesSyncBehavior behavior)
            return;

        if (behavior.AssociatedObject == null)
            return;

        if (e.NewValue is WorkContext newWorkContext)
            behavior.AssociatedObject.ApplyTemplate(newWorkContext.ClockTemplate);
        else
            behavior.AssociatedObject.ApplyTemplate(null);
    }

    public WorkContext WorkContext
    {
        get => (WorkContext)GetValue(WorkContextProperty);
        set => SetValue(WorkContextProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Loaded += HandleAnalogClockLoaded;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.Loaded -= HandleAnalogClockLoaded;

        if (AssociatedObject.Shapes != null)
        {
            AssociatedObject.Shapes.CollectionChanged -= HandleShapesCollectionChanged;
            AssociatedObject.Shapes.Clear();
        }

        base.OnDetaching();
    }

    private void HandleAnalogClockLoaded(object sender, RoutedEventArgs e)
    {
        if (AssociatedObject.Shapes != null)
        {
            AssociatedObject.Shapes.CollectionChanged += HandleShapesCollectionChanged;
            SyncShapesToWorkContext();
        }
    }

    private void HandleShapesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        SyncShapesToWorkContext();
    }

    private void SyncShapesToWorkContext()
    {
        if (WorkContext == null)
            return;

        WorkContext.Shapes.Clear();

        if (AssociatedObject.Shapes != null)
        {
            foreach (Shapes.Shape shape in AssociatedObject.Shapes)
                WorkContext.Shapes.Add(shape);
        }
    }
}