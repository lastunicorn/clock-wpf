using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.TemplateEditor.Templates;

public class TemplatesViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;

    public ObservableCollection<TemplateInfo> TemplateTypes { get; } = [];

    public TemplateInfo SelectedTemplateType
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
                applicationState.CurrentTemplate = field == null
                    ? null
                    : (ClockTemplate)Activator.CreateInstance(field.Type);
            }
        }
    }

    public ObservableCollection<Shape> Shapes
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

    public Shape SelectedShape
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

    public TemplatesViewModel(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

        SaveTemplateCommand = new SaveTemplateCommand(applicationState);

        Initialize();

        applicationState.CurrentTemplateChanged += HandleCurrentTemplateChanged;
        applicationState.CurrentMovementChanged += HandleCurrentMovementChanged;
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            if (applicationState.AvailableTemplateTypes != null)
            {
                foreach (Type type in applicationState.AvailableTemplateTypes)
                {
                    TemplateTypes.Add(new TemplateInfo
                    {
                        Name = type.Name
                            .Replace("ClockTemplate", "")
                            .Replace("Template", ""),
                        Type = type
                    });
                }
            }

            if (applicationState.CurrentTemplate != null)
            {
                Type currentTymplateType = applicationState.CurrentTemplate.GetType();

                SelectedTemplateType = TemplateTypes
                    .FirstOrDefault(x => x.Type == currentTymplateType);

                Shapes = new ObservableCollection<Shape>(applicationState.CurrentTemplate);
            }

            Movement = applicationState.CurrentMovement;
        });
    }

    private void HandleCurrentMovementChanged(object sender, EventArgs e)
    {
        Movement = applicationState.CurrentMovement;
    }

    private void HandleCurrentTemplateChanged(object sender, EventArgs e)
    {
        Shapes = new ObservableCollection<Shape>(applicationState.CurrentTemplate);
        SaveTemplateCommand.RaiseCanExecuteChanged();
    }
}
