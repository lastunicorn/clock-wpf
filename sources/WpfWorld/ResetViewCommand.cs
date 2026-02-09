using System.Windows.Input;

namespace DustInTheWind.WpfWorld;

internal class ResetViewCommand : ICommand
{
    private readonly ZoomPanControl zoomPanControl;

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public ResetViewCommand(ZoomPanControl zoomPanControl)
    {
        this.zoomPanControl = zoomPanControl ?? throw new ArgumentNullException(nameof(zoomPanControl));
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        zoomPanControl.Reset();
    }
}
