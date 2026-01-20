using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.TimeProviders;

namespace DustInTheWind.ClockWpf.Demo.Templates;

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

            if (!IsInitializing)
                applicationState.CurrentShape = value;
        }
    }

    public ITimeProvider TimeProvider
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

    public TemplatesViewModel(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

        applicationState.CurrentTemplateChanged += HandleCurrentTemplateChanged;
        applicationState.CurrentTimeProviderChanged += HandleCurrentTimeProviderChanged;

        Initialize();
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

            TimeProvider = applicationState.CurrentTimeProvider;
        });
    }

    private void HandleCurrentTimeProviderChanged(object sender, EventArgs e)
    {
        TimeProvider = applicationState.CurrentTimeProvider;
    }

    private void HandleCurrentTemplateChanged(object sender, EventArgs e)
    {
        Shapes = new ObservableCollection<Shape>(applicationState.CurrentTemplate);
    }
}
