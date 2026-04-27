using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingTextBox : TextBox
{
    static EverythingTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingTextBox),
            new FrameworkPropertyMetadata(typeof(EverythingTextBox)));
    }

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(EverythingTextBox),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty PlaceholderBrushProperty =
        DependencyProperty.Register(nameof(PlaceholderBrush), typeof(Brush), typeof(EverythingTextBox),
            new PropertyMetadata(Brushes.Gray));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingTextBox),
            new PropertyMetadata(new CornerRadius(6)));

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public Brush PlaceholderBrush
    {
        get => (Brush)GetValue(PlaceholderBrushProperty);
        set => SetValue(PlaceholderBrushProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}
