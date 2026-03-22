using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Shapes;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;
using DustInTheWind.ClockWpf.TemplateEditor.State;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.ShapesArea;

public class InUseShapesViewModel : ViewModelBase
{
    private readonly ApplicationState applicationState;

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

    public InUseShapesViewModel(ClockTemplatePool clockTemplatePool, ApplicationState applicationState)
    {
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));

        AvailableShapes = [];

        Initialize();

        applicationState.ClockShapesChanged += HandleClockShapesChanged;
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            Shapes = applicationState.ClockShapes;

            foreach (Type type in LoadStyles())
            {
                AvailableShapes.Add(new ShapeDescriptor(type));
            }
        });
    }

    private static IEnumerable<Type> LoadStyles()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && typeof(Shape).IsAssignableFrom(x));
    }

    private void HandleClockShapesChanged(object sender, EventArgs e)
    {
        Shapes = applicationState.ClockShapes;
    }
}
