using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class ColorToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is Color color ? new SolidColorBrush(color) : new SolidColorBrush(Colors.Transparent);

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => value is SolidColorBrush brush ? brush.Color : Colors.Transparent;
}
