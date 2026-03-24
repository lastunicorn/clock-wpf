using System.Windows;
using DustInTheWind.ClockWpf.Templates;
using Microsoft.Xaml.Behaviors;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Behaviors;

public class ApplyClockTemplateBehavior : Behavior<AnalogClock>
{
    public static readonly DependencyProperty ClockTemplateProperty = DependencyProperty.Register(
        nameof(ClockTemplate),
        typeof(ClockTemplate),
        typeof(ApplyClockTemplateBehavior),
        new PropertyMetadata(null, HandleClockTemplateChanged));

    private static void HandleClockTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ApplyClockTemplateBehavior behavior)
            return;

        if (behavior.AssociatedObject == null)
            return;

        if (e.NewValue is ClockTemplate newClockTemplate)
            behavior.AssociatedObject.ApplyTemplate(newClockTemplate);
        else
            behavior.AssociatedObject.ApplyTemplate(null);
    }

    public ClockTemplate ClockTemplate
    {
        get => (ClockTemplate)GetValue(ClockTemplateProperty);
        set => SetValue(ClockTemplateProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();

        if (ClockTemplate != null)
            AssociatedObject.ApplyTemplate(ClockTemplate);
    }
}