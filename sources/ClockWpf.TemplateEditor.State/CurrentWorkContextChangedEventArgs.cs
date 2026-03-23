namespace DustInTheWind.ClockWpf.TemplateEditor.State;

public class CurrentWorkContextChangedEventArgs : EventArgs
{
    public WorkContext OldContext { get; }
 
    public WorkContext NewContext { get; }
    
    public CurrentWorkContextChangedEventArgs(WorkContext oldContext, WorkContext newContext)
    {
        OldContext = oldContext;
        NewContext = newContext;
    }
}
