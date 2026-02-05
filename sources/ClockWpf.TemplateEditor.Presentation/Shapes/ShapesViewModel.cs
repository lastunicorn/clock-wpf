using System.Collections.ObjectModel;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Shapes;

public class ShapesViewModel : ViewModelBase
{
    public ObservableCollection<ShapeInfo> Shapes { get; } = [];

    public ShapeInfo SelectedShape
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

    public ShapesViewModel()
    {
        Initialize();
    }

    private void Initialize()
    {
        Initialize(() =>
        {
            foreach (Type type in LoadStyles())
            {
                Shapes.Add(new ShapeInfo
                {
                    Name = type.Name.Replace("Shape", ""),
                    Type = type
                });
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
