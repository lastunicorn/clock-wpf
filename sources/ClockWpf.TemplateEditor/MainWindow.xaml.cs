using System.Windows;
using DustInTheWind.ClockWpf.TemplateEditor.Commands;

namespace DustInTheWind.ClockWpf.TemplateEditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        Loaded += HandleLoaded;
    }

    private void HandleLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.ResetClockViewCommand = new ResetClockViewCommand(analogClock1);
        }
    }
}
