using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.State;

public class CurrentTemplateChangedEventArgs : EventArgs
{
    public ClockTemplate OldTemplate { get; }
 
    public ClockTemplate NewTemplate { get; }
    
    public CurrentTemplateChangedEventArgs(ClockTemplate oldTemplate, ClockTemplate newTemplate)
    {
        OldTemplate = oldTemplate;
        NewTemplate = newTemplate;
    }
}
