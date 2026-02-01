using System.Globalization;
using System.Windows.Data;

namespace EverythingUI.WPF.Converters;

public class BooleanToTextConverter : IValueConverter
{
    public string TrueText { get; set; } = "折叠";
    public string FalseText { get; set; } = "展开";

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? TrueText : FalseText;
        }
        return FalseText;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
