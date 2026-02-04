using System.Windows;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.TemplateEditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ResetClockView_Click(object sender, RoutedEventArgs e)
    {
        if (analogClock1.RenderTransform is not TransformGroup transformGroup)
            return;

        ScaleTransform scaleTransform = FindScaleTransform(transformGroup);
        TranslateTransform translateTransform = FindTranslateTransform(transformGroup);

        if (scaleTransform != null)
        {
            scaleTransform.ScaleX = 1.0;
            scaleTransform.ScaleY = 1.0;
        }

        if (translateTransform != null)
        {
            translateTransform.X = 0.0;
            translateTransform.Y = 0.0;
        }
    }

    private static ScaleTransform FindScaleTransform(TransformGroup transformGroup)
    {
        foreach (Transform transform in transformGroup.Children)
        {
            if (transform is ScaleTransform scaleTransform)
                return scaleTransform;
        }

        return null;
    }

    private static TranslateTransform FindTranslateTransform(TransformGroup transformGroup)
    {
        foreach (Transform transform in transformGroup.Children)
        {
            if (transform is TranslateTransform translateTransform)
                return translateTransform;
        }

        return null;
    }
}
