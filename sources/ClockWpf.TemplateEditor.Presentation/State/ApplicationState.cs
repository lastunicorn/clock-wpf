using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.State;

public class ApplicationState
{
    public RotationDirection ClockDirection
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnClockDirectionChanged();
        }
    }

    public event EventHandler ClockDirectionChanged;

    private void OnClockDirectionChanged()
    {
        ClockDirectionChanged?.Invoke(this, EventArgs.Empty);
    }
}