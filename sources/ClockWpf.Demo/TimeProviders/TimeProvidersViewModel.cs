using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.TimeProviders;

namespace DustInTheWind.ClockWpf.Demo.TimeProviders;

public class TimeProvidersViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;

    public ObservableCollection<TimeProviderDescriptor> TimeProviderTypes { get; } = [];

    public TimeProviderDescriptor SelectedTimeProviderType
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
                applicationState.CurrentTimeProvider = field == null
                    ? null
                    : (ITimeProvider)Activator.CreateInstance(field.Type);

                applicationState.CurrentTimeProvider?.Start();
            }
        }
    }

    public ITimeProvider SelectedTimeProvider
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

    public TimeProvidersViewModel(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

        Initialize();

        applicationState.CurrentTimeProviderChanged += HandleCurrentTimeProviderChanged;
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            if (applicationState.AvailableTimeProviderTypes != null)
            {
                foreach (Type type in applicationState.AvailableTimeProviderTypes)
                {
                    TimeProviderTypes.Add(new TimeProviderDescriptor
                    {
                        Name = type.Name
                            .Replace("TimeProvider", "")
                            .Replace("Provider", ""),
                        Type = type
                    });
                }
            }

            if (applicationState.CurrentTimeProvider != null)
            {
                Type currentTimeProviderType = applicationState.CurrentTimeProvider.GetType();

                SelectedTimeProviderType = TimeProviderTypes
                    .FirstOrDefault(x => x.Type == currentTimeProviderType);
            }

            SelectedTimeProvider = applicationState.CurrentTimeProvider;
        });
    }

    private void HandleCurrentTimeProviderChanged(object sender, EventArgs e)
    {
        SelectedTimeProvider = applicationState.CurrentTimeProvider;
    }
}
