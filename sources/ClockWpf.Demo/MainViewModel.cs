using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Demo.Templates;
using DustInTheWind.ClockWpf.Demo.TimeProviders;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.TimeProviders;

namespace DustInTheWind.ClockWpf.Demo;

internal class MainViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;

    //public ObservableCollection<TemplateInfo> TemplateTypes { get; } = [];

    //public TemplateInfo SelectedTemplateType
    //{
    //    get => field;
    //    set
    //    {
    //        if (field == value)
    //            return;

    //        field = value;
    //        OnPropertyChanged();

    //        if (!IsInitializing)
    //        {
    //            applicationState.CurrentTemplate = field == null
    //                ? null
    //                : (ClockTemplate)Activator.CreateInstance(field.Type);
    //        }
    //    }
    //}

    //public Shape SelectedShape
    //{
    //    get => field;
    //    set
    //    {
    //        if (field == value)
    //            return;

    //        field = value;
    //        OnPropertyChanged();

    //        if (!IsInitializing)
    //            applicationState.CurrentShape = value;
    //    }
    //}

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
            //if (applicationState.AvailableTemplateTypes != null)
            //{
            //    foreach (Type type in applicationState.AvailableTemplateTypes)
            //    {
            //        TemplateTypes.Add(new TemplateInfo
            //        {
            //            Name = type.Name
            //                .Replace("ClockTemplate", "")
            //                .Replace("Template", ""),
            //            Type = type
            //        });
            //    }
            //}

            //if (applicationState.CurrentTemplate != null)
            //{
            //    Type currentTymplateType = applicationState.CurrentTemplate.GetType();

            //    SelectedTemplateType = TemplateTypes
            //        .FirstOrDefault(x => x.Type == currentTymplateType);
            //}

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
