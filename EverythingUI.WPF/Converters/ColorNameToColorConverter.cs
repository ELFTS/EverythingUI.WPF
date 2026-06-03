using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using EverythingUI.WPF.Controls;

namespace EverythingUI.WPF.Converters;

/// <summary>
/// ColorName 转换为渐变起始色的转换器
/// </summary>
public class ColorNameToStartColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ColorName colorName)
        {
            var (start, _) = colorName.GetGradientColors();
            return start;
        }
        return Color.FromRgb(0, 172, 240);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

/// <summary>
/// ColorName 转换为渐变结束色的转换器
/// </summary>
public class ColorNameToEndColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ColorName colorName)
        {
            var (_, end) = colorName.GetGradientColors();
            return end;
        }
        return Color.FromRgb(0, 120, 212);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

/// <summary>
/// ColorName 转换为轨道颜色的转换器
/// </summary>
public class ColorNameToTrackColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ColorName colorName)
            return ColorHelper.GetTrackColor(colorName);
        return Color.FromRgb(224, 224, 224);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
