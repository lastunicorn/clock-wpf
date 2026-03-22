using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.Utils;
using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;

public class TemplatesViewModel : ViewModelBase
{
    private readonly ClockTemplatePool clockTemplatePool;
    private readonly ClockMovementPool clockMovementPool;

    public ObservableCollection<TemplateDescriptor> Templates { get; } = [];

    public TemplateDescriptor SelectedTemplate
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();

            if (!IsInitializing)
                clockTemplatePool.SetDefault(field.Type);
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

    public TemplatesViewModel(ClockTemplatePool clockTemplatePool, ClockMovementPool clockMovementPool)
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
            IEnumerable<TemplateDescriptor> templateDescriptors = clockTemplatePool.EnumerateKnownTypes()
                .Select(x => new TemplateDescriptor(x));

            Templates.Add(templateDescriptors);

            if (clockTemplatePool.CurrentTemplate != null)
            {
                Type currentTymplateType = clockTemplatePool.CurrentTemplate.GetType();

                SelectedTemplate = Templates
                    .FirstOrDefault(x => x.Type == currentTymplateType);
            }

            Movement = clockMovementPool.CurrentMovement?.Instance;
        });
    }

    private void HandleCurrentMovementChanged(object sender, EventArgs e)
    {
        Movement = clockMovementPool.CurrentMovement?.Instance;
    }
}
