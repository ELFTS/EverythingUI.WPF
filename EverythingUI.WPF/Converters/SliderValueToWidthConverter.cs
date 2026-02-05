using System.Globalization;
using System.Windows.Data;

namespace EverythingUI.WPF.Converters;

public class SliderValueToWidthConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 4)
            return 0;

        if (values[0] is double value && 
            values[1] is double minimum && 
            values[2] is double maximum &&
            values[3] is double actualWidth)
        {
            double range = maximum - minimum;
            if (range <= 0 || actualWidth <= 0)
                return 0;

            double percentage = (value - minimum) / range;
            return percentage * actualWidth;
        }

        return 0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}