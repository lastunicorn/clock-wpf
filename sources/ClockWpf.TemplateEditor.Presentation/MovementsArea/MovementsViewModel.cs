using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.MovementsArea;

public class MovementsViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;
    private readonly ClockMovementPool clockMovementPool;

    public ObservableCollection<MovementDescriptor> MovementDescriptors { get; } = [];

    public MovementDescriptor SelectedMovementDescriptor
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();

            if (!IsInitializing)
                clockMovementPool.SetDefault(field.Type);
        }
    }

    public IMovement SelectedMovement
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

    public ResetMovementCommand ResetMovementCommand { get; }

    public MovementsViewModel(ApplicationState applicationState, ClockMovementPool clockMovementPool)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        this.clockMovementPool = clockMovementPool ?? throw new ArgumentNullException(nameof(clockMovementPool));

        ResetMovementCommand = new ResetMovementCommand(clockMovementPool);

        Initialize();

        clockMovementPool.CurrentMovementChanged += HandleCurrentMovementChanged;
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            foreach (MovementDescriptor movementDescriptor in clockMovementPool.EnumerateKnownMovements())
                MovementDescriptors.Add(movementDescriptor);

            if (clockMovementPool.CurrentMovement != null)
                SelectedMovementDescriptor = clockMovementPool.CurrentMovement;

            SelectedMovement = clockMovementPool.CurrentMovement?.Instance;
        });
    }

    private void HandleCurrentMovementChanged(object sender, EventArgs e)
    {
        SelectedMovement = clockMovementPool.CurrentMovement?.Instance;
    }
}
