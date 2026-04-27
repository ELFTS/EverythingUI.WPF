using System.Globalization;
using System.Windows.Data;

namespace EverythingUI.WPF.Controls;

public class ProgressBarWidthConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 4)
            return 0.0;

        if (values[0] is double value &&
            values[1] is double minimum &&
            values[2] is double maximum &&
            values[3] is double actualWidth)
        {
            if (maximum > minimum && actualWidth > 0)
            {
                double percentage = (value - minimum) / (maximum - minimum);
                return actualWidth * percentage;
            }
        }

        return 0.0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
