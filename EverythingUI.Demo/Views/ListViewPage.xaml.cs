using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using EverythingUI.WPF.Controls;

namespace EverythingUI.Demo.Views;

public partial class ListViewPage : UserControl
{
    public ObservableCollection<FileItem> Files { get; } = new()
    {
        new("项目方案.docx", "文档", "248 KB", "2026-06-20 14:32"),
        new("财务报表.xlsx", "表格", "512 KB", "2026-06-18 09:15"),
        new("产品演示.pptx", "演示文稿", "4.2 MB", "2026-06-22 16:48"),
        new("源代码.zip", "压缩包", "18.6 MB", "2026-06-24 11:02"),
        new("界面设计稿.png", "图片", "1.8 MB", "2026-06-23 10:25"),
        new("需求说明.pdf", "文档", "892 KB", "2026-06-19 13:40"),
        new("会议纪要.txt", "文本", "12 KB", "2026-06-25 08:50"),
        new("安装包.exe", "应用程序", "62.4 MB", "2026-06-21 17:30"),
    };

    public ListViewPage()
    {
        InitializeComponent();
        DataContext = this;
    }
}

public sealed class FileItem
{
    public string Name { get; }
    public string Type { get; }
    public string Size { get; }
    public string Modified { get; }
    public FileItem(string name, string type, string size, string modified)
    {
        Name = name; Type = type; Size = size; Modified = modified;
    }
}
