using System.Windows;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        CabinetWindow cabinetWindow = new()
        {
            Owner = this
        };
        cabinetWindow.Show();
    }
}
