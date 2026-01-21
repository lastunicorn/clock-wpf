using DustInTheWind.ClockWpf.TemplateEditor.Templates;
using DustInTheWind.ClockWpf.TemplateEditor.TimeProviders;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.TimeProviders;

namespace DustInTheWind.ClockWpf.TemplateEditor;

internal class MainViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;

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

    public TemplatesViewModel TemplatesViewModel { get; }

    public TimeProvidersViewModel TimeProvidersViewModel { get; }

    public MainViewModel(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

        TemplatesViewModel = new TemplatesViewModel(applicationState);
        TimeProvidersViewModel = new TimeProvidersViewModel(applicationState);

        applicationState.CurrentTemplateChanged += HandleCurrentTemplateChanged;
        applicationState.CurrentTimeProviderChanged += HandleCurrentTimeProviderChanged;

        Initialize();
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            ClockTemplate = applicationState.CurrentTemplate;
            TimeProvider = applicationState.CurrentTimeProvider;
        });
    }

    private void HandleCurrentTimeProviderChanged(object sender, EventArgs e)
    {
        TimeProvider = applicationState.CurrentTimeProvider;
    }

    private void HandleCurrentTemplateChanged(object sender, EventArgs e)
    {
        ClockTemplate = applicationState.CurrentTemplate;
    }
}
