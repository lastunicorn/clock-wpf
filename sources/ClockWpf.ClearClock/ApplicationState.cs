using DustInTheWind.ClockWpf.ClearClock.Controls;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.ClearClock;

public class ApplicationState
{
    public List<Type> AvailableTemplateTypes { get; set; }

    public ClockTemplate ClockTemplate
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnClockTemplateChanged();
        }
    }

    public event EventHandler ClockTemplateChanged;

    public void OnClockTemplateChanged()
    {
        ClockTemplateChanged?.Invoke(this, EventArgs.Empty);
    }
}

public class TemplateDescriptor
{
    public string Name { get; set; }

    public Type Type { get; set; }
}