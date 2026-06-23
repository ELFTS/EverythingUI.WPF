using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingProgressBar : ProgressBar
{
    private FrameworkElement? _sweepLight;

    static EverythingProgressBar() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingProgressBar),
            new FrameworkPropertyMetadata(typeof(EverythingProgressBar)));

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _sweepLight = GetTemplateChild("SweepLight") as FrameworkElement;
        UpdateSweepLightVisibility();
    }

    protected override void OnValueChanged(double oldValue, double newValue)
    {
        base.OnValueChanged(oldValue, newValue);
        UpdateSweepLightVisibility();
    }

    private void UpdateSweepLightVisibility()
    {
        if (_sweepLight == null) return;
        _sweepLight.Visibility = Value >= Maximum ? Visibility.Collapsed : Visibility.Visible;
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingProgressBar),
            new FrameworkPropertyMetadata(new CornerRadius(6), FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty ShowPercentageProperty =
        DependencyProperty.Register(nameof(ShowPercentage), typeof(bool), typeof(EverythingProgressBar),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty AnimationDurationProperty =
        DependencyProperty.Register(nameof(AnimationDuration), typeof(Duration), typeof(EverythingProgressBar),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(300))));

    public CornerRadius CornerRadius { get => (CornerRadius)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }
    public bool ShowPercentage { get => (bool)GetValue(ShowPercentageProperty); set => SetValue(ShowPercentageProperty, value); }
    public Duration AnimationDuration { get => (Duration)GetValue(AnimationDurationProperty); set => SetValue(AnimationDurationProperty, value); }
}
