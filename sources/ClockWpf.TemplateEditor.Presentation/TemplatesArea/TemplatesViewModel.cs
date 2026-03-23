using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.Utils;
using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;

public class TemplatesViewModel : ViewModelBase
{
    private readonly WorkContextPool clockTemplatePool;
    private readonly ClockMovementPool clockMovementPool;

    public ObservableCollection<WorkContextDescriptor> Templates { get; } = [];

    public WorkContextDescriptor SelectedTemplate
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();

            if (!IsInitializing)
                clockTemplatePool.OpenWorkContext(field.ClockTemplateType);
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
        this.clockTemplatePool = clockTemplatePool ?? throw new ArgumentNullException(nameof(clockTemplatePool));
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
            IEnumerable<WorkContextDescriptor> templateDescriptors = clockTemplatePool
                .Select(x => new WorkContextDescriptor(x));

            Templates.Add(templateDescriptors);

            if (clockTemplatePool.CurrentWorkContext?.ClockTemplate != null)
            {
                Type currentTymplateType = clockTemplatePool.CurrentWorkContext.ClockTemplateType;

                SelectedTemplate = Templates
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
