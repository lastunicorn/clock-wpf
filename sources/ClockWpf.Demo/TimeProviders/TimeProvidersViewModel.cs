namespace DustInTheWind.ClockWpf.Demo.TimeProviders;

public class TimeProvidersViewModel : ViewModelBase
{
    private ApplicationState applicationState;

    public TimeProvidersViewModel(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
    }
}
