using System.Windows;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls
{
    public class EverythingToolBarItem : DependencyObject
    {
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(EverythingToolBarItem));

        public ImageSource? Icon
        {
            get => (ImageSource?)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(EverythingToolBarItem));

        public double IconWidth
        {
            get => (double)GetValue(IconWidthProperty);
            set => SetValue(IconWidthProperty, value);
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register(nameof(IconWidth), typeof(double), typeof(EverythingToolBarItem), new PropertyMetadata(18.0));

        public double IconHeight
        {
            get => (double)GetValue(IconHeightProperty);
            set => SetValue(IconHeightProperty, value);
        }

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register(nameof(IconHeight), typeof(double), typeof(EverythingToolBarItem), new PropertyMetadata(18.0));

        public object? Tag
        {
            get => GetValue(TagProperty);
            set => SetValue(TagProperty, value);
        }

        public static readonly DependencyProperty TagProperty =
            DependencyProperty.Register(nameof(Tag), typeof(object), typeof(EverythingToolBarItem));

        public EverythingToolBarItem()
        {
        }

        public EverythingToolBarItem(string text)
        {
            Text = text;
        }

        public EverythingToolBarItem(string text, ImageSource? icon)
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
