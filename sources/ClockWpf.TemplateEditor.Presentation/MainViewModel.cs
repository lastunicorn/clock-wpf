using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.Performance;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.MiscellaneousArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.MovementsArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.ShapesArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;
using DustInTheWind.ClockWpf.TemplateEditor.State;
using DustInTheWind.ClockWpf.Templates;
using Microsoft.Xaml.Behaviors.Core;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation;

public class MainViewModel : ViewModelBase
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

    public MiscellaneousViewModel MiscellaneousViewModel { get; }

    public TemplatesViewModel TemplatesViewModel { get; }

    public ShapesViewModel ShapesViewModel { get; }

    public MovementsViewModel MovementsViewModel { get; }

    public CabinetViewModel CabinetViewModel { get; }

    public ApplicationState ApplicationState => applicationState;

    public MainViewModel(
        ApplicationState applicationState,
        WorkContextPool clockTemplatePool,
        ClockMovementPool clockMovementPool,
        MiscellaneousViewModel miscellaneousViewModel,
        TemplatesViewModel templatesViewModel,
        ShapesViewModel shapesViewModel,
        MovementsViewModel movementsViewModel,
        CabinetViewModel cabinetViewModel)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        this.clockTemplatePool = clockTemplatePool ?? throw new ArgumentNullException(nameof(clockTemplatePool));
        this.clockMovementPool = clockMovementPool ?? throw new ArgumentNullException(nameof(clockMovementPool));

        MiscellaneousViewModel = miscellaneousViewModel ?? throw new ArgumentNullException(nameof(miscellaneousViewModel));
        TemplatesViewModel = templatesViewModel ?? throw new ArgumentNullException(nameof(templatesViewModel));
        ShapesViewModel = shapesViewModel ?? throw new ArgumentNullException(nameof(shapesViewModel));
        MovementsViewModel = movementsViewModel ?? throw new ArgumentNullException(nameof(movementsViewModel));
        CabinetViewModel = cabinetViewModel ?? throw new ArgumentNullException(nameof(cabinetViewModel));

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
