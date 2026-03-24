using System.Windows.Input;
using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;

public class ResetTemplateCommand : ICommand
{
    private readonly WorkContextPool clockTemplatePool;

    public event EventHandler CanExecuteChanged;

    public ResetTemplateCommand(WorkContextPool clockTemplatePool)
    {
        this.clockTemplatePool = clockTemplatePool ?? throw new ArgumentNullException(nameof(clockTemplatePool));

        if (clockTemplatePool.CurrentWorkContext != null)
            clockTemplatePool.CurrentWorkContext.StateChanged += HandleTemplateStateChanged;

        clockTemplatePool.CurrentWorkContextChanged += HandleCurrentTemplateEditContextChanged;
    }

    private void HandleCurrentTemplateEditContextChanged(object sender, CurrentWorkContextChangedEventArgs e)
    {
        if (e.OldContext != null)
            e.OldContext.StateChanged -= HandleTemplateStateChanged;

        if (e.NewContext != null)
            e.NewContext.StateChanged += HandleTemplateStateChanged;

        OnCanExecuteChanged();
    }

    private void HandleTemplateStateChanged(object sender, EventArgs e)
    {
        OnCanExecuteChanged();
    }

    public bool CanExecute(object parameter)
    {
        return clockTemplatePool.CurrentWorkContext?.CanReset == true;
    }

    public void Execute(object parameter)
    {
        clockTemplatePool.CurrentWorkContext?.Reset();
    }

    private void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
