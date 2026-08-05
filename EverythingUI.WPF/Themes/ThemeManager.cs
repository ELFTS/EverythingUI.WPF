using System.Windows;
using System.Windows.Media;
using EverythingUI.WPF.Controls;

namespace EverythingUI.WPF.Themes;

public static class ThemeManager
{
    public static ColorName CurrentColorName { get; private set; } = ColorHelper.DefaultColorName;
    public static event EventHandler<ColorName>? ColorChanged;

    public static void ChangeColor(ColorName colorName)
    {
        if (CurrentColorName == colorName) return;
        CurrentColorName = colorName;
        UpdateGlobalResources(colorName);
        ColorChanged?.Invoke(null, colorName);
    }

    public static void Initialize(ColorName defaultColorName = ColorHelper.DefaultColorName)
    {
        CurrentColorName = defaultColorName;
        UpdateGlobalResources(defaultColorName);
    }

    private static void UpdateGlobalResources(ColorName colorName)
    {
        if (Application.Current == null) return;
        var (start, end) = colorName.GetGradientColors();
        var trackColor = ColorHelper.GetTrackColor(colorName);
        var trackBrush = new SolidColorBrush(trackColor);
        trackBrush.Freeze();
        var r = Application.Current.Resources;
        r["GlobalColorName"] = colorName;
        r["GlobalGradientStartColor"] = start;
        r["GlobalGradientEndColor"] = end;
        r["GlobalTrackColor"] = trackColor;
        r["GlobalTrackBrush"] = trackBrush;
    }

    public static Color GetCurrentGradientStartColor() => CurrentColorName.GetGradientColors().start;
    public static Color GetCurrentGradientEndColor() => CurrentColorName.GetGradientColors().end;
    public static Color GetCurrentTrackColor() => ColorHelper.GetTrackColor(CurrentColorName);
}
