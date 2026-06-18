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
        if (CurrentColorName != colorName)
        {
            CurrentColorName = colorName;
            UpdateGlobalResources(colorName);
            ColorChanged?.Invoke(null, colorName);
        }
    }

    public static void Initialize(ColorName defaultColorName = ColorHelper.DefaultColorName)
    {
        CurrentColorName = defaultColorName;
        EnsureGlobalResourcesExist();
        UpdateGlobalResources(defaultColorName);
    }

    private static void EnsureGlobalResourcesExist()
    {
        if (Application.Current == null) return;
        var r = Application.Current.Resources;
        if (!r.Contains("GlobalGradientStartColor")) r["GlobalGradientStartColor"] = ColorHelper.DefaultGradientStartColor;
        if (!r.Contains("GlobalGradientEndColor")) r["GlobalGradientEndColor"] = ColorHelper.DefaultGradientEndColor;
        if (!r.Contains("GlobalTrackColor")) r["GlobalTrackColor"] = ColorHelper.DefaultTrackColor;
        if (!r.Contains("GlobalGradientStartBrush")) r["GlobalGradientStartBrush"] = new SolidColorBrush(ColorHelper.DefaultGradientStartColor);
        if (!r.Contains("GlobalGradientEndBrush")) r["GlobalGradientEndBrush"] = new SolidColorBrush(ColorHelper.DefaultGradientEndColor);
        if (!r.Contains("GlobalTrackBrush")) r["GlobalTrackBrush"] = new SolidColorBrush(ColorHelper.DefaultTrackColor);
    }

    private static void UpdateGlobalResources(ColorName colorName)
    {
        if (Application.Current == null) return;
        var (start, end) = colorName.GetGradientColors();
        var trackColor = ColorHelper.GetTrackColor(colorName);
        var r = Application.Current.Resources;
        r["GlobalColorName"] = colorName;
        r["GlobalGradientStartColor"] = Clone(start);
        r["GlobalGradientEndColor"] = Clone(end);
        r["GlobalTrackColor"] = Clone(trackColor);

        SolidColorBrush[] brushes = [new(start), new(end), new(trackColor)];
        foreach (var b in brushes) b.Freeze();
        r["GlobalGradientStartBrush"] = brushes[0];
        r["GlobalGradientEndBrush"] = brushes[1];
        r["GlobalTrackBrush"] = brushes[2];
    }

    private static Color Clone(Color c) => new() { A = c.A, R = c.R, G = c.G, B = c.B };

    public static Color GetCurrentGradientStartColor() => CurrentColorName.GetGradientColors().start;
    public static Color GetCurrentGradientEndColor() => CurrentColorName.GetGradientColors().end;
    public static Color GetCurrentTrackColor() => ColorHelper.GetTrackColor(CurrentColorName);
}
