using System.Windows;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public static class ColorManager
{
    public static readonly DependencyProperty ColorNameProperty =
        DependencyProperty.RegisterAttached("ColorName", typeof(ColorName), typeof(DependencyObject),
            new PropertyMetadata(ColorHelper.DefaultColorName, OnColorNameChanged));

    internal static readonly DependencyProperty GradientStartColorProperty =
        DependencyProperty.RegisterAttached("GradientStartColor", typeof(Color), typeof(DependencyObject),
            new PropertyMetadata(default));

    internal static readonly DependencyProperty GradientEndColorProperty =
        DependencyProperty.RegisterAttached("GradientEndColor", typeof(Color), typeof(DependencyObject),
            new PropertyMetadata(default));

    public static ColorName GetColorName(DependencyObject obj) => (ColorName)obj.GetValue(ColorNameProperty);

    public static void SetColorName(DependencyObject obj, ColorName value) => obj.SetValue(ColorNameProperty, value);

    internal static Color GetGradientStartColor(DependencyObject obj) => (Color)obj.GetValue(GradientStartColorProperty);

    internal static void SetGradientStartColor(DependencyObject obj, Color value) => obj.SetValue(GradientStartColorProperty, value);

    internal static Color GetGradientEndColor(DependencyObject obj) => (Color)obj.GetValue(GradientEndColorProperty);

    internal static void SetGradientEndColor(DependencyObject obj, Color value) => obj.SetValue(GradientEndColorProperty, value);

    public static void UpdateColors(DependencyObject control)
    {
        var colorName = GetColorName(control);
        control.SetCurrentValue(GradientStartColorProperty, ColorHelper.GetGradientStartColor(colorName));
        control.SetCurrentValue(GradientEndColorProperty, ColorHelper.GetGradientEndColor(colorName));
    }

    private static void OnColorNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => UpdateColors(d);
}
