using System.Windows;

namespace EverythingUI.WPF.Controls;

public class EverythingContextMenuItemClickEventArgs : RoutedEventArgs
{
    public EverythingContextMenuItem ClickedItem { get; }

    public EverythingContextMenuItemClickEventArgs(EverythingContextMenuItem item)
    {
        ClickedItem = item;
    }
}
