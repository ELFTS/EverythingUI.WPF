using System.Globalization;
using System.Windows.Data;

namespace EverythingUI.WPF.Controls;

public class ArcIsLargeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double angle)
        {
            // 当角度大于180度时，IsLargeArc为true
            return angle > 180;
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
