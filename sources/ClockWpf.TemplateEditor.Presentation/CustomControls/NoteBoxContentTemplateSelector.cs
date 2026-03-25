using System.Windows;
using System.Windows.Controls;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.CustomControls;

public class NoteBoxContentTemplateSelector : DataTemplateSelector
{
    public DataTemplate TextTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is string)
            return TextTemplate;

        return null;
    }
}
