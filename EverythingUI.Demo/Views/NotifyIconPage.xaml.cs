using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EverythingUI.WPF.Controls;

namespace EverythingUI.Demo.Views;

public partial class NotifyIconPage : UserControl
{
    public NotifyIconPage()
    {
        InitializeComponent();
    }

    private void ShowIcon_Click(object sender, RoutedEventArgs e)
    {
        NotifyIcon.ToolTipText = ToolTipInput.Text;
        NotifyIcon.Visible = true;
        AppendLog("托盘图标已显示，请在任务栏通知区域查看。");
    }

    private void HideIcon_Click(object sender, RoutedEventArgs e)
    {
        NotifyIcon.Visible = false;
        AppendLog("托盘图标已隐藏。");
    }

    private void Balloon_Click(object sender, RoutedEventArgs e)
    {
        if (!NotifyIcon.Visible)
        {
            AppendLog("请先显示托盘图标。");
            return;
        }
        NotifyIcon.ShowBalloonTip("EverythingUI", "这是一条来自托盘图标的气泡通知！");
        AppendLog("已发送气泡通知。");
    }

    private void NotifyIcon_MouseClick(object sender, NotifyIconMouseEventArgs e)
        => AppendLog($"托盘图标 {e.Button} 键点击");

    private void NotifyIcon_MouseDoubleClick(object sender, NotifyIconMouseEventArgs e)
        => AppendLog($"托盘图标 {e.Button} 键双击");

    private void TrayMenu_ItemClick(object sender, EverythingContextMenuItemClickEventArgs e)
    {
        var text = e.ClickedItem.Text;
        AppendLog($"菜单点击：{text}");
        switch (text)
        {
            case "打开主窗口":
                if (Window.GetWindow(this) is Window w) w.Activate();
                break;
            case "显示气泡通知":
                NotifyIcon.ShowBalloonTip("EverythingUI", "这是一条来自托盘图标的气泡通知！");
                break;
            case "隐藏托盘图标":
                NotifyIcon.Visible = false;
                break;
        }
    }

    private void AppendLog(string line)
    {
        if (StatusLog.Text.StartsWith("点击"))
            StatusLog.Text = line;
        else
            StatusLog.Text += Environment.NewLine + line;
    }
}
