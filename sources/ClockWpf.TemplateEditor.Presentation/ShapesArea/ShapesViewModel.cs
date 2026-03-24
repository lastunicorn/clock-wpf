using DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;
using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.ShapesArea;

public class ShapesViewModel : ViewModelBase
{
    private readonly WorkContextPool workContextPool;

    public InUseShapesViewModel InUseShapesViewModel { get; }

    public AvailableShapesViewModel AvailableShapesViewModel { get; }

    public ResetTemplateCommand ResetTemplateCommand { get; }

    public WorkContextState WorkContextState
    {
        get => field;
        private set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();
        }
    }

    public string TemplateName
    {
        get => field;
        private set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();
        }
    }

    public ShapesViewModel(InUseShapesViewModel inUseShapesViewModel, AvailableShapesViewModel availableShapesViewModel, WorkContextPool workContextPool)
    {
        InUseShapesViewModel = inUseShapesViewModel ?? throw new ArgumentNullException(nameof(inUseShapesViewModel));
        AvailableShapesViewModel = availableShapesViewModel ?? throw new ArgumentNullException(nameof(availableShapesViewModel));
        this.workContextPool = workContextPool ?? throw new ArgumentNullException(nameof(workContextPool));

        ResetTemplateCommand = new ResetTemplateCommand(workContextPool);

        this.workContextPool.CurrentWorkContextChanged += HandleCurrentWorkContextChanged;

        Initialize();
    }

    private void Initialize()
    {
        if (workContextPool.CurrentWorkContext != null)
            workContextPool.CurrentWorkContext.StateChanged += HandleWorkContextStateChanged;

        UpdateFromCurrentWorkContext();
    }

    private void HandleCurrentWorkContextChanged(object sender, CurrentWorkContextChangedEventArgs e)
    {
        if (e.OldContext != null)
            e.OldContext.StateChanged -= HandleWorkContextStateChanged;

        if (e.NewContext != null)
            e.NewContext.StateChanged += HandleWorkContextStateChanged;

        UpdateFromCurrentWorkContext();
    }

    private void HandleWorkContextStateChanged(object sender, EventArgs e)
    {
        UpdateFromCurrentWorkContext();
    }

    private void UpdateFromCurrentWorkContext()
    {
        WorkContextState = workContextPool.CurrentWorkContext?.State ?? WorkContextState.Closed;
        TemplateName = workContextPool.CurrentWorkContext?.TemplateName ?? string.Empty;
    }
}
