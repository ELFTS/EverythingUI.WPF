using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EverythingUI.WPF.Controls;

public class EverythingProgressBar : ProgressBar
{
    static EverythingProgressBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingProgressBar),
            new FrameworkPropertyMetadata(typeof(EverythingProgressBar)));
    }

    public EverythingProgressBar()
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
        SetCurrentValue(TrackColorProperty, ColorHelper.GetTrackColor(ColorName));
    }

    public static readonly DependencyProperty ColorNameProperty =
        DependencyProperty.Register(nameof(ColorName), typeof(ColorName), typeof(EverythingProgressBar),
            new PropertyMetadata(ColorName.Blue, OnColorNameChanged));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingProgressBar),
            new PropertyMetadata(new CornerRadius(6)));

    internal static readonly DependencyProperty GradientStartColorProperty =
        DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingProgressBar),
            new PropertyMetadata(default(Color)));

    internal static readonly DependencyProperty GradientEndColorProperty =
        DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingProgressBar),
            new PropertyMetadata(default(Color)));

    internal static readonly DependencyProperty TrackColorProperty =
        DependencyProperty.Register(nameof(TrackColor), typeof(Color), typeof(EverythingProgressBar),
            new PropertyMetadata(default(Color)));

    public static readonly DependencyProperty ShowPercentageProperty =
        DependencyProperty.Register(nameof(ShowPercentage), typeof(bool), typeof(EverythingProgressBar),
            new PropertyMetadata(false));

    public static readonly DependencyProperty AnimationDurationProperty =
        DependencyProperty.Register(nameof(AnimationDuration), typeof(Duration), typeof(EverythingProgressBar),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(300))));

    /// <summary>
    /// 颜色名称 - 直接使用颜色英文名
    /// </summary>
    public ColorName ColorName
    {
        get => (ColorName)GetValue(ColorNameProperty);
        set => SetValue(ColorNameProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
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

    internal Color TrackColor
    {
        get => (Color)GetValue(TrackColorProperty);
        set => SetValue(TrackColorProperty, value);
    }

    public bool ShowPercentage
    {
        get => (bool)GetValue(ShowPercentageProperty);
        set => SetValue(ShowPercentageProperty, value);
    }

    public Duration AnimationDuration
    {
        get => (Duration)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }

    private static void OnColorNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingProgressBar progressBar)
        {
            progressBar.UpdateColors();
        }
    }
}
