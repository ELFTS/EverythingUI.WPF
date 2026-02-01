using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EverythingUI.WPF.Converters;

public class IconPlaceholderMarginConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Length >= 2 && values[0] is Thickness padding)
        {
            bool hasIcon = values[1] != null;
            if (hasIcon)
            {
                return new Thickness(padding.Left + 28, padding.Top, padding.Right, padding.Bottom);
            }
            return padding;
        }
        return new Thickness(12, 8, 12, 8);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
