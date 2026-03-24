using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.Performance;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.TemplateEditor.State;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.MainArea;

public class ClockViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;
    private readonly WorkContextPool clockTemplatePool;
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

    public ApplicationState ApplicationState => applicationState;

    public ClockViewModel(
        ApplicationState applicationState,
        WorkContextPool clockTemplatePool,
        ClockMovementPool clockMovementPool)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        this.clockTemplatePool = clockTemplatePool ?? throw new ArgumentNullException(nameof(clockTemplatePool));
        this.clockMovementPool = clockMovementPool ?? throw new ArgumentNullException(nameof(clockMovementPool));

        clockTemplatePool.CurrentWorkContextChanged += HandleCurrentTemplateEditContextChanged;
        clockMovementPool.CurrentMovementChanged += HandleCurrentMovementChanged;
        applicationState.ClockDirectionChanged += HandleClockDirectionChanged;

        Initialize();
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            PerformanceMeter = new PerformanceMeter();

            ClockTemplate = clockTemplatePool.CurrentWorkContext?.ClockTemplate;
            Movement = clockMovementPool.CurrentMovement?.Instance;
            ClockDirection = applicationState.ClockDirection;
        });
    }

    private void HandleCurrentMovementChanged(object sender, EventArgs e)
    {
        Movement = clockMovementPool.CurrentMovement?.Instance;
    }

    private void HandleCurrentTemplateEditContextChanged(object sender, EventArgs e)
    {
        ClockTemplate = clockTemplatePool.CurrentWorkContext?.ClockTemplate;
    }

    private void HandleClockDirectionChanged(object sender, EventArgs e)
    {
        Initialize(() =>
        {
            ClockDirection = applicationState.ClockDirection;
        });
    }

    public void SetClockShapes(ObservableCollection<Shape> shapes)
    {
        applicationState.ClockShapes = shapes;
    }
}
