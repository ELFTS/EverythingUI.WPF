using System.Windows;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingListViewItem : DependencyObject
{
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(EverythingListViewItem));

    public ImageSource? Icon { get => (ImageSource?)GetValue(IconProperty); set => SetValue(IconProperty, value); }
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(EverythingListViewItem));

    public double IconSize { get => (double)GetValue(IconSizeProperty); set => SetValue(IconSizeProperty, value); }
    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(EverythingListViewItem), new PropertyMetadata(20.0));

    public object? Tag { get => GetValue(TagProperty); set => SetValue(TagProperty, value); }
    public static readonly DependencyProperty TagProperty =
        DependencyProperty.Register(nameof(Tag), typeof(object), typeof(EverythingListViewItem));

    public bool IsEnabled { get => (bool)GetValue(IsEnabledProperty); set => SetValue(IsEnabledProperty, value); }
    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.Register(nameof(IsEnabled), typeof(bool), typeof(EverythingListViewItem), new PropertyMetadata(true));

    public EverythingListViewItem() { }
    public EverythingListViewItem(string text) => Text = text;
    public EverythingListViewItem(string text, ImageSource? icon) { Text = text; Icon = icon; }
    public override string? ToString() => Text ?? base.ToString();
}
