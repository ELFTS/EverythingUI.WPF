using System.Windows;
using System.Windows.Input;

namespace EverythingUI.WPF.Controls;

public class EverythingListViewItemEventArgs(object clickedItem, MouseButtonEventArgs? mouseEventArgs = null) : EventArgs
{
    public object ClickedItem => clickedItem;
    public MouseButtonEventArgs? MouseEventArgs => mouseEventArgs;
}
