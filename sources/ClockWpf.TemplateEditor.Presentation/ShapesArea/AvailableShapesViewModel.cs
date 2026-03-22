using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.ShapesArea;

public class AvailableShapesViewModel : ViewModelBase
{
    public ObservableCollection<ShapeDescriptor> Shapes { get; } = [];

    public ShapeDescriptor SelectedShape
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

    public AvailableShapesViewModel()
    {
        Initialize();
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            foreach (Type type in LoadStyles())
            {
                Shapes.Add(new ShapeDescriptor(type));
            }
        });
    }

    private static IEnumerable<Type> LoadStyles()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && typeof(Shape).IsAssignableFrom(x));
    }
}
