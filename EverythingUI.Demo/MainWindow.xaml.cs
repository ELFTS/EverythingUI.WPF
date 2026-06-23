using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using EverythingUI.Demo.Views;
using EverythingUI.WPF.Controls;
using EverythingUI.WPF.Themes;

namespace EverythingUI.Demo;

public partial class MainWindow : Window
{
    private readonly Dictionary<string, UserControl> _pages = new();

    public object SelectedCategory { get => GetValue(SelectedCategoryProperty); set => SetValue(SelectedCategoryProperty, value); }
    public static readonly DependencyProperty SelectedCategoryProperty =
        DependencyProperty.Register(nameof(SelectedCategory), typeof(object), typeof(MainWindow),
            new PropertyMetadata(null, OnSelectedCategoryChanged));

    public MainWindow()
    {
        InitializeComponent();
        InitializePages();
        InitializeSideBarItems();
        ColorComboBox.ItemsSource = Enum.GetValues<ColorName>();
        ColorComboBox.SelectedItem = ThemeManager.CurrentColorName;
    }

    private void InitializePages()
    {
        var names = new[] { "按钮", "开关", "复选框", "单选框", "输入框", "组合框", "滑块", "进度条", "圆形进度条", "卡片", "遮罩对话框", "侧边栏", "工具栏", "图标列表框" };
        var types = new[] { typeof(ButtonPage), typeof(ToggleSwitchPage), typeof(CheckBoxPage), typeof(RadioButtonPage),
            typeof(TextBoxPage), typeof(ComboBoxPage), typeof(SliderPage), typeof(ProgressBarPage),
            typeof(CircularProgressBarPage), typeof(CardPage), typeof(OverlayDialogPage), typeof(SideBarPage), typeof(ToolBarPage), typeof(IconListBoxPage) };
        for (int i = 0; i < names.Length; i++) _pages[names[i]] = (UserControl)Activator.CreateInstance(types[i])!;
    }

    private void InitializeSideBarItems()
    {
        var items = _pages.Keys.Select(t => new EverythingSideBarItem { Text = t }).ToArray();
        if (FindName("SideBar") is EverythingSideBar sideBar)
        {
            sideBar.ItemsSource = items;
            SelectedCategory = items[0];
        }
        else SelectedCategory = new EverythingSideBarItem { Text = "按钮" };
    }

    private static void OnSelectedCategoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is MainWindow w && e.NewValue is EverythingSideBarItem item) w.NavigateTo(item.Text);
    }

    private void NavigateTo(string pageName)
    {
        if (_pages.TryGetValue(pageName, out var page)) ContentFrame.Content = page;
    }

    public void OpenBasicDialog()
    {
        SetFrostedBackground(true);
        BasicDialog.IsOpen = true;
    }

    public void OpenCustomDialog()
    {
        SetFrostedBackground(true, 24);
        CustomDialog.IsOpen = true;
    }

    private void CloseBasicDialogButton_Click(object sender, RoutedEventArgs e)
    {
        BasicDialog.IsOpen = false;
        SetFrostedBackground(false);
    }

    private void CloseCustomDialogButton_Click(object sender, RoutedEventArgs e)
    {
        CustomDialog.IsOpen = false;
        SetFrostedBackground(false);
    }

    private void SetFrostedBackground(bool enabled, double radius = 18)
    {
        var effect = enabled ? new BlurEffect { Radius = radius, RenderingBias = RenderingBias.Quality } : null;
        TopBar.Effect = effect;
        SideBar.Effect = effect;
        ContentScrollViewer.Effect = effect;
    }

    private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ColorComboBox.SelectedItem is ColorName cn) ThemeManager.ChangeColor(cn);
    }
}
