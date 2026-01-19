using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.TimeProviders;

namespace DustInTheWind.ClockWpf.ClearClock.Controls;

public class ClockPageModel : ViewModelBase
{
    private readonly ApplicationState applicationState;

    public ClockTemplate ClockTemplate
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

    public ITimeProvider TimeProvider
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

    public ClockPageModel(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

        applicationState.ClockTemplateChanged += HandleClockTemplateChanged;
        ClockTemplate = applicationState.ClockTemplate;

        TimeProvider = new LocalTimeProvider();
        TimeProvider.Start();
        this.applicationState = applicationState;
    }

    private void HandleClockTemplateChanged(object sender, EventArgs e)
    {
        ClockTemplate = applicationState.ClockTemplate;
    }
}
