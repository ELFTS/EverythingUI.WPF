using System.Globalization;
using System.Windows.Data;

namespace EverythingUI.WPF.Converters;

public class SliderValueToPercentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double val)
        {
            // 假设 Value 范围是 0-100，转换为 0-1
            return val / 100.0;
        }
        return 0.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
