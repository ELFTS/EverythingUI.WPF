using System.Globalization;
using System.Windows.Data;

namespace EverythingUI.WPF.Controls;

public class ProgressToAngleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double progress)
        {
            // 如果是检查 IsLargeArc
            if (parameter is string str && str == "IsLarge")
            {
                double angle = progress * 3.6; // progress * 360 / 100
                return angle > 180;
            }
            
            // 返回角度值
            double result = progress * 3.6; // progress * 360 / 100
            // 当接近100%时，使用稍微小于360度的角度
            if (result >= 359.99)
            {
                result = 359.99;
            }
            return result;
        }
        return 0.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
