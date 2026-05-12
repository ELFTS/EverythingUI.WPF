using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingButton : Button
{
    static EverythingButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingButton), 
            new FrameworkPropertyMetadata(typeof(EverythingButton)));
    }

    public EverythingButton()
    {
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        UpdateColors();
    }

    private void UpdateColors()
    {
        SetCurrentValue(GradientStartColorProperty, ColorHelper.GetGradientStartColor(ColorName));
        SetCurrentValue(GradientEndColorProperty, ColorHelper.GetGradientEndColor(ColorName));
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingButton),
            new PropertyMetadata(new CornerRadius(6)));

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(EverythingButton),
            new PropertyMetadata(null));

    public static readonly DependencyProperty IconPlacementProperty =
        DependencyProperty.Register(nameof(IconPlacement), typeof(Dock), typeof(EverythingButton),
            new PropertyMetadata(Dock.Left));

    public static readonly DependencyProperty ColorNameProperty =
        DependencyProperty.Register(nameof(ColorName), typeof(ColorName), typeof(EverythingButton),
            new PropertyMetadata(ColorName.Blue, OnColorNameChanged));

    internal static readonly DependencyProperty GradientStartColorProperty =
        DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingButton),
            new PropertyMetadata(default(Color)));

    internal static readonly DependencyProperty GradientEndColorProperty =
        DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingButton),
            new PropertyMetadata(default(Color)));

    private static void OnColorNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingButton button)
        {
            button.UpdateColors();
        }
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public object Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public Dock IconPlacement
    {
        get => (Dock)GetValue(IconPlacementProperty);
        set => SetValue(IconPlacementProperty, value);
    }

    /// <summary>
    /// 颜色名称 - 直接使用颜色英文名
    /// </summary>
    public ColorName ColorName
    {
        get => (ColorName)GetValue(ColorNameProperty);
        set => SetValue(ColorNameProperty, value);
    }

    internal Color GradientStartColor
    {
        get => (Color)GetValue(GradientStartColorProperty);
        set => SetValue(GradientStartColorProperty, value);
    }

    internal Color GradientEndColor
    {
        get => (Color)GetValue(GradientEndColorProperty);
        set => SetValue(GradientEndColorProperty, value);
    }
}
