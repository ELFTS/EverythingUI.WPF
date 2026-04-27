using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EverythingUI.WPF.Controls;

public class EverythingCircularProgressBar : Control
{
    private DoubleAnimation? _angleAnimation;
    private bool _isAnimating = false;

    static EverythingCircularProgressBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingCircularProgressBar),
            new FrameworkPropertyMetadata(typeof(EverythingCircularProgressBar)));
    }

    public EverythingCircularProgressBar()
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

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(double), typeof(EverythingCircularProgressBar),
            new PropertyMetadata(0.0, OnValueChanged));

    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(EverythingCircularProgressBar),
            new PropertyMetadata(0.0));

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(EverythingCircularProgressBar),
            new PropertyMetadata(100.0));

    public static readonly DependencyProperty GradientStartColorProperty =
        DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingCircularProgressBar),
            new PropertyMetadata(default(Color)));

    public static readonly DependencyProperty GradientEndColorProperty =
        DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingCircularProgressBar),
            new PropertyMetadata(default(Color)));

    public static readonly DependencyProperty TrackColorProperty =
        DependencyProperty.Register(nameof(TrackColor), typeof(Color), typeof(EverythingCircularProgressBar),
            new PropertyMetadata(default(Color)));

    public static readonly DependencyProperty StrokeThicknessProperty =
        DependencyProperty.Register(nameof(StrokeThickness), typeof(double), typeof(EverythingCircularProgressBar),
            new PropertyMetadata(8.0));

    public static readonly DependencyProperty ShowPercentageProperty =
        DependencyProperty.Register(nameof(ShowPercentage), typeof(bool), typeof(EverythingCircularProgressBar),
            new PropertyMetadata(false));

    public static readonly DependencyProperty AnimationDurationProperty =
        DependencyProperty.Register(nameof(AnimationDuration), typeof(Duration), typeof(EverythingCircularProgressBar),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(300))));

    public static readonly DependencyProperty AnimatedAngleProperty =
        DependencyProperty.Register(nameof(AnimatedAngle), typeof(double), typeof(EverythingCircularProgressBar),
            new PropertyMetadata(0.0));

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
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

    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
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

    public double AnimatedAngle
    {
        get => (double)GetValue(AnimatedAngleProperty);
        private set => SetValue(AnimatedAngleProperty, value);
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingCircularProgressBar control && !control._isAnimating)
        {
            control.AnimateToNewAngle();
        }
    }

    private void AnimateToNewAngle()
    {
        double targetAngle = CalculateTargetAngle();

        if (!AnimationDuration.HasTimeSpan || AnimationDuration.TimeSpan.TotalMilliseconds <= 0)
        {
            AnimatedAngle = targetAngle;
            return;
        }

        // 如果正在动画中，先停止
        if (_isAnimating)
        {
            BeginAnimation(AnimatedAngleProperty, null);
            _isAnimating = false;
        }

        // 获取当前实际值（可能是动画中的值）
        double currentAngle = AnimatedAngle;

        _angleAnimation = new DoubleAnimation
        {
            From = currentAngle,
            To = targetAngle,
            Duration = AnimationDuration,
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        _angleAnimation.Completed += (s, e) =>
        {
            _isAnimating = false;
            AnimatedAngle = targetAngle;
        };

        _isAnimating = true;
        BeginAnimation(AnimatedAngleProperty, _angleAnimation);
    }

    private double CalculateTargetAngle()
    {
        double range = Maximum - Minimum;
        if (range <= 0) return 0;

        double normalizedValue = (Value - Minimum) / range;
        return normalizedValue * 360;
    }
}
