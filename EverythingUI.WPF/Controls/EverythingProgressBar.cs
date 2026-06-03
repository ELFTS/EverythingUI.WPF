using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingProgressBar : ProgressBar
{
    static EverythingProgressBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingProgressBar),
            new FrameworkPropertyMetadata(typeof(EverythingProgressBar)));
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingProgressBar),
            new PropertyMetadata(new CornerRadius(6)));

    public static readonly DependencyProperty ShowPercentageProperty =
        DependencyProperty.Register(nameof(ShowPercentage), typeof(bool), typeof(EverythingProgressBar),
            new PropertyMetadata(false));

    public static readonly DependencyProperty AnimationDurationProperty =
        DependencyProperty.Register(nameof(AnimationDuration), typeof(Duration), typeof(EverythingProgressBar),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(300))));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
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
}
