using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.Utils;
using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;

public class TemplatesViewModel : ViewModelBase
{
    private readonly WorkContextPool workContextsPool;
    private readonly ClockMovementPool clockMovementPool;

    public ObservableCollection<WorkContextDescriptor> WorkContexts { get; } = [];

    public WorkContextDescriptor SelectedWorkContext
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();

            if (!IsInitializing)
                workContextsPool.OpenWorkContext(field.ClockTemplateType);
        }
    }

    public IMovement Movement
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

    public SaveTemplateCommand SaveTemplateCommand { get; }

    public ResetTemplateCommand ResetTemplateCommand { get; }

    public TemplatesViewModel(WorkContextPool clockTemplatePool, ClockMovementPool clockMovementPool)
    {
        this.workContextsPool = clockTemplatePool ?? throw new ArgumentNullException(nameof(clockTemplatePool));
        this.clockMovementPool = clockMovementPool ?? throw new ArgumentNullException(nameof(clockMovementPool));

        SaveTemplateCommand = new SaveTemplateCommand(clockTemplatePool);
        ResetTemplateCommand = new ResetTemplateCommand(clockTemplatePool);

        Initialize();

        clockMovementPool.CurrentMovementChanged += HandleCurrentMovementChanged;
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            IEnumerable<WorkContextDescriptor> templateDescriptors = workContextsPool
                .Select(x => new WorkContextDescriptor(x));

            WorkContexts.Add(templateDescriptors);

            if (workContextsPool.CurrentWorkContext?.ClockTemplate != null)
            {
                Type currentTymplateType = workContextsPool.CurrentWorkContext.ClockTemplateType;

                SelectedWorkContext = WorkContexts
                    .FirstOrDefault(x => x.ClockTemplateType == currentTymplateType);
            }

            Movement = clockMovementPool.CurrentMovement?.Instance;
        });
    }

    private void HandleCurrentMovementChanged(object sender, EventArgs e)
    {
        Movement = clockMovementPool.CurrentMovement?.Instance;
    }
}
