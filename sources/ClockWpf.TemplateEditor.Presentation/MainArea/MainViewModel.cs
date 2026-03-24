using DustInTheWind.ClockWpf.TemplateEditor.Presentation.CabinetArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.MiscellaneousArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.MovementsArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.ShapesArea;
using DustInTheWind.ClockWpf.TemplateEditor.Presentation.TemplatesArea;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.MainArea;

public class MainViewModel : ViewModelBase
{
    public ClockViewModel ClockViewModel { get; }

    public MiscellaneousViewModel MiscellaneousViewModel { get; }

    public TemplatesViewModel TemplatesViewModel { get; }

    public ShapesViewModel ShapesViewModel { get; }

    public MovementsViewModel MovementsViewModel { get; }

    public CabinetViewModel CabinetViewModel { get; }

    public MainViewModel(
        ClockViewModel clockViewModel,
        MiscellaneousViewModel miscellaneousViewModel,
        TemplatesViewModel templatesViewModel,
        ShapesViewModel shapesViewModel,
        MovementsViewModel movementsViewModel,
        CabinetViewModel cabinetViewModel)
    {
        ClockViewModel = clockViewModel ?? throw new ArgumentNullException(nameof(clockViewModel));
        MiscellaneousViewModel = miscellaneousViewModel ?? throw new ArgumentNullException(nameof(miscellaneousViewModel));
        TemplatesViewModel = templatesViewModel ?? throw new ArgumentNullException(nameof(templatesViewModel));
        ShapesViewModel = shapesViewModel ?? throw new ArgumentNullException(nameof(shapesViewModel));
        MovementsViewModel = movementsViewModel ?? throw new ArgumentNullException(nameof(movementsViewModel));
        CabinetViewModel = cabinetViewModel ?? throw new ArgumentNullException(nameof(cabinetViewModel));
    }
}
