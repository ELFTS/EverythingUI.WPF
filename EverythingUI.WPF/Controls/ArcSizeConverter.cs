using System.Globalization;
using System.Windows.Data;

namespace EverythingUI.WPF.Controls;

public class ArcSizeConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 3)
            return new System.Windows.Size(40, 40);

        if (values[0] is double width &&
            values[1] is double height &&
            values[2] is double strokeThickness)
        {
            double radius = (Math.Min(width, height) - strokeThickness) / 2;
            return new System.Windows.Size(radius, radius);
        }

        return new System.Windows.Size(40, 40);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
