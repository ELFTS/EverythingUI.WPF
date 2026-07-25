using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EverythingUI.WPF.Controls;

public class EverythingComboBox : ComboBox
{
    private Border? _dropDownBorder;
    private readonly ScaleTransform _borderOpenScaleTransform = new(1, 0);
    private readonly ScaleTransform _borderCloseScaleTransform = new(1, 1);
    private readonly CubicEase _easeOut = new() { EasingMode = EasingMode.EaseOut };
    private readonly CubicEase _easeIn = new() { EasingMode = EasingMode.EaseIn };
    // 复用动画对象，避免每次开/关都新建 DoubleAnimation
    private readonly DoubleAnimation _borderScaleOpenAnim = new() { Duration = TimeSpan.FromSeconds(0.4) };
    private readonly DoubleAnimation _borderOpacityOpenAnim = new() { Duration = TimeSpan.FromSeconds(0.15) };
    private readonly DoubleAnimation _borderScaleCloseAnim = new() { Duration = TimeSpan.FromSeconds(0.25) };
    private readonly DoubleAnimation _borderOpacityCloseAnim = new() { Duration = TimeSpan.FromSeconds(0.1) };

    static EverythingComboBox() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingComboBox),
            new FrameworkPropertyMetadata(typeof(EverythingComboBox)));

    public EverythingComboBox()
    {
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _dropDownBorder = GetTemplateChild("dropDownBorder") as Border;
        if (GetTemplateChild("popup") is Popup popup)
            popup.CustomPopupPlacementCallback = OnCustomPopupPlacement;
    }

    private CustomPopupPlacement[] OnCustomPopupPlacement(Size popupSize, Size targetSize, Point offset)
        => [new(new Point(0, targetSize.Height + 2), PopupPrimaryAxis.Vertical)];

    protected override void OnDropDownOpened(EventArgs e) { base.OnDropDownOpened(e); AnimateDropDownBlindsOpen(); }
    protected override void OnDropDownClosed(EventArgs e) { base.OnDropDownClosed(e); AnimateDropDownBlindsClose(); }

    private void AnimateDropDownBlindsOpen()
    {
        if (_dropDownBorder == null) return;
        _dropDownBorder.RenderTransformOrigin = new Point(0.5, 0);
        _dropDownBorder.RenderTransform = _borderOpenScaleTransform;
        _dropDownBorder.ClipToBounds = true;

        _borderScaleOpenAnim.From = 0;
        _borderScaleOpenAnim.To = 1;
        _borderScaleOpenAnim.EasingFunction = _easeOut;
        _borderOpenScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, _borderScaleOpenAnim);

        _borderOpacityOpenAnim.From = 0;
        _borderOpacityOpenAnim.To = 1;
        _borderOpacityOpenAnim.EasingFunction = _easeOut;
        _dropDownBorder.BeginAnimation(OpacityProperty, _borderOpacityOpenAnim);

        const double blindDelayMs = 30;       // 较小避免大列表过慢
        const double blindDurationMs = 200;
        var blindDuration = TimeSpan.FromMilliseconds(blindDurationMs);
        var blindDurationShort = TimeSpan.FromMilliseconds(blindDurationMs * 0.6);

        // 不使用 ToList 避免 O(n) 复制；延迟动画对每隔过多 item 取消（避免长列表过久）
        var count = Items.Count;
        var maxDelayCount = Math.Min(count, 16);   // 对前 16 项做错开，其余同步动画

        for (var i = 0; i < count; i++)
        {
            if (ItemContainerGenerator.ContainerFromIndex(i) is not ComboBoxItem container) continue;

            var scaleTransform = container.RenderTransform as ScaleTransform;
            if (scaleTransform == null)
            {
                scaleTransform = new ScaleTransform(1, 0);
                container.RenderTransform = scaleTransform;
                container.RenderTransformOrigin = new Point(0.5, 0);
            }
            else
            {
                container.RenderTransformOrigin = new Point(0.5, 0);
                scaleTransform.ScaleX = 1;
                scaleTransform.ScaleY = 0;
            }
            container.Opacity = 0;

            var delay = i < maxDelayCount ? TimeSpan.FromMilliseconds(blindDelayMs * i) + TimeSpan.FromSeconds(0.05) : TimeSpan.FromSeconds(0.05);

            var scaleAnim = new DoubleAnimation(0, 1, blindDuration) { BeginTime = delay, EasingFunction = _easeOut };
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnim);
            var opacityAnim = new DoubleAnimation(0, 1, blindDurationShort) { BeginTime = delay, EasingFunction = _easeOut };
            container.BeginAnimation(OpacityProperty, opacityAnim);
        }
    }

    private void AnimateDropDownBlindsClose()
    {
        if (_dropDownBorder == null) return;
        _dropDownBorder.RenderTransformOrigin = new Point(0.5, 1);
        _dropDownBorder.RenderTransform = _borderCloseScaleTransform;

        const double blindDelayMs = 20;
        const double blindDurationMs = 200;
        var blindDuration = TimeSpan.FromMilliseconds(blindDurationMs);

        var count = Items.Count;
        var maxDelayCount = Math.Min(count, 16);

        // 关闭时按倒序错开
        for (var i = count - 1; i >= 0; i--)
        {
            if (ItemContainerGenerator.ContainerFromIndex(i) is not ComboBoxItem container) continue;
            var reverseIndex = count - 1 - i;
            var delay = reverseIndex < maxDelayCount ? TimeSpan.FromMilliseconds(blindDelayMs * reverseIndex) : TimeSpan.Zero;

            container.RenderTransformOrigin = new Point(0.5, 1);
            var scaleTransform = container.RenderTransform as ScaleTransform;
            if (scaleTransform == null)
            {
                scaleTransform = new ScaleTransform(1, 1);
                container.RenderTransform = scaleTransform;
            }
            else
            {
                scaleTransform.ScaleX = 1;
                scaleTransform.ScaleY = 1;
            }

            var scaleAnim = new DoubleAnimation(1, 0, blindDuration) { BeginTime = delay, EasingFunction = _easeIn };
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnim);
            var opacityAnim = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(blindDurationMs * 0.8))
            { BeginTime = delay + TimeSpan.FromMilliseconds(blindDurationMs * 0.2), EasingFunction = _easeIn };
            container.BeginAnimation(OpacityProperty, opacityAnim);
        }

        // 固定关闭总时长封顶，避免长列表过久
        var totalItemDuration = TimeSpan.FromMilliseconds(blindDelayMs * Math.Min(count, maxDelayCount) + blindDurationMs);
        var borderScaleBegin = totalItemDuration - TimeSpan.FromSeconds(0.25);
        if (borderScaleBegin < TimeSpan.Zero) borderScaleBegin = TimeSpan.Zero;

        _borderScaleCloseAnim.From = 1;
        _borderScaleCloseAnim.To = 0;
        _borderScaleCloseAnim.BeginTime = borderScaleBegin;
        _borderScaleCloseAnim.EasingFunction = _easeIn;
        _borderCloseScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, _borderScaleCloseAnim);

        _borderOpacityCloseAnim.From = 1;
        _borderOpacityCloseAnim.To = 0;
        _borderOpacityCloseAnim.BeginTime = totalItemDuration - TimeSpan.FromSeconds(0.1);
        if (_borderOpacityCloseAnim.BeginTime < TimeSpan.Zero) _borderOpacityCloseAnim.BeginTime = TimeSpan.Zero;
        _borderOpacityCloseAnim.EasingFunction = _easeIn;
        _dropDownBorder.BeginAnimation(OpacityProperty, _borderOpacityCloseAnim);
    }

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(EverythingComboBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None));

    public string Placeholder { get => (string)GetValue(PlaceholderProperty); set => SetValue(PlaceholderProperty, value); }
}
