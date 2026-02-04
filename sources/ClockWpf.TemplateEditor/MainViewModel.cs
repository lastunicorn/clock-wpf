using DustInTheWind.ClockWpf.Performance;
using DustInTheWind.ClockWpf.TemplateEditor.Templates;
using DustInTheWind.ClockWpf.TemplateEditor.Movements;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.TemplateEditor;

internal class MainViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;

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
        set
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
        }
    }

    public double ZoomLevel
    {
        get => field;
        set
        {
            if (Math.Abs(field - value) < 0.001)
                return;

            field = value;
            OnPropertyChanged();
        }
    }

    public double TranslateX
    {
        get => field;
        set
        {
            if (Math.Abs(field - value) < 0.001)
                return;

            field = value;
            OnPropertyChanged();
        }
    }

    public double TranslateY
    {
        get => field;
        set
        {
            if (Math.Abs(field - value) < 0.001)
                return;

            field = value;
            OnPropertyChanged();
        }
    }

    public TemplatesViewModel TemplatesViewModel { get; }

    public MovementsViewModel MovementsViewModel { get; }

    public MainViewModel(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

        TemplatesViewModel = new TemplatesViewModel(applicationState);
        MovementsViewModel = new MovementsViewModel(applicationState);

        PerformanceMeter = new PerformanceMeter();

        applicationState.CurrentTemplateChanged += HandleCurrentTemplateChanged;
        applicationState.CurrentMovementChanged += HandleCurrentMovementChanged;

        Initialize();
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            ClockTemplate = applicationState.CurrentTemplate;
            Movement = applicationState.CurrentMovement;
            ClockDirection = RotationDirection.Clockwise;
            ZoomLevel = 1.0;
            TranslateX = 0.0;
            TranslateY = 0.0;
        });
    }

    private void HandleCurrentMovementChanged(object sender, EventArgs e)
    {
        Movement = applicationState.CurrentMovement;
    }

    private void HandleCurrentTemplateChanged(object sender, EventArgs e)
    {
        ClockTemplate = applicationState.CurrentTemplate;
    }
}
