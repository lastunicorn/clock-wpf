using System.Windows.Input;
using DustInTheWind.ClockWpf.TemplateEditor.State;


namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Movements;

public class ResetMovementCommand : ICommand
{
    private readonly ClockMovementPool clockMovementPool;

    public event EventHandler CanExecuteChanged;

    public ResetMovementCommand(ClockMovementPool clockMovementPool)
    {
        this.clockMovementPool = clockMovementPool ?? throw new ArgumentNullException(nameof(clockMovementPool));

        if (clockMovementPool.CurrentMovement?.Instance != null)
            clockMovementPool.CurrentMovement.Instance.Modified += HandleCurrentMovementModified;

        clockMovementPool.CurrentMovementChanged += HandleCurrentMovementChanged;
    }

    private void HandleCurrentMovementChanged(object sender, CurrentMovementChangedEventArgs e)
    {
        if (e.OldMovement != null)
            e.OldMovement.Modified -= HandleCurrentMovementModified;

        if (e.NewMovement != null)
            e.NewMovement.Modified += HandleCurrentMovementModified;

        OnCanExecuteChanged();
    }

    private void HandleCurrentMovementModified(object sender, EventArgs e)
    {
        OnCanExecuteChanged();
    }

    public bool CanExecute(object parameter)
    {
        return clockMovementPool.CurrentMovement?.IsNew == false;
    }

    public void Execute(object parameter)
    {
        clockMovementPool.RecreateCurrentTemplate();
    }

    private void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
