using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EverythingUI.WPF.Controls;

public class EverythingCircularProgressBar : Control
{
    private const double BackEaseAmplitude = 0.3;
    private const double DefaultAnimationMilliseconds = 400;

    private readonly Stopwatch _animationClock = new();
    private Path? _progressPath;
    private double _animationStartValue;
    private double _animationTargetValue;
    private bool _isAnimating;

    static EverythingCircularProgressBar() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingCircularProgressBar),
            new FrameworkPropertyMetadata(typeof(EverythingCircularProgressBar)));

    public EverythingCircularProgressBar()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _progressPath = GetTemplateChild("ProgressPath") as Path;
        UpdateProgressGeometry();
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        UpdateProgressGeometry();
    }

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(double), typeof(EverythingCircularProgressBar),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, OnValueChanged));

    public static readonly DependencyProperty AnimatedValueProperty =
        DependencyProperty.Register(nameof(AnimatedValue), typeof(double), typeof(EverythingCircularProgressBar),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, OnAnimatedValueChanged));

    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(EverythingCircularProgressBar),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, OnRangeChanged));

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(EverythingCircularProgressBar),
            new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsRender, OnRangeChanged));

    public static readonly DependencyProperty StrokeThicknessProperty =
        DependencyProperty.Register(nameof(StrokeThickness), typeof(double), typeof(EverythingCircularProgressBar),
            new FrameworkPropertyMetadata(8.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender, OnRangeChanged));

    public static readonly DependencyProperty ShowPercentageProperty =
        DependencyProperty.Register(nameof(ShowPercentage), typeof(bool), typeof(EverythingCircularProgressBar),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty AnimationDurationProperty =
        DependencyProperty.Register(nameof(AnimationDuration), typeof(Duration), typeof(EverythingCircularProgressBar),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(DefaultAnimationMilliseconds))));

    public double Value { get => (double)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
    public double AnimatedValue { get => (double)GetValue(AnimatedValueProperty); set => SetValue(AnimatedValueProperty, value); }
    public double Minimum { get => (double)GetValue(MinimumProperty); set => SetValue(MinimumProperty, value); }
    public double Maximum { get => (double)GetValue(MaximumProperty); set => SetValue(MaximumProperty, value); }
    public double StrokeThickness { get => (double)GetValue(StrokeThicknessProperty); set => SetValue(StrokeThicknessProperty, value); }
    public bool ShowPercentage { get => (bool)GetValue(ShowPercentageProperty); set => SetValue(ShowPercentageProperty, value); }
    public Duration AnimationDuration { get => (Duration)GetValue(AnimationDurationProperty); set => SetValue(AnimationDurationProperty, value); }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingCircularProgressBar bar)
            bar.StartAnimation((double)e.NewValue);
    }

    private static void OnAnimatedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingCircularProgressBar bar)
            bar.UpdateProgressGeometry();
    }

    private static void OnRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingCircularProgressBar bar)
            bar.UpdateProgressGeometry();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        AnimatedValue = Value;
        UpdateProgressGeometry();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e) => StopAnimation();

    private void StartAnimation(double targetValue)
    {
        if (!IsLoaded)
        {
            AnimatedValue = targetValue;
            return;
        }

        _animationStartValue = AnimatedValue;
        _animationTargetValue = targetValue;
        _animationClock.Restart();

        if (_isAnimating)
            return;

        _isAnimating = true;
        CompositionTarget.Rendering += OnRendering;
    }

    private void OnRendering(object? sender, EventArgs e)
    {
        double duration = GetAnimationMilliseconds();
        double progress = Math.Min(1, _animationClock.Elapsed.TotalMilliseconds / duration);
        double easedProgress = EaseOutBack(progress);

        AnimatedValue = _animationStartValue + (_animationTargetValue - _animationStartValue) * easedProgress;

        if (progress >= 1)
        {
            AnimatedValue = _animationTargetValue;
            StopAnimation();
        }
    }

    private double GetAnimationMilliseconds()
    {
        if (!AnimationDuration.HasTimeSpan)
            return DefaultAnimationMilliseconds;

        return Math.Max(1, AnimationDuration.TimeSpan.TotalMilliseconds);
    }

    private void StopAnimation()
    {
        if (!_isAnimating)
            return;

        CompositionTarget.Rendering -= OnRendering;
        _animationClock.Stop();
        _isAnimating = false;
    }

    private static double EaseOutBack(double progress)
    {
        double c1 = 1.70158 * BackEaseAmplitude;
        double c3 = c1 + 1;
        double p = progress - 1;
        return 1 + c3 * p * p * p + c1 * p * p;
    }

    private void UpdateProgressGeometry()
    {
        if (_progressPath == null)
            return;

        double diameter = Math.Min(ActualWidth, ActualHeight);
        _progressPath.Data = CircularArcHelper.CreateGeometry(AnimatedValue, Minimum, Maximum, diameter, StrokeThickness);
    }
}
