using System.Windows;

namespace EverythingUI.WPF.Controls
{
    /// <summary>
    /// 侧边栏菜单项数据模型
    /// </summary>
    public class EverythingSideBarItem : DependencyObject
    {
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(EverythingSideBarItem));

        /// <summary>
        /// 关联的数据对象
        /// </summary>
        public object? Tag
        {
            get => GetValue(TagProperty);
            set => SetValue(TagProperty, value);
        }

        public static readonly DependencyProperty TagProperty =
            DependencyProperty.Register(nameof(Tag), typeof(object), typeof(EverythingSideBarItem));

        public EverythingSideBarItem()
        {
        }

        public EverythingSideBarItem(string text)
        {
            Text = text;
        }

        public override string? ToString()
        {
            return Text ?? base.ToString();
        }
    }
}
