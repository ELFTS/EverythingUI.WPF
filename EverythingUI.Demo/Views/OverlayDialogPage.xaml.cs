using System.Windows;
using System.Windows.Controls;

namespace EverythingUI.Demo.Views;

public partial class OverlayDialogPage : UserControl
{
    public OverlayDialogPage() => InitializeComponent();

    private void OpenDialogButton_Click(object sender, RoutedEventArgs e)
    {
        if (Window.GetWindow(this) is MainWindow window) window.OpenBasicDialog();
    }

    private void OpenCustomDialogButton_Click(object sender, RoutedEventArgs e)
    {
        if (Window.GetWindow(this) is MainWindow window) window.OpenCustomDialog();
    }
}
