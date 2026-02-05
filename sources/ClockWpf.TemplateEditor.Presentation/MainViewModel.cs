using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.Performance;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.Miscellaneous;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.Movements;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.Shapes;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.Templates;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation;

public class MainViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;
    private readonly ClockTemplatePool clockTemplatePool;

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

    public ClockTemplate ClockTemplate
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

    public PerformanceMeter PerformanceMeter
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

    public RotationDirection ClockDirection
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();

            if (!IsInitializing)
                applicationState.ClockDirection = value;
        }
    }

    public MiscellaneousViewModel MiscellaneousViewModel { get; }

    public TemplatesViewModel TemplatesViewModel { get; }

    public ShapesViewModel ShapesViewModel { get; }

    public MovementsViewModel MovementsViewModel { get; }

    public MainViewModel(ApplicationState applicationState, ClockTemplatePool clockTemplatePool)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        this.clockTemplatePool = clockTemplatePool ?? throw new ArgumentNullException(nameof(clockTemplatePool));

        MiscellaneousViewModel = new MiscellaneousViewModel(applicationState);
        TemplatesViewModel = new TemplatesViewModel(applicationState, clockTemplatePool);
        ShapesViewModel = new ShapesViewModel();
        MovementsViewModel = new MovementsViewModel(applicationState);

        PerformanceMeter = new PerformanceMeter();

        clockTemplatePool.CurrentTemplateChanged += HandleCurrentTemplateChanged;
        applicationState.CurrentMovementChanged += HandleCurrentMovementChanged;
        applicationState.ClockDirectionChanged += HandleClockDirectionChanged;

        Initialize();
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            ClockTemplate = clockTemplatePool.CurrentTemplate;
            Movement = applicationState.CurrentMovement;
            ClockDirection = applicationState.ClockDirection;
        });
    }

    private void HandleCurrentMovementChanged(object sender, EventArgs e)
    {
        Movement = applicationState.CurrentMovement;
    }

    private void HandleCurrentTemplateChanged(object sender, EventArgs e)
    {
        ClockTemplate = clockTemplatePool.CurrentTemplate;
    }

    private void HandleClockDirectionChanged(object sender, EventArgs e)
    {
        Initialize(() =>
        {
            ClockDirection = applicationState.ClockDirection;
        });
    }
}
