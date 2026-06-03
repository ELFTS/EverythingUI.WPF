using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingCheckBox : CheckBox
{
    static EverythingCheckBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingCheckBox), new FrameworkPropertyMetadata(typeof(EverythingCheckBox)));
    }

    public static readonly DependencyProperty BoxSizeProperty =
        DependencyProperty.Register(nameof(BoxSize), typeof(double), typeof(EverythingCheckBox), new PropertyMetadata(22.0));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingCheckBox), new PropertyMetadata(new CornerRadius(6)));

    public static readonly DependencyProperty CheckMarkBrushProperty =
        DependencyProperty.Register(nameof(CheckMarkBrush), typeof(Brush), typeof(EverythingCheckBox),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));

    public double BoxSize
    {
        get => (double)GetValue(BoxSizeProperty);
        set => SetValue(BoxSizeProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public Brush CheckMarkBrush
    {
        get => (Brush)GetValue(CheckMarkBrushProperty);
        set => SetValue(CheckMarkBrushProperty, value);
    }
}
