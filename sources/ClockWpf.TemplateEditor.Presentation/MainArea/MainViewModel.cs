using DustInTheWind.ClockWpf.TemplateEditor.Presentation.CabinetArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.MiscellaneousArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.MovementsArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.ShapesArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;
using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.MainArea;

public class MainViewModel : ViewModelBase
{
    private readonly WorkContextPool workContextPool;
    private readonly ClockMovementPool clockMovementPool;

    public ClockViewModel ClockViewModel { get; }

    public MiscellaneousViewModel MiscellaneousViewModel { get; }

    public TemplatesViewModel TemplatesViewModel { get; }

    public ShapesViewModel ShapesViewModel { get; }

    public MovementsViewModel MovementsViewModel { get; }

    public CabinetViewModel CabinetViewModel { get; }

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

    public MainViewModel(
        ClockViewModel clockViewModel,
        MiscellaneousViewModel miscellaneousViewModel,
        TemplatesViewModel templatesViewModel,
        ShapesViewModel shapesViewModel,
        MovementsViewModel movementsViewModel,
        CabinetViewModel cabinetViewModel,
        WorkContextPool workContextPool,
        ClockMovementPool clockMovementPool)
    {
        ClockViewModel = clockViewModel ?? throw new ArgumentNullException(nameof(clockViewModel));
        MiscellaneousViewModel = miscellaneousViewModel ?? throw new ArgumentNullException(nameof(miscellaneousViewModel));
        TemplatesViewModel = templatesViewModel ?? throw new ArgumentNullException(nameof(templatesViewModel));
        ShapesViewModel = shapesViewModel ?? throw new ArgumentNullException(nameof(shapesViewModel));
        MovementsViewModel = movementsViewModel ?? throw new ArgumentNullException(nameof(movementsViewModel));
        CabinetViewModel = cabinetViewModel ?? throw new ArgumentNullException(nameof(cabinetViewModel));
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
