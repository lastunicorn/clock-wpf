using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates;
using DustInTheWind.ClockWpf.TimeProviders;

namespace DustInTheWind.ClockWpf.Demo;

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

    public Shape CurrentShape
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnCurrentShapeChanged();
        }
    }

    public ITimeProvider CurrentTimeProvider
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            OnCurrentTimeProviderChanged();
        }
    }

    public event EventHandler CurrentTemplateChanged;
    public event EventHandler CurrentShapeChanged;
    public event EventHandler CurrentTimeProviderChanged;

    public void OnCurrentTemplateChanged()
    {
        CurrentTemplateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void OnCurrentShapeChanged()
    {
        CurrentShapeChanged?.Invoke(this, EventArgs.Empty);
    }

    public void OnCurrentTimeProviderChanged()
    {
        CurrentTimeProviderChanged?.Invoke(this, EventArgs.Empty);
    }
}