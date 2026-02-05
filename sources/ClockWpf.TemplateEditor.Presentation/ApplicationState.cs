using DustInTheWind.ClockWpf.Movements;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation;

public class ApplicationState
{
    public List<Type> AvailableTemplateTypes { get; set; }

    public ClockTemplate CurrentTemplate
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnCurrentTemplateChanged();
        }
    }

    public List<Type> AvailableMovementTypes { get; set; }

    public IMovement CurrentMovement
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnCurrentMovementChanged();
        }
    }

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

    public event EventHandler CurrentTemplateChanged;
    public event EventHandler CurrentMovementChanged;
    public event EventHandler ClockDirectionChanged;

    private void OnCurrentTemplateChanged()
    {
        CurrentTemplateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnCurrentMovementChanged()
    {
        CurrentMovementChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnClockDirectionChanged()
    {
        ClockDirectionChanged?.Invoke(this, EventArgs.Empty);
    }
}