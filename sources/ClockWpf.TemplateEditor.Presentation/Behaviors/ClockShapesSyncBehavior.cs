using System.Windows;
using DustInTheWind.ClockWpf.TemplateEditor.State;
using Microsoft.Xaml.Behaviors;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Behaviors;

/// <summary>
/// Synchronizes the AnalogClock.Shapes collection with the ApplicationState.ClockShapes property.
/// </summary>
public class ClockShapesSyncBehavior : Behavior<AnalogClock>
{
    public static readonly DependencyProperty ApplicationStateProperty = DependencyProperty.Register(
        nameof(ApplicationState),
        typeof(ApplicationState),
        typeof(ClockShapesSyncBehavior),
        new PropertyMetadata(null));

    public ApplicationState ApplicationState
    {
        get => (ApplicationState)GetValue(ApplicationStateProperty);
        set => SetValue(ApplicationStateProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Loaded += HandleAnalogClockLoaded;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.Loaded -= HandleAnalogClockLoaded;
        base.OnDetaching();
    }

    private void HandleAnalogClockLoaded(object sender, RoutedEventArgs e)
    {
        if (ApplicationState != null && AssociatedObject.Shapes != null)
            ApplicationState.ClockShapes = AssociatedObject.Shapes;
    }
}