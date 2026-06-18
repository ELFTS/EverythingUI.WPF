using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingIconListBoxItem : DependencyObject
{
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(EverythingIconListBoxItem));

    public ImageSource? Icon { get => (ImageSource?)GetValue(IconProperty); set => SetValue(IconProperty, value); }
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(EverythingIconListBoxItem));

    public double IconWidth { get => (double)GetValue(IconWidthProperty); set => SetValue(IconWidthProperty, value); }
    public static readonly DependencyProperty IconWidthProperty =
        DependencyProperty.Register(nameof(IconWidth), typeof(double), typeof(EverythingIconListBoxItem), new PropertyMetadata(28.0));

    public double IconHeight { get => (double)GetValue(IconHeightProperty); set => SetValue(IconHeightProperty, value); }
    public static readonly DependencyProperty IconHeightProperty =
        DependencyProperty.Register(nameof(IconHeight), typeof(double), typeof(EverythingIconListBoxItem), new PropertyMetadata(28.0));

    public object? Tag { get => GetValue(TagProperty); set => SetValue(TagProperty, value); }
    public static readonly DependencyProperty TagProperty =
        DependencyProperty.Register(nameof(Tag), typeof(object), typeof(EverythingIconListBoxItem));

    public bool IsEnabled { get => (bool)GetValue(IsEnabledProperty); set => SetValue(IsEnabledProperty, value); }
    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.Register(nameof(IsEnabled), typeof(bool), typeof(EverythingIconListBoxItem), new PropertyMetadata(true));

    public EverythingIconListBoxItem() { }
    public EverythingIconListBoxItem(string text) => Text = text;
    public EverythingIconListBoxItem(string text, ImageSource? icon) { Text = text; Icon = icon; }
    public override string? ToString() => Text ?? base.ToString();
}
