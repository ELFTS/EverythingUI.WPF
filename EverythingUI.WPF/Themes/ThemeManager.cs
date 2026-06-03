using System.Windows;
using System.Windows.Media;
using EverythingUI.WPF.Controls;

namespace EverythingUI.WPF.Themes;

/// <summary>
/// 主题管理器 - 管理全局颜色设置
/// </summary>
public static class ThemeManager
{
    /// <summary>
    /// 当前全局颜色名称
    /// </summary>
    public static ColorName CurrentColorName { get; private set; } = ColorName.Blue;

    /// <summary>
    /// 颜色变更事件
    /// </summary>
    public static event EventHandler<ColorName>? ColorChanged;

    /// <summary>
    /// 设置全局颜色
    /// </summary>
    public static void SetColor(ColorName colorName)
    {
        if (CurrentColorName != colorName)
        {
            CurrentColorName = colorName;
            UpdateGlobalResources(colorName);
            ColorChanged?.Invoke(null, colorName);
        }
    }

    /// <summary>
    /// 初始化主题管理器
    /// </summary>
    /// <param name="defaultColorName">默认颜色名称，默认值为 Blue</param>
    public static void Initialize(ColorName defaultColorName = ColorName.Blue)
    {
        CurrentColorName = defaultColorName;
        EnsureGlobalResourcesExist();
        UpdateGlobalResources(defaultColorName);
    }

    /// <summary>
    /// 确保全局资源存在
    /// </summary>
    private static void EnsureGlobalResourcesExist()
    {
        if (Application.Current == null) return;

        var resources = Application.Current.Resources;

        if (!resources.Contains("GlobalGradientStartColor"))
            resources["GlobalGradientStartColor"] = Color.FromRgb(0, 172, 240);
        if (!resources.Contains("GlobalGradientEndColor"))
            resources["GlobalGradientEndColor"] = Color.FromRgb(0, 120, 212);
        if (!resources.Contains("GlobalTrackColor"))
            resources["GlobalTrackColor"] = Color.FromRgb(224, 224, 224);

        if (!resources.Contains("GlobalGradientStartBrush"))
            resources["GlobalGradientStartBrush"] = new SolidColorBrush(Color.FromRgb(0, 172, 240));
        if (!resources.Contains("GlobalGradientEndBrush"))
            resources["GlobalGradientEndBrush"] = new SolidColorBrush(Color.FromRgb(0, 120, 212));
        if (!resources.Contains("GlobalTrackBrush"))
            resources["GlobalTrackBrush"] = new SolidColorBrush(Color.FromRgb(224, 224, 224));
    }

    /// <summary>
    /// 更新全局资源颜色
    /// </summary>
    private static void UpdateGlobalResources(ColorName colorName)
    {
        var (start, end) = colorName.GetGradientColors();
        var trackColor = ColorHelper.GetTrackColor(colorName);

        if (Application.Current == null) return;

        var resources = Application.Current.Resources;
        resources["GlobalColorName"] = colorName;

        resources["GlobalGradientStartColor"] = new Color { A = start.A, R = start.R, G = start.G, B = start.B };
        resources["GlobalGradientEndColor"] = new Color { A = end.A, R = end.R, G = end.G, B = end.B };
        resources["GlobalTrackColor"] = new Color { A = trackColor.A, R = trackColor.R, G = trackColor.G, B = trackColor.B };

        SolidColorBrush[] brushes = [new(start), new(end), new(trackColor)];
        foreach (var brush in brushes)
            brush.Freeze();

        resources["GlobalGradientStartBrush"] = brushes[0];
        resources["GlobalGradientEndBrush"] = brushes[1];
        resources["GlobalTrackBrush"] = brushes[2];
    }

    /// <summary>
    /// 获取当前颜色的渐变起始色
    /// </summary>
    public static Color GetCurrentGradientStartColor()
    {
        var (start, _) = CurrentColorName.GetGradientColors();
        return start;
    }

    /// <summary>
    /// 获取当前颜色的渐变结束色
    /// </summary>
    public static Color GetCurrentGradientEndColor()
    {
        var (_, end) = CurrentColorName.GetGradientColors();
        return end;
    }

    /// <summary>
    /// 获取当前颜色的轨道色
    /// </summary>
    public static Color GetCurrentTrackColor() => ColorHelper.GetTrackColor(CurrentColorName);
}
