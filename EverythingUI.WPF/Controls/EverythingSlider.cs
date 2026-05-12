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
        UpdateColors();
    }

    private void UpdateColors()
    {
        SetCurrentValue(GradientStartColorProperty, ColorHelper.GetGradientStartColor(ColorName));
        SetCurrentValue(GradientEndColorProperty, ColorHelper.GetGradientEndColor(ColorName));
        SetCurrentValue(TrackBackgroundColorProperty, ColorHelper.GetTrackColor(ColorName));
    }

    /// <summary>
    /// 颜色名称 - 直接使用颜色英文名
    /// </summary>
    public ColorName ColorName
    {
        get => (ColorName)GetValue(ColorNameProperty);
        set => SetValue(ColorNameProperty, value);
    }

    public static readonly DependencyProperty ColorNameProperty =
        DependencyProperty.Register(nameof(ColorName), typeof(ColorName), typeof(EverythingSlider),
            new PropertyMetadata(ColorName.Blue, OnColorNameChanged));

    internal static readonly DependencyProperty GradientStartColorProperty =
        DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingSlider),
            new PropertyMetadata(default(Color)));

    internal static readonly DependencyProperty GradientEndColorProperty =
        DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingSlider),
            new PropertyMetadata(default(Color)));

    internal static readonly DependencyProperty TrackBackgroundColorProperty =
        DependencyProperty.Register(nameof(TrackBackgroundColor), typeof(Color), typeof(EverythingSlider),
            new PropertyMetadata(default(Color)));

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

    internal Color TrackBackgroundColor
    {
        get => (Color)GetValue(TrackBackgroundColorProperty);
        set => SetValue(TrackBackgroundColorProperty, value);
    }

    private static void OnColorNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingSlider slider)
        {
            slider.UpdateColors();
        }
    }
}
