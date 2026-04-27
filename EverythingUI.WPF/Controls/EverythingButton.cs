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
        // 从资源字典加载默认颜色
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // 如果未设置颜色，则使用资源字典中的默认颜色
        if (GradientStartColor == default)
        {
            SetCurrentValue(GradientStartColorProperty, (Color)FindResource("GradientBlueStart"));
        }
        if (GradientEndColor == default)
        {
            SetCurrentValue(GradientEndColorProperty, (Color)FindResource("GradientBlueEnd"));
        }
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
            new PropertyMetadata(default(Color)));

    public static readonly DependencyProperty GradientEndColorProperty =
        DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingButton),
            new PropertyMetadata(default(Color)));

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
