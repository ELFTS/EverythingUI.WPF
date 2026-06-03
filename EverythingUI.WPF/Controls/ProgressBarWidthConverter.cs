using System.Globalization;
using System.Windows.Data;

namespace EverythingUI.WPF.Controls;

public class ProgressBarWidthConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 4 || values is not [double value, double minimum, double maximum, double actualWidth])
            return 0.0;

        if (maximum > minimum && actualWidth > 0)
            return actualWidth * (value - minimum) / (maximum - minimum);

        return 0.0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
