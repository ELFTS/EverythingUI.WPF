using System.Windows;
using System.Windows.Controls;

namespace EverythingUI.WPF.Controls;

public class EverythingOverlayDialog : ContentControl
{
    static EverythingOverlayDialog() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(typeof(EverythingOverlayDialog)));

    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty BlurRadiusProperty =
        DependencyProperty.Register(nameof(BlurRadius), typeof(double), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(18.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty DialogWidthProperty =
        DependencyProperty.Register(nameof(DialogWidth), typeof(double), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static readonly DependencyProperty DialogMaxWidthProperty =
        DependencyProperty.Register(nameof(DialogMaxWidth), typeof(double), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(520.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static readonly DependencyProperty DialogMaxHeightProperty =
        DependencyProperty.Register(nameof(DialogMaxHeight), typeof(double), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(720.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static readonly DependencyProperty DialogPaddingProperty =
        DependencyProperty.Register(nameof(DialogPadding), typeof(Thickness), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(new Thickness(24), FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static readonly DependencyProperty DialogCornerRadiusProperty =
        DependencyProperty.Register(nameof(DialogCornerRadius), typeof(CornerRadius), typeof(EverythingOverlayDialog),
            new FrameworkPropertyMetadata(new CornerRadius(16), FrameworkPropertyMetadataOptions.AffectsRender));

    public bool IsOpen { get => (bool)GetValue(IsOpenProperty); set => SetValue(IsOpenProperty, value); }
    public double BlurRadius { get => (double)GetValue(BlurRadiusProperty); set => SetValue(BlurRadiusProperty, value); }
    public double DialogWidth { get => (double)GetValue(DialogWidthProperty); set => SetValue(DialogWidthProperty, value); }
    public double DialogMaxWidth { get => (double)GetValue(DialogMaxWidthProperty); set => SetValue(DialogMaxWidthProperty, value); }
    public double DialogMaxHeight { get => (double)GetValue(DialogMaxHeightProperty); set => SetValue(DialogMaxHeightProperty, value); }
    public Thickness DialogPadding { get => (Thickness)GetValue(DialogPaddingProperty); set => SetValue(DialogPaddingProperty, value); }
    public CornerRadius DialogCornerRadius { get => (CornerRadius)GetValue(DialogCornerRadiusProperty); set => SetValue(DialogCornerRadiusProperty, value); }
}
