using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingSlider : Slider
{
    static EverythingSlider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingSlider),
            new FrameworkPropertyMetadata(typeof(EverythingSlider)));
    }

    public EverythingSlider()
    {
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // 从资源字典加载默认颜色
        if (GradientStartColor == default)
        {
            SetCurrentValue(GradientStartColorProperty, (Color)FindResource("GradientBlueStart"));
        }
        if (GradientEndColor == default)
        {
            SetCurrentValue(GradientEndColorProperty, (Color)FindResource("GradientBlueEnd"));
        }
        if (TrackBackgroundColor == default)
        {
            SetCurrentValue(TrackBackgroundColorProperty, (Color)ColorConverter.ConvertFromString("#C8C8C8"));
        }
    }

    /// <summary>
    /// 渐变起始颜色（顶部和底部）
    /// </summary>
    public Color GradientStartColor
    {
        get => (Color)GetValue(GradientStartColorProperty);
        set => SetValue(GradientStartColorProperty, value);
    }

    public static readonly DependencyProperty GradientStartColorProperty =
        DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingSlider),
            new PropertyMetadata(default(Color)));

    /// <summary>
    /// 渐变中间颜色
    /// </summary>
    public Color GradientEndColor
    {
        get => (Color)GetValue(GradientEndColorProperty);
        set => SetValue(GradientEndColorProperty, value);
    }

    public static readonly DependencyProperty GradientEndColorProperty =
        DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingSlider),
            new PropertyMetadata(default(Color)));

    /// <summary>
    /// 轨道背景色
    /// </summary>
    public Color TrackBackgroundColor
    {
        get => (Color)GetValue(TrackBackgroundColorProperty);
        set => SetValue(TrackBackgroundColorProperty, value);
    }

    public static readonly DependencyProperty TrackBackgroundColorProperty =
        DependencyProperty.Register(nameof(TrackBackgroundColor), typeof(Color), typeof(EverythingSlider),
            new PropertyMetadata(default(Color)));
}
