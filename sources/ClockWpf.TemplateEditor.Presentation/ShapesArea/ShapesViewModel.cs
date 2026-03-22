namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.ShapesArea;

public class ShapesViewModel : ViewModelBase
{
    public InUseShapesViewModel InUseShapesViewModel { get; }

    public AvailableShapesViewModel AvailableShapesViewModel { get; }

    public ShapesViewModel(InUseShapesViewModel inUseShapesViewModel, AvailableShapesViewModel availableShapesViewModel)
    {
        InUseShapesViewModel = inUseShapesViewModel ?? throw new ArgumentNullException(nameof(inUseShapesViewModel));
        AvailableShapesViewModel = availableShapesViewModel ?? throw new ArgumentNullException(nameof(availableShapesViewModel));
    }
}
