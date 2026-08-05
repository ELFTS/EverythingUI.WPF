using System.Windows;
using System.Windows.Controls;
using EverythingUI.WPF.Controls;

namespace EverythingUI.Demo.Views;

public partial class ContextMenuPage : UserControl
{
    public ContextMenuPage()
    {
        InitializeComponent();
    }

    private void BasicMenu_ItemClick(object sender, EverythingContextMenuItemClickEventArgs e)
        => AppendLog($"[基础菜单] 点击了：{e.ClickedItem.Text}");

    private void TextOnlyMenu_ItemClick(object sender, EverythingContextMenuItemClickEventArgs e)
        => AppendLog($"[仅文字菜单] 点击了：{e.ClickedItem.Text}");

    private void DisabledMenu_ItemClick(object sender, EverythingContextMenuItemClickEventArgs e)
        => AppendLog($"[禁用项菜单] 点击了：{e.ClickedItem.Text}");

    private void AppendLog(string line)
    {
        if (EventLog.Text.StartsWith("右击上方任意区域触发菜单"))
            EventLog.Text = line;
        else
            EventLog.Text += Environment.NewLine + line;
    }
}
