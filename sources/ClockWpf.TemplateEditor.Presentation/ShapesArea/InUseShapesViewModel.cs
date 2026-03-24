using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.ShapesArea;

public class InUseShapesViewModel : ViewModelBase
{
    private readonly WorkContextPool workContextPool;

    public ObservableCollection<Shape> Shapes
    {
        get => field;
        private set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();
        }
    }

    public Shape SelectedShape
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

    public ObservableCollection<ShapeDescriptor> AvailableShapes
    {
        get => field;
        private set
        {
            if (field == value)
                return;

            field = value;
            OnPropertyChanged();
        }
    }

    public InUseShapesViewModel(WorkContextPool workContextPool)
    {
        this.workContextPool = workContextPool ?? throw new ArgumentNullException(nameof(workContextPool));

        Shapes = [];
        AvailableShapes = [];

        workContextPool.CurrentWorkContextChanged += HandleCurrentTemplateEditContextChanged;

        Initialize();
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            UpdateShapesFromWorkContext();

            if (workContextPool.CurrentWorkContext != null)
                workContextPool.CurrentWorkContext.ShapesCreated += HandleContextShapesCreated;

            foreach (Type type in LoadStyles())
                AvailableShapes.Add(new ShapeDescriptor(type));
        });
    }

    private static IEnumerable<Type> LoadStyles()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && typeof(Shape).IsAssignableFrom(x));
    }

    private void HandleCurrentTemplateEditContextChanged(object sender, CurrentWorkContextChangedEventArgs e)
    {
        if (e.OldContext != null)
            e.OldContext.ShapesCreated -= HandleContextShapesCreated;

        UpdateShapesFromWorkContext();

        if (e.NewContext != null)
            e.NewContext.ShapesCreated += HandleContextShapesCreated;
    }

    private void HandleContextShapesCreated(object sender, EventArgs e)
    {
        UpdateShapesFromWorkContext();
    }

    private void UpdateShapesFromWorkContext()
    {
        Shapes.Clear();

        if (workContextPool.CurrentWorkContext?.Shapes != null)
        {
            foreach (Shape shape in workContextPool.CurrentWorkContext.Shapes)
                Shapes.Add(shape);
        }
    }
}
