using DustInTheWind.ClockWpf.TemplateEditor.State;
using DustInTheWind.ClockWpf.Templates;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.ShapesArea;

public class ShapesViewModel : ViewModelBase
{
    private readonly WorkContextPool workContextPool;

    public InUseShapesViewModel InUseShapesViewModel { get; }

    public AvailableShapesViewModel AvailableShapesViewModel { get; }

    public string TemplateName
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

    public ShapesViewModel(InUseShapesViewModel inUseShapesViewModel, AvailableShapesViewModel availableShapesViewModel, WorkContextPool workContextPool)
    {
        InUseShapesViewModel = inUseShapesViewModel ?? throw new ArgumentNullException(nameof(inUseShapesViewModel));
        AvailableShapesViewModel = availableShapesViewModel ?? throw new ArgumentNullException(nameof(availableShapesViewModel));
        this.workContextPool = workContextPool ?? throw new ArgumentNullException(nameof(workContextPool));

        this.workContextPool.CurrentWorkContextChanged += HandleCurrentWorkContextChanged;
        UpdateTemplateName();
    }

    private void HandleCurrentWorkContextChanged(object sender, CurrentWorkContextChangedEventArgs e)
    {
        UpdateTemplateName();
    }

    private void UpdateTemplateName()
    {
        TemplateName = workContextPool.CurrentWorkContext?.TemplateName ?? string.Empty;
    }
}
