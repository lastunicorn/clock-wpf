using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.Movements;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Templates;

public class TemplatesViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;
    private readonly ClockTemplatePool clockTemplatePool;

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
                clockTemplatePool.SetDefault(field.Type);
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

    public ResetTemplateCommand ResetTemplateCommand { get; }

    public TemplatesViewModel(ApplicationState applicationState, ClockTemplatePool clockTemplatePool)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        this.clockTemplatePool = clockTemplatePool ?? throw new ArgumentNullException(nameof(clockTemplatePool));

        SaveTemplateCommand = new SaveTemplateCommand(clockTemplatePool);
        ResetTemplateCommand = new ResetTemplateCommand(clockTemplatePool);

        Initialize();

        clockTemplatePool.CurrentTemplateChanged += HandleCurrentTemplateChanged;
        applicationState.CurrentMovementChanged += HandleCurrentMovementChanged;
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            foreach (Type type in clockTemplatePool.EnumerateKnownTypes())
            {
                TemplateTypes.Add(new TemplateInfo
                {
                    Name = type.Name
                        .Replace("ClockTemplate", "")
                        .Replace("Template", ""),
                    Type = type
                });
            }

            if (clockTemplatePool.CurrentTemplate != null)
            {
                Type currentTymplateType = clockTemplatePool.CurrentTemplate.GetType();

                SelectedTemplateType = TemplateTypes
                    .FirstOrDefault(x => x.Type == currentTymplateType);

                Shapes = new ObservableCollection<Shape>(clockTemplatePool.CurrentTemplate);
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
        Shapes = new ObservableCollection<Shape>(clockTemplatePool.CurrentTemplate);
        SaveTemplateCommand.RaiseCanExecuteChanged();
    }
}
