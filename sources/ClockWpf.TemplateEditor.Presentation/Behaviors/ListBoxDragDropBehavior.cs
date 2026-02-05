using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DustInTheWind.ClockWpf.Shapes;

namespace DustInTheWind.ClockWpf.TemplateEditor.Presentation.Behaviors;

public static class ListBoxDragDropBehavior
{
    private static readonly DependencyProperty DragDropStateProperty = DependencyProperty.RegisterAttached(
        "DragDropState",
        typeof(DragDropState),
        typeof(ListBoxDragDropBehavior),
        new PropertyMetadata(null));

    public static readonly DependencyProperty EnableDragDropProperty = DependencyProperty.RegisterAttached(
        "EnableDragDrop",
        typeof(bool),
        typeof(ListBoxDragDropBehavior),
        new PropertyMetadata(false, OnEnableDragDropChanged));

    public static bool GetEnableDragDrop(DependencyObject obj)
    {
        return (bool)obj.GetValue(EnableDragDropProperty);
    }

    public static void SetEnableDragDrop(DependencyObject obj, bool value)
    {
        obj.SetValue(EnableDragDropProperty, value);
    }

    private static void OnEnableDragDropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ListBox listBox)
            return;

        if ((bool)e.NewValue)
        {
            DragDropState state = new();
            listBox.SetValue(DragDropStateProperty, state);

            listBox.PreviewMouseLeftButtonDown += HandlePreviewMouseLeftButtonDown;
            listBox.PreviewMouseMove += HandlePreviewMouseMove;
            listBox.Drop += HandleDrop;
            listBox.AllowDrop = true;
        }
        else
        {
            listBox.PreviewMouseLeftButtonDown -= HandlePreviewMouseLeftButtonDown;
            listBox.PreviewMouseMove -= HandlePreviewMouseMove;
            listBox.Drop -= HandleDrop;
            listBox.AllowDrop = false;

            listBox.ClearValue(DragDropStateProperty);
        }
    }

    private static void HandlePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not ListBox listBox)
            return;

        DragDropState state = (DragDropState)listBox.GetValue(DragDropStateProperty);
        state.StartPoint = e.GetPosition(null);

        DependencyObject dep = (DependencyObject)e.OriginalSource;
        ListBoxItem listBoxItem = FindAncestor<ListBoxItem>(dep);

        if (listBoxItem != null)
            state.DraggedItem = listBoxItem.DataContext as Shape;
        else
            state.DraggedItem = null;
    }

    private static void HandlePreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (sender is not ListBox listBox)
            return;

        DragDropState state = (DragDropState)listBox.GetValue(DragDropStateProperty);

        if (e.LeftButton == MouseButtonState.Pressed && state.DraggedItem != null)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = state.StartPoint - mousePos;

            if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                DataObject dragData = new(typeof(Shape), state.DraggedItem);
                DragDrop.DoDragDrop(listBox, dragData, DragDropEffects.Move);
            }
        }
    }

    private static void HandleDrop(object sender, DragEventArgs e)
    {
        if (sender is not ListBox listBox)
            return;

        if (!e.Data.GetDataPresent(typeof(Shape)))
            return;

        Shape droppedItem = e.Data.GetData(typeof(Shape)) as Shape;

        if (droppedItem == null)
            return;

        if (listBox.ItemsSource is not ObservableCollection<Shape> shapes)
            return;

        DependencyObject dep = (DependencyObject)e.OriginalSource;
        ListBoxItem targetItem = FindAncestor<ListBoxItem>(dep);

        if (targetItem == null)
            return;

        Shape targetShape = targetItem.DataContext as Shape;

        if (targetShape == null || droppedItem == targetShape)
            return;

        int droppedIndex = shapes.IndexOf(droppedItem);
        int targetIndex = shapes.IndexOf(targetShape);

        if (droppedIndex >= 0 && targetIndex >= 0)
        {
            shapes.Move(droppedIndex, targetIndex);
        }
    }

    private static T FindAncestor<T>(DependencyObject current)
        where T : DependencyObject
    {
        do
        {
            if (current is T ancestor)
                return ancestor;

            current = System.Windows.Media.VisualTreeHelper.GetParent(current);
        }
        while (current != null);

        return null;
    }
}
