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
        if (GradientStartColor == default)
        {
            SetCurrentValue(GradientStartColorProperty, (Color)FindResource("GradientBlueStart"));
        }
        if (GradientEndColor == default)
        {
            SetCurrentValue(GradientEndColorProperty, (Color)FindResource("GradientBlueEnd"));
        }
        if (TrackColor == default)
        {
            SetCurrentValue(TrackColorProperty, (Color)ColorConverter.ConvertFromString("#E6E6E6"));
        }
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingProgressBar),
            new PropertyMetadata(new CornerRadius(6)));

    public static readonly DependencyProperty GradientStartColorProperty =
        DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingProgressBar),
            new PropertyMetadata(default(Color)));

    public static readonly DependencyProperty GradientEndColorProperty =
        DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingProgressBar),
            new PropertyMetadata(default(Color)));

    public static readonly DependencyProperty TrackColorProperty =
        DependencyProperty.Register(nameof(TrackColor), typeof(Color), typeof(EverythingProgressBar),
            new PropertyMetadata(default(Color)));

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

    public Color TrackColor
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
}
