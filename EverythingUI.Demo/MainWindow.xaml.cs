using System.Windows;
using System.Windows.Controls;
using EverythingUI.Demo.Views;
using EverythingUI.WPF.Controls;

namespace EverythingUI.Demo;

public partial class MainWindow : Window
{
    private readonly Dictionary<string, UserControl> _pages = new();

    public object SelectedCategory
    {
        get => GetValue(SelectedCategoryProperty);
        set => SetValue(SelectedCategoryProperty, value);
    }

    public static readonly DependencyProperty SelectedCategoryProperty =
        DependencyProperty.Register(nameof(SelectedCategory), typeof(object), typeof(MainWindow),
            new PropertyMetadata(null, OnSelectedCategoryChanged));

    public MainWindow()
    {
        InitializeComponent();
        InitializePages();
        InitializeSideBarItems();
    }

    private void InitializePages()
    {
        _pages["按钮"] = new ButtonPage();
        _pages["开关"] = new ToggleSwitchPage();
        _pages["输入框"] = new TextBoxPage();
        _pages["组合框"] = new ComboBoxPage();
        _pages["卡片"] = new CardPage();
        _pages["侧边栏"] = new SideBarPage();
        _pages["综合示例"] = new ExamplesPage();
    }

    private void InitializeSideBarItems()
    {
        // 创建菜单项
        var items = new[]
        {
            new EverythingSideBarItem { Text = "按钮" },
            new EverythingSideBarItem { Text = "开关" },
            new EverythingSideBarItem { Text = "输入框" },
            new EverythingSideBarItem { Text = "组合框" },
            new EverythingSideBarItem { Text = "卡片" },
            new EverythingSideBarItem { Text = "侧边栏" },
            new EverythingSideBarItem { Text = "综合示例" }
        };

        // 找到侧边栏控件并设置数据源
        if (FindName("SideBar") is EverythingSideBar sideBar)
        {
            sideBar.ItemsSource = items;
            SelectedCategory = items[0];
        }
        else
        {
            // 如果找不到控件，使用默认选中项
            SelectedCategory = new EverythingSideBarItem { Text = "按钮" };
        }
    }

    private static void OnSelectedCategoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is MainWindow window && e.NewValue is EverythingSideBarItem item)
        {
            window.NavigateTo(item.Text);
        }
    }

    private void NavigateTo(string pageName)
    {
        if (_pages.TryGetValue(pageName, out var page))
        {
            ContentFrame.Content = page;
        }
    }
}
