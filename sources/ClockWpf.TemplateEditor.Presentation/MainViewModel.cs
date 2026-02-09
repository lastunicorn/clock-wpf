using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.Performance;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.Miscellaneous;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.Movements;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.Shapes;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.Templates;
using DustInTheWind.ClockWpf.TemplateEditor.State;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation;

public class MainViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;
    private readonly ClockTemplatePool clockTemplatePool;
    private readonly ClockMovementPool clockMovementPool;

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

    public MainViewModel(
        ApplicationState applicationState,
        ClockTemplatePool clockTemplatePool,
        ClockMovementPool clockMovementPool,
        MiscellaneousViewModel miscellaneousViewModel,
        TemplatesViewModel templatesViewModel,
        ShapesViewModel shapesViewModel,
        MovementsViewModel movementsViewModel)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        this.clockTemplatePool = clockTemplatePool ?? throw new ArgumentNullException(nameof(clockTemplatePool));
        this.clockMovementPool = clockMovementPool ?? throw new ArgumentNullException(nameof(clockMovementPool));

        MiscellaneousViewModel = miscellaneousViewModel ?? throw new ArgumentNullException(nameof(miscellaneousViewModel));
        TemplatesViewModel = templatesViewModel ?? throw new ArgumentNullException(nameof(templatesViewModel));
        ShapesViewModel = shapesViewModel ?? throw new ArgumentNullException(nameof(shapesViewModel));
        MovementsViewModel = movementsViewModel ?? throw new ArgumentNullException(nameof(movementsViewModel));

        clockTemplatePool.CurrentTemplateChanged += HandleCurrentTemplateChanged;
        clockMovementPool.CurrentMovementChanged += HandleCurrentMovementChanged;
        applicationState.ClockDirectionChanged += HandleClockDirectionChanged;

        Initialize();
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            PerformanceMeter = new PerformanceMeter();

            ClockTemplate = clockTemplatePool.CurrentTemplate;
            Movement = clockMovementPool.CurrentMovement?.Instance;
            ClockDirection = applicationState.ClockDirection;
        });
    }

    private void HandleCurrentMovementChanged(object sender, EventArgs e)
    {
        Movement = clockMovementPool.CurrentMovement?.Instance;
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
