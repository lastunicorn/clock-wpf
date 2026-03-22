using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.TemplateEditor.State;

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

    public ObservableCollection<Shape> ClockShapes
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnClockShapesChanged();
        }
    }

    public event EventHandler ClockShapesChanged;

    private void OnClockDirectionChanged()
    {
        ClockDirectionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnClockShapesChanged()
    {
        ClockShapesChanged?.Invoke(this, EventArgs.Empty);
    }
}