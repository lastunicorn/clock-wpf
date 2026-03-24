using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

    private ObservableCollection<Shape> shapes;

    public ObservableCollection<Shape> Shapes
    {
        get => shapes;
        private set
        {
            if (shapes != null)
            {
                shapes.CollectionChanged -= HandleShapesCollectionChanged;
                foreach (Shape shape in shapes)
                    shape.Changed -= HandleShapeChanged;
            }

            shapes = value;

            if (shapes != null)
            {
                shapes.CollectionChanged += HandleShapesCollectionChanged;
                foreach (Shape shape in shapes)
                    shape.Changed += HandleShapeChanged;
            }
        }
    }

    public bool CanReset => State == WorkContextState.Modified;

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

    public event EventHandler ShapesCreated;

    private void OnShapesCreated()
    {
        ShapesCreated?.Invoke(this, EventArgs.Empty);
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
        if (State != WorkContextState.Closed)
            throw new InvalidOperationException("Only closed work contexts can be opened.");

        if (ClockTemplate != null)
            return;

        ClockTemplate instance = clockTemplateFactory.Create(ClockTemplateType);

        if (instance == null)
            throw new Exception("Clock template could not be created by the factory. Verify that the type was registerd into the dependency container.");

        ClockTemplate = instance;
        State = WorkContextState.New;
    }

    public void Reset()
    {
        if (State == WorkContextState.Closed)
            throw new InvalidOperationException("Closed work contexts cannot be reset.");

        ClockTemplate instance = clockTemplateFactory.Create(ClockTemplateType);

        if (instance == null)
            throw new Exception("Clock template could not be created by the factory. Verify that the type was registerd into the dependency container.");

        ClockTemplate = instance;

        if (Shapes != null)
        {
            foreach (Shape shape in Shapes)
                shape.Changed -= HandleShapeChanged;

            Shapes.Clear();
        }

        State = WorkContextState.New;
    }

    public void SetShapes(IEnumerable<Shape> shapes)
    {
        if (State == WorkContextState.Closed)
            throw new InvalidOperationException("Work context must be opened.");

        Shapes = new ObservableCollection<Shape>(shapes);

        State = WorkContextState.Unmodified;
        OnShapesCreated();
    }

    private void HandleShapesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems != null)
        {
            foreach (Shape shape in e.OldItems)
                shape.Changed -= HandleShapeChanged;
        }

        if (e.NewItems != null)
        {
            foreach (Shape shape in e.NewItems)
                shape.Changed += HandleShapeChanged;
        }

        if (State != WorkContextState.Closed && State != WorkContextState.New)
            State = WorkContextState.Modified;
    }

    private void HandleShapeChanged(object sender, EventArgs e)
    {
        if (State != WorkContextState.Closed && State != WorkContextState.New)
            State = WorkContextState.Modified;
    }
}
