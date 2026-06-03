using System.Windows;
using System.Windows.Input;

namespace EverythingUI.WPF.Controls;

public class EverythingIconListBoxItemClickEventArgs(object clickedItem, MouseButtonEventArgs? mouseEventArgs = null) : EventArgs
{
    public object ClickedItem => clickedItem;
    public MouseButtonEventArgs? MouseEventArgs => mouseEventArgs;
}
