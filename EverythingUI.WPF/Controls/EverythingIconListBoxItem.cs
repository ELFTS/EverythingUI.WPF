using System.Windows;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls
{
    /// <summary>
    /// 图标列表框项数据模型
    /// </summary>
    public class EverythingIconListBoxItem : DependencyObject
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
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(EverythingIconListBoxItem));

        /// <summary>
        /// 图标源
        /// </summary>
        public ImageSource? Icon
        {
            get => (ImageSource?)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(EverythingIconListBoxItem));

        /// <summary>
        /// 图标宽度
        /// </summary>
        public double IconWidth
        {
            get => (double)GetValue(IconWidthProperty);
            set => SetValue(IconWidthProperty, value);
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register(nameof(IconWidth), typeof(double), typeof(EverythingIconListBoxItem), new PropertyMetadata(28.0));

        /// <summary>
        /// 图标高度
        /// </summary>
        public double IconHeight
        {
            get => (double)GetValue(IconHeightProperty);
            set => SetValue(IconHeightProperty, value);
        }

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register(nameof(IconHeight), typeof(double), typeof(EverythingIconListBoxItem), new PropertyMetadata(28.0));

        /// <summary>
        /// 附加数据
        /// </summary>
        public object? Tag
        {
            get => GetValue(TagProperty);
            set => SetValue(TagProperty, value);
        }

        public static readonly DependencyProperty TagProperty =
            DependencyProperty.Register(nameof(Tag), typeof(object), typeof(EverythingIconListBoxItem));

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register(nameof(IsEnabled), typeof(bool), typeof(EverythingIconListBoxItem), new PropertyMetadata(true));

        public EverythingIconListBoxItem()
        {
        }

        public EverythingIconListBoxItem(string text)
        {
            Text = text;
        }

        public EverythingIconListBoxItem(string text, ImageSource? icon)
        {
            Text = text;
            Icon = icon;
        }

        public override string? ToString()
        {
            return Text ?? base.ToString();
        }
    }
}
