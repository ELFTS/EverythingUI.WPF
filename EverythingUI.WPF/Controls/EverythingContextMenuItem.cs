using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingContextMenuItem : DependencyObject
{
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(EverythingContextMenuItem));

    public ImageSource? Icon { get => (ImageSource?)GetValue(IconProperty); set => SetValue(IconProperty, value); }
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(EverythingContextMenuItem));

    public object? Tag { get => GetValue(TagProperty); set => SetValue(TagProperty, value); }
    public static readonly DependencyProperty TagProperty =
        DependencyProperty.Register(nameof(Tag), typeof(object), typeof(EverythingContextMenuItem));

    public bool IsEnabled { get => (bool)GetValue(IsEnabledProperty); set => SetValue(IsEnabledProperty, value); }
    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.Register(nameof(IsEnabled), typeof(bool), typeof(EverythingContextMenuItem), new PropertyMetadata(true));

    public bool IsSeparator { get => (bool)GetValue(IsSeparatorProperty); set => SetValue(IsSeparatorProperty, value); }
    public static readonly DependencyProperty IsSeparatorProperty =
        DependencyProperty.Register(nameof(IsSeparator), typeof(bool), typeof(EverythingContextMenuItem), new PropertyMetadata(false));

    public ICommand? Command { get => (ICommand?)GetValue(CommandProperty); set => SetValue(CommandProperty, value); }
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(EverythingContextMenuItem));

    public object? CommandParameter { get => GetValue(CommandParameterProperty); set => SetValue(CommandParameterProperty, value); }
    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(EverythingContextMenuItem));

    public string? InputGestureText { get => (string?)GetValue(InputGestureTextProperty); set => SetValue(InputGestureTextProperty, value); }
    public static readonly DependencyProperty InputGestureTextProperty =
        DependencyProperty.Register(nameof(InputGestureText), typeof(string), typeof(EverythingContextMenuItem));

    public EverythingContextMenuItem() { }
    public EverythingContextMenuItem(string text) => Text = text;
    public EverythingContextMenuItem(string text, ImageSource? icon) { Text = text; Icon = icon; }
    public override string? ToString() => Text ?? base.ToString();
}
