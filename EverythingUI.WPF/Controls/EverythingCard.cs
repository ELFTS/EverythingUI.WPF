using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public enum CardVariant { Default, Elevated, Outlined }

public class EverythingCard : ContentControl
{
    static EverythingCard() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingCard),
            new FrameworkPropertyMetadata(typeof(EverythingCard)));

    public static readonly DependencyProperty FooterProperty =
        DependencyProperty.Register(nameof(Footer), typeof(object), typeof(EverythingCard),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty FooterTemplateProperty =
        DependencyProperty.Register(nameof(FooterTemplate), typeof(DataTemplate), typeof(EverythingCard),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty CardVariantProperty =
        DependencyProperty.Register(nameof(CardVariant), typeof(CardVariant), typeof(EverythingCard),
            new FrameworkPropertyMetadata(CardVariant.Default, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingCard),
            new FrameworkPropertyMetadata(new CornerRadius(8), FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty ShadowDepthProperty =
        DependencyProperty.Register(nameof(ShadowDepth), typeof(double), typeof(EverythingCard),
            new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty FooterPaddingProperty =
        DependencyProperty.Register(nameof(FooterPadding), typeof(Thickness), typeof(EverythingCard),
            new FrameworkPropertyMetadata(new Thickness(16, 0, 16, 16), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

    public object Footer { get => GetValue(FooterProperty); set => SetValue(FooterProperty, value); }
    public DataTemplate FooterTemplate { get => (DataTemplate)GetValue(FooterTemplateProperty); set => SetValue(FooterTemplateProperty, value); }
    public CardVariant CardVariant { get => (CardVariant)GetValue(CardVariantProperty); set => SetValue(CardVariantProperty, value); }
    public CornerRadius CornerRadius { get => (CornerRadius)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }
    public double ShadowDepth { get => (double)GetValue(ShadowDepthProperty); set => SetValue(ShadowDepthProperty, value); }
    public Thickness FooterPadding { get => (Thickness)GetValue(FooterPaddingProperty); set => SetValue(FooterPaddingProperty, value); }
}
