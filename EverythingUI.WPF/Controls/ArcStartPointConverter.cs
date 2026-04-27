using System.Globalization;
using System.Windows.Data;

namespace EverythingUI.WPF.Controls;

public class ArcStartPointConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 3)
            return new System.Windows.Point(50, 10);

        if (values[0] is double width &&
            values[1] is double height &&
            values[2] is double strokeThickness)
        {
            double radius = (Math.Min(width, height) - strokeThickness) / 2;
            double centerX = width / 2;
            double centerY = height / 2;

            // 起始点在12点钟方向（正上方）
            double x = centerX;
            double y = centerY - radius;

            return new System.Windows.Point(x, y);
        }

        return new System.Windows.Point(50, 10);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
