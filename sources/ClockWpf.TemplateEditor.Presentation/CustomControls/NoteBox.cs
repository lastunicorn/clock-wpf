using System.Windows;
using System.Windows.Controls;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.CustomControls;

public class NoteBox : ContentControl
{
    #region NoteType DependencyProperty

    public static readonly DependencyProperty NoteTypeProperty = DependencyProperty.Register(
        nameof(NoteType),
        typeof(NoteType),
        typeof(NoteBox),
        new PropertyMetadata(NoteType.Info));

    public NoteType NoteType
    {
        get => (NoteType)GetValue(NoteTypeProperty);
        set => SetValue(NoteTypeProperty, value);
    }

    #endregion

    static NoteBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NoteBox), new FrameworkPropertyMetadata(typeof(NoteBox)));
    }
}
