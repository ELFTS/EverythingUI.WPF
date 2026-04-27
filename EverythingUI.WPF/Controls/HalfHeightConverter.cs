using System;
using System.Globalization;
using System.Windows.Data;

namespace EverythingUI.WPF.Controls
{
    public class HalfHeightConverter : IValueConverter
    {
        public static readonly HalfHeightConverter Instance = new HalfHeightConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double height)
            {
                return height / 2.0;
            }
            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
