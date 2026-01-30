using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.TemplateEditor.Movements;

public class MovementsViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;

    public ObservableCollection<MovementDescriptor> MovementTypes { get; } = [];

    public MovementDescriptor SelectedMovementType
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();

            if (!IsInitializing)
            {
                applicationState.CurrentMovement?.Stop();

                applicationState.CurrentMovement = field == null
                    ? null
                    : (IMovement)Activator.CreateInstance(field.Type);

                applicationState.CurrentMovement?.Start();
            }
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

    public MovementsViewModel(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

        Initialize();

        applicationState.CurrentMovementChanged += HandleCurrentMovementChanged;
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            if (applicationState.AvailableMovementTypes != null)
            {
                foreach (Type type in applicationState.AvailableMovementTypes)
                {
                    MovementTypes.Add(new MovementDescriptor
                    {
                        Name = type.Name.Replace("Movement", ""),
                        Type = type
                    });
                }
            }

            if (applicationState.CurrentMovement != null)
            {
                Type currentMovementType = applicationState.CurrentMovement.GetType();

                SelectedMovementType = MovementTypes
                    .FirstOrDefault(x => x.Type == currentMovementType);
            }

            SelectedMovement = applicationState.CurrentMovement;
        });
    }

    private void HandleCurrentMovementChanged(object sender, EventArgs e)
    {
        SelectedMovement = applicationState.CurrentMovement;
    }
}
