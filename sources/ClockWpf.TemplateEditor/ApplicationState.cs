using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.Movements;

namespace DustInTheWind.ClockWpf.TemplateEditor;

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

    public event EventHandler CurrentTemplateChanged;
    public event EventHandler CurrentMovementChanged;

    public void OnCurrentTemplateChanged()
    {
        CurrentTemplateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void OnCurrentMovementChanged()
    {
        CurrentMovementChanged?.Invoke(this, EventArgs.Empty);
    }
}