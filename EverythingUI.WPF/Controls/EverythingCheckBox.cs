using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingCheckBox : CheckBox
{
    static EverythingCheckBox() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingCheckBox), new FrameworkPropertyMetadata(typeof(EverythingCheckBox)));

    public static readonly DependencyProperty BoxSizeProperty =
        DependencyProperty.Register(nameof(BoxSize), typeof(double), typeof(EverythingCheckBox),
            new FrameworkPropertyMetadata(22.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingCheckBox),
            new FrameworkPropertyMetadata(new CornerRadius(6), FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty CheckMarkBrushProperty =
        DependencyProperty.Register(nameof(CheckMarkBrush), typeof(Brush), typeof(EverythingCheckBox),
            new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White), FrameworkPropertyMetadataOptions.AffectsRender));

    public double BoxSize { get => (double)GetValue(BoxSizeProperty); set => SetValue(BoxSizeProperty, value); }
    public CornerRadius CornerRadius { get => (CornerRadius)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }
    public Brush CheckMarkBrush { get => (Brush)GetValue(CheckMarkBrushProperty); set => SetValue(CheckMarkBrushProperty, value); }
}
