using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Miscellaneous;

public class MiscellaneousViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;

    public RotationDirection ClockDirection
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();

            if (!IsInitializing)
                applicationState.ClockDirection = value;
        }
    }

    public MiscellaneousViewModel(ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

        applicationState.ClockDirectionChanged += HandleClockDirectionChanged;

        Initialize();
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            ClockDirection = applicationState.ClockDirection;
        });
    }

    private void HandleClockDirectionChanged(object sender, EventArgs e)
    {
        Initialize(() =>
        {
            ClockDirection = applicationState.ClockDirection;
        });
    }
}
