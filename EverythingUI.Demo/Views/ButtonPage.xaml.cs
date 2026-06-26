using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EverythingUI.Demo.Views;

public partial class ButtonPage : UserControl
{
    public ButtonPage() => InitializeComponent();

    private void LongPressButton_Click(object sender, MouseButtonEventArgs e) =>
        MessageBox.Show("长按已触发", "EverythingButton");
}
