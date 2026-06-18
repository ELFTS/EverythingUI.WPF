using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingRadioButton : RadioButton
{
    static EverythingRadioButton() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingRadioButton), new FrameworkPropertyMetadata(typeof(EverythingRadioButton)));

    public static readonly DependencyProperty BoxSizeProperty =
        DependencyProperty.Register(nameof(BoxSize), typeof(double), typeof(EverythingRadioButton),
            new FrameworkPropertyMetadata(22.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty DotBrushProperty =
        DependencyProperty.Register(nameof(DotBrush), typeof(Brush), typeof(EverythingRadioButton),
            new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White), FrameworkPropertyMetadataOptions.AffectsRender));

    public double BoxSize { get => (double)GetValue(BoxSizeProperty); set => SetValue(BoxSizeProperty, value); }
    public Brush DotBrush { get => (Brush)GetValue(DotBrushProperty); set => SetValue(DotBrushProperty, value); }
}
