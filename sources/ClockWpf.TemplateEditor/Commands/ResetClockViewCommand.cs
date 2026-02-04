using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.TemplateEditor.Commands;

internal class ResetClockViewCommand : ICommand
{
    private readonly FrameworkElement targetElement;

    public event EventHandler CanExecuteChanged
    {
        add { }
        remove { }
    }

    public ResetClockViewCommand(FrameworkElement targetElement)
    {
        this.targetElement = targetElement ?? throw new ArgumentNullException(nameof(targetElement));
    }

    public bool CanExecute(object parameter)
    {
        return targetElement.RenderTransform is TransformGroup;
    }

    public void Execute(object parameter)
    {
        if (targetElement.RenderTransform is not TransformGroup transformGroup)
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
