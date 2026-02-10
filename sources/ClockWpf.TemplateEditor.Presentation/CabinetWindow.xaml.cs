using System.Windows;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation;

/// <summary>
/// Interaction logic for CabinetWindow.xaml
/// </summary>
public partial class CabinetWindow : Window
{
    public CabinetWindow()
    {
        InitializeComponent();

        DataContext = new CabinetViewModel();
    }
}
