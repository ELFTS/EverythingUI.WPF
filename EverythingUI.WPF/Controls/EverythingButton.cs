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

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingButton),
            new PropertyMetadata(new CornerRadius(6)));

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(EverythingButton),
            new PropertyMetadata(null));

    public static readonly DependencyProperty IconPlacementProperty =
        DependencyProperty.Register(nameof(IconPlacement), typeof(Dock), typeof(EverythingButton),
            new PropertyMetadata(Dock.Left));

    public static readonly DependencyProperty GradientStartColorProperty =
        DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingButton),
            new PropertyMetadata(Color.FromRgb(0, 172, 240)));

    public static readonly DependencyProperty GradientEndColorProperty =
        DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingButton),
            new PropertyMetadata(Color.FromRgb(0, 120, 212)));

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

    public Color GradientStartColor
    {
        get => (Color)GetValue(GradientStartColorProperty);
        set => SetValue(GradientStartColorProperty, value);
    }

    public Color GradientEndColor
    {
        get => (Color)GetValue(GradientEndColorProperty);
        set => SetValue(GradientEndColorProperty, value);
    }
}
