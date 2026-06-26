using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EverythingUI.WPF.Controls;

public class EverythingProgressBar : ProgressBar
{
    private FrameworkElement? _sweepLight;
    private FrameworkElement? _progressGrid;

    static EverythingProgressBar() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingProgressBar),
            new FrameworkPropertyMetadata(typeof(EverythingProgressBar)));

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _sweepLight = GetTemplateChild("SweepLight") as FrameworkElement;
        _progressGrid = GetTemplateChild("ProgressGrid") as FrameworkElement;
        UpdateSweepLightVisibility();
        UpdateProgressWidth(animate: false);
    }

    protected override void OnValueChanged(double oldValue, double newValue)
    {
        base.OnValueChanged(oldValue, newValue);
        UpdateSweepLightVisibility();
        UpdateProgressWidth(animate: true);
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        if (sizeInfo.WidthChanged)
            UpdateProgressWidth(animate: false);
    }

    private void UpdateSweepLightVisibility()
    {
        if (_sweepLight == null) return;
        _sweepLight.Visibility = Value >= Maximum ? Visibility.Collapsed : Visibility.Visible;
    }

    private void UpdateProgressWidth(bool animate)
    {
        if (_progressGrid == null) return;

        double actualWidth = ActualWidth;
        if (actualWidth <= 0 || double.IsNaN(actualWidth)) return;

        double progress = Maximum > Minimum
            ? Math.Max(0, Math.Min(1, (Value - Minimum) / (Maximum - Minimum)))
            : 0;
        double targetWidth = actualWidth * progress;

        if (!animate)
        {
            _progressGrid.BeginAnimation(WidthProperty, null);
            _progressGrid.Width = targetWidth;
            return;
        }

        var animation = new DoubleAnimation
        {
            To = targetWidth,
            Duration = AnimationDuration,
            EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.3 }
        };
        _progressGrid.BeginAnimation(WidthProperty, animation);
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingProgressBar),
            new FrameworkPropertyMetadata(new CornerRadius(6), FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty ShowPercentageProperty =
        DependencyProperty.Register(nameof(ShowPercentage), typeof(bool), typeof(EverythingProgressBar),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty AnimationDurationProperty =
        DependencyProperty.Register(nameof(AnimationDuration), typeof(Duration), typeof(EverythingProgressBar),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(400))));

    public CornerRadius CornerRadius { get => (CornerRadius)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }
    public bool ShowPercentage { get => (bool)GetValue(ShowPercentageProperty); set => SetValue(ShowPercentageProperty, value); }
    public Duration AnimationDuration { get => (Duration)GetValue(AnimationDurationProperty); set => SetValue(AnimationDurationProperty, value); }
}
