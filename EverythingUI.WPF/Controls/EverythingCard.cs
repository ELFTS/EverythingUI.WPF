using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public enum CardVariant
{
    Default,
    Elevated,
    Outlined
}

public class EverythingCard : ContentControl
{
    static EverythingCard()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingCard),
            new FrameworkPropertyMetadata(typeof(EverythingCard)));
    }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(object), typeof(EverythingCard),
            new PropertyMetadata(null));

    public static readonly DependencyProperty HeaderTemplateProperty =
        DependencyProperty.Register(nameof(HeaderTemplate), typeof(DataTemplate), typeof(EverythingCard),
            new PropertyMetadata(null));

    public static readonly DependencyProperty FooterProperty =
        DependencyProperty.Register(nameof(Footer), typeof(object), typeof(EverythingCard),
            new PropertyMetadata(null));

    public static readonly DependencyProperty FooterTemplateProperty =
        DependencyProperty.Register(nameof(FooterTemplate), typeof(DataTemplate), typeof(EverythingCard),
            new PropertyMetadata(null));

    public static readonly DependencyProperty CardVariantProperty =
        DependencyProperty.Register(nameof(CardVariant), typeof(CardVariant), typeof(EverythingCard),
            new PropertyMetadata(CardVariant.Default));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingCard),
            new PropertyMetadata(new CornerRadius(8)));

    public static readonly DependencyProperty ShadowDepthProperty =
        DependencyProperty.Register(nameof(ShadowDepth), typeof(double), typeof(EverythingCard),
            new PropertyMetadata(4.0));

    public static readonly DependencyProperty HeaderPaddingProperty =
        DependencyProperty.Register(nameof(HeaderPadding), typeof(Thickness), typeof(EverythingCard),
            new PropertyMetadata(new Thickness(16, 16, 16, 0)));

    public static readonly DependencyProperty FooterPaddingProperty =
        DependencyProperty.Register(nameof(FooterPadding), typeof(Thickness), typeof(EverythingCard),
            new PropertyMetadata(new Thickness(16, 0, 16, 16)));

    public object Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public DataTemplate HeaderTemplate
    {
        get => (DataTemplate)GetValue(HeaderTemplateProperty);
        set => SetValue(HeaderTemplateProperty, value);
    }

    public object Footer
    {
        get => GetValue(FooterProperty);
        set => SetValue(FooterProperty, value);
    }

    public DataTemplate FooterTemplate
    {
        get => (DataTemplate)GetValue(FooterTemplateProperty);
        set => SetValue(FooterTemplateProperty, value);
    }

    public CardVariant CardVariant
    {
        get => (CardVariant)GetValue(CardVariantProperty);
        set => SetValue(CardVariantProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public double ShadowDepth
    {
        get => (double)GetValue(ShadowDepthProperty);
        set => SetValue(ShadowDepthProperty, value);
    }

    public Thickness HeaderPadding
    {
        get => (Thickness)GetValue(HeaderPaddingProperty);
        set => SetValue(HeaderPaddingProperty, value);
    }

    public Thickness FooterPadding
    {
        get => (Thickness)GetValue(FooterPaddingProperty);
        set => SetValue(FooterPaddingProperty, value);
    }
}
