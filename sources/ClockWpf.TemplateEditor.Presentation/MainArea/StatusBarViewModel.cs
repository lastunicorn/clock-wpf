using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.MainArea;

public class StatusBarViewModel : ViewModelBase
{
    private readonly WorkContextPool workContextPool;
    private readonly ClockMovementPool clockMovementPool;

    public string TemplateName
    {
        get => field;
        set
        {
            if (value == field)
                return;

            field = value;
            OnPropertyChanged();
        }
    }

    public string MovementName
    {
        get => field;
        set
        {
            if (value == field)
                return;

            field = value;
            OnPropertyChanged();
        }
    }

    public StatusBarViewModel(WorkContextPool workContextPool, ClockMovementPool clockMovementPool)
    {
        this.workContextPool = workContextPool ?? throw new ArgumentNullException(nameof(workContextPool));
        this.clockMovementPool = clockMovementPool ?? throw new ArgumentNullException(nameof(clockMovementPool));

        TemplateName = workContextPool.CurrentWorkContext?.TemplateName;
        MovementName = clockMovementPool.CurrentMovement?.Name;

        workContextPool.CurrentWorkContextChanged += HandleCurrentWorkContextChanged;
        clockMovementPool.CurrentMovementChanged += HandleCurrentMovementChanged;
    }

    private void HandleCurrentWorkContextChanged(object sender, CurrentWorkContextChangedEventArgs e)
    {
        TemplateName = workContextPool.CurrentWorkContext?.TemplateName;
    }

    private void HandleCurrentMovementChanged(object sender, CurrentMovementChangedEventArgs e)
    {
        MovementName = clockMovementPool.CurrentMovement?.Name;
    }
}
