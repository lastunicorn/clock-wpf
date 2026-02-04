using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DustInTheWind.ClockWpf.TemplateEditor.CustomControls;

internal class ResetViewCommand : ICommand
{
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter)
    {
        if (parameter is not FrameworkElement targetElement)
            return false;

        return targetElement.RenderTransform is TransformGroup;
    }

    public void Execute(object parameter)
    {
        if (parameter is not FrameworkElement targetElement)
            return;

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
