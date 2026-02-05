using System.Windows.Input;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Templates;

public class ResetTemplateCommand : ICommand
{
    private readonly ClockTemplatePool clockTemplatePool;

    public event EventHandler CanExecuteChanged;

    public ResetTemplateCommand(ClockTemplatePool clockTemplatePool)
    {
        this.clockTemplatePool = clockTemplatePool ?? throw new ArgumentNullException(nameof(clockTemplatePool));

        clockTemplatePool.CurrentTemplateChanged += HandleCurrentTemplateChanged;
    }

    private void HandleCurrentTemplateChanged(object sender, EventArgs e)
    {
        RaiseCanExecuteChanged();
    }

    public bool CanExecute(object parameter)
    {
        return clockTemplatePool.CurrentTemplate != null;
    }

    public void Execute(object parameter)
    {
        clockTemplatePool.RecreateCurrentTemplate();
    }

    private void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
