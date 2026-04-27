using System.Globalization;
using System.Windows.Data;

namespace EverythingUI.WPF.Controls;

public class ArcPointConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 4)
            return new System.Windows.Point(50, 10);

        if (values[0] is double angle &&
            values[1] is double width &&
            values[2] is double height &&
            values[3] is double strokeThickness)
        {
            double radius = (Math.Min(width, height) - strokeThickness) / 2;
            double centerX = width / 2;
            double centerY = height / 2;

            // 将角度转换为弧度，从12点钟方向开始（减去90度）
            double radians = (angle - 90) * Math.PI / 180;

            double x = centerX + radius * Math.Cos(radians);
            double y = centerY + radius * Math.Sin(radians);

            return new System.Windows.Point(x, y);
        }

        return new System.Windows.Point(50, 10);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
