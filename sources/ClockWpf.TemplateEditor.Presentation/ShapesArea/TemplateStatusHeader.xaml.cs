using System.Windows;
using System.Windows.Controls;
using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.ShapesArea;

public class TemplateStatusHeader : Control
{
    #region TemplateName DependencyProperty

    public static readonly DependencyProperty TemplateNameProperty = DependencyProperty.Register(
        nameof(TemplateName),
        typeof(string),
        typeof(TemplateStatusHeader),
        new PropertyMetadata(string.Empty));

    public string TemplateName
    {
        get => (string)GetValue(TemplateNameProperty);
        set => SetValue(TemplateNameProperty, value);
    }

    #endregion

    #region Status DependencyProperty

    public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
        nameof(Status),
        typeof(WorkContextState),
        typeof(TemplateStatusHeader),
        new PropertyMetadata(WorkContextState.Closed));

    public WorkContextState Status
    {
        get => (WorkContextState)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    #endregion

    static TemplateStatusHeader()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TemplateStatusHeader), new FrameworkPropertyMetadata(typeof(TemplateStatusHeader)));
    }
}
