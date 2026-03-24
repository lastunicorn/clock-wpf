using System.Collections.Specialized;
using System.Windows;
using DustInTheWind.ClockWpf.TemplateEditor.State;
using Microsoft.Xaml.Behaviors;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Behaviors;

/// <summary>
/// Synchronizes the AnalogClock.Shapes collection with the ApplicationState.ClockShapes property
/// and the current WorkContext.Shapes list.
/// </summary>
public class ApplyWorkContextBehavior : Behavior<AnalogClock>
{
    #region WorkContext DependencyProperty

    public static readonly DependencyProperty WorkContextProperty = DependencyProperty.Register(
        nameof(WorkContext),
        typeof(WorkContext),
        typeof(ApplyWorkContextBehavior),
        new PropertyMetadata(null, HandleWorkContextChanged));

    private static void HandleWorkContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ApplyWorkContextBehavior behavior)
            return;

        if (behavior.AssociatedObject == null)
            return;

        if (e.NewValue is WorkContext newWorkContext)
            behavior.InitializeFromWorkContext(newWorkContext);
        else
            behavior.AssociatedObject.ApplyTemplate(null);
    }

    private void InitializeFromWorkContext(WorkContext workContext)
    {
        if (workContext == null)
            return;

        isInitializing = true;

        try
        {
            if (workContext.State == WorkContextState.New)
            {
                AssociatedObject.ApplyTemplate(workContext.ClockTemplate);
                SyncShapesToWorkContext();
            }
            else if (workContext.State == WorkContextState.Unmodified || workContext.State == WorkContextState.Modified)
            {
                AssociatedObject.Shapes.Clear();

                foreach (Shapes.Shape shape in workContext.Shapes)
                    AssociatedObject.Shapes.Add(shape);
            }
            else
            {
                AssociatedObject.ApplyTemplate(null);
            }
        }
        finally
        {
            isInitializing = false;
        }
    }

    public WorkContext WorkContext
    {
        get => (WorkContext)GetValue(WorkContextProperty);
        set => SetValue(WorkContextProperty, value);
    }

    #endregion

    private bool isInitializing;

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
        if (isInitializing)
            return;

        SyncShapesToWorkContext();
    }

    private void SyncShapesToWorkContext(WorkContext workContext = null)
    {
        workContext ??= WorkContext;

        if (workContext == null)
            return;

        workContext.SetShapes(AssociatedObject.Shapes);
    }
}
