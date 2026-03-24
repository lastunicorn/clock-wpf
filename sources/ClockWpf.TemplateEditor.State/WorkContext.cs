using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.State;

public class WorkContext
{
    private readonly IClockTemplateFactory clockTemplateFactory;

    public Type ClockTemplateType { get; }

    public string TemplateName { get; }

    public string Description { get; }

    public ClockTemplate ClockTemplate { get; private set; }

    public List<Shape> Shapes { get; } = [];

    public bool CanReset => ClockTemplate?.IsNew == false;

    public WorkContextState State
    {
        get => field;
        private set
        {
            if (field == value)
                return;

            field = value;
            OnStateChanged();
        }
    }

    public event EventHandler StateChanged;

    protected virtual void OnStateChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    public WorkContext(Type type, IClockTemplateFactory clockTemplateFactory)
    {
        ClockTemplateType = type ?? throw new ArgumentNullException(nameof(type));
        this.clockTemplateFactory = clockTemplateFactory ?? throw new ArgumentNullException(nameof(clockTemplateFactory));

        bool isClockTemplate = typeof(ClockTemplate).IsAssignableFrom(type);
        if (!isClockTemplate)
            throw new ArgumentException($"The type {type.FullName} is not a clock template.", nameof(type));

        ClockTemplateMetadata clockTemplateMetadata = type.AsClockTemplateMetadata();
        TemplateName = clockTemplateMetadata.Name;
        Description = clockTemplateMetadata.Description;

        State = WorkContextState.Closed;
    }

    public void Open()
    {
        if (ClockTemplate != null)
            return;

        ClockTemplate instance = clockTemplateFactory.Create(ClockTemplateType);

        if (instance == null)
            throw new Exception("Clock template could not be created by the factory. Verify that the type was registerd into the dependency container.");

        ClockTemplate = instance;
        State = WorkContextState.Opened;
    }

    public void Reset()
    {
        ClockTemplate instance = clockTemplateFactory.Create(ClockTemplateType);

        if (instance == null)
            throw new Exception("Clock template could not be created by the factory. Verify that the type was registerd into the dependency container.");

        ClockTemplate = instance;
        Shapes.Clear();

        State = WorkContextState.Opened;
    }
}
