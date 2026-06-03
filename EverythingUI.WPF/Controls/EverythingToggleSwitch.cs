using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingToggleSwitch : ToggleButton
{
    static EverythingToggleSwitch()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingToggleSwitch), new FrameworkPropertyMetadata(typeof(EverythingToggleSwitch)));
    }

    public static readonly DependencyProperty SwitchWidthProperty =
        DependencyProperty.Register(nameof(SwitchWidth), typeof(double), typeof(EverythingToggleSwitch), new PropertyMetadata(50.0));

    public static readonly DependencyProperty SwitchHeightProperty =
        DependencyProperty.Register(nameof(SwitchHeight), typeof(double), typeof(EverythingToggleSwitch), new PropertyMetadata(26.0));

    public static readonly DependencyProperty ThumbSizeProperty =
        DependencyProperty.Register(nameof(ThumbSize), typeof(double), typeof(EverythingToggleSwitch), new PropertyMetadata(22.0));

    public static readonly DependencyProperty UncheckedBackgroundProperty =
        DependencyProperty.Register(nameof(UncheckedBackground), typeof(Brush), typeof(EverythingToggleSwitch),
            new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCCCC"))));

    public static readonly DependencyProperty ThumbBrushProperty =
        DependencyProperty.Register(nameof(ThumbBrush), typeof(Brush), typeof(EverythingToggleSwitch),
            new PropertyMetadata(Brushes.White));

    public double SwitchWidth
    {
        get => (double)GetValue(SwitchWidthProperty);
        set => SetValue(SwitchWidthProperty, value);
    }

    public double SwitchHeight
    {
        get => (double)GetValue(SwitchHeightProperty);
        set => SetValue(SwitchHeightProperty, value);
    }

    public double ThumbSize
    {
        get => (double)GetValue(ThumbSizeProperty);
        set => SetValue(ThumbSizeProperty, value);
    }

    public Brush UncheckedBackground
    {
        get => (Brush)GetValue(UncheckedBackgroundProperty);
        set => SetValue(UncheckedBackgroundProperty, value);
    }

    public Brush ThumbBrush
    {
        get => (Brush)GetValue(ThumbBrushProperty);
        set => SetValue(ThumbBrushProperty, value);
    }
}
