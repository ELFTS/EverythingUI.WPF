using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace EverythingUI.WPF.Controls;

public class EverythingButton : Button
{
    private DispatcherTimer? _longPressTimer;
    private Border? _shadowBorder;
    private Border? _mainBorder;
    private Border? _topInnerShadow;
    private Border? _sideInnerShadow;
    private Border? _glossLayer;
    private Border? _longPressIndicator;
    private Grid? _pressedInnerShadow;
    private Grid? _contentPanel;

    public new static readonly RoutedEvent ClickEvent =
        EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
            typeof(MouseButtonEventHandler), typeof(EverythingButton));

    public static readonly RoutedEvent LongPressEvent =
        EventManager.RegisterRoutedEvent(nameof(LongPress), RoutingStrategy.Bubble,
            typeof(MouseButtonEventHandler), typeof(EverythingButton));

    public new event MouseButtonEventHandler Click
    {
        add => AddHandler(ClickEvent, value);
        remove => RemoveHandler(ClickEvent, value);
    }

    public event MouseButtonEventHandler LongPress
    {
        add => AddHandler(LongPressEvent, value);
        remove => RemoveHandler(LongPressEvent, value);
    }

    static EverythingButton() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingButton),
            new FrameworkPropertyMetadata(typeof(EverythingButton)));

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _shadowBorder = GetTemplateChild("shadowBorder") as Border;
        _mainBorder = GetTemplateChild("border") as Border;
        _topInnerShadow = GetTemplateChild("topInnerShadow") as Border;
        _sideInnerShadow = GetTemplateChild("sideInnerShadow") as Border;
        _glossLayer = GetTemplateChild("glossLayer") as Border;
        _longPressIndicator = GetTemplateChild("longPressIndicator") as Border;
        _pressedInnerShadow = GetTemplateChild("pressedInnerShadow") as Grid;
        _contentPanel = GetTemplateChild("contentPanel") as Grid;
        UpdateCapsuleCornerRadius();
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        UpdateCapsuleCornerRadius();
    }

    private void UpdateCapsuleCornerRadius()
    {
        if (!IsCapsule) return;
        var radius = ActualHeight / 2.0;
        var corner = new CornerRadius(radius);
        if (_shadowBorder != null) _shadowBorder.CornerRadius = corner;
        if (_mainBorder != null) _mainBorder.CornerRadius = corner;
        if (_topInnerShadow != null) _topInnerShadow.CornerRadius = corner;
        if (_sideInnerShadow != null) _sideInnerShadow.CornerRadius = corner;
        if (_glossLayer != null) _glossLayer.CornerRadius = new CornerRadius(radius, radius, 0, 0);
        if (_longPressIndicator != null) _longPressIndicator.CornerRadius = corner;
    }

    private static void OnCapsuleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingButton btn)
        {
            if ((bool)e.NewValue)
                btn.UpdateCapsuleCornerRadius();
            else
                btn.ResetCornerRadius();
        }
    }

    private void ResetCornerRadius()
    {
        var corner = new CornerRadius(6);
        if (_shadowBorder != null) _shadowBorder.CornerRadius = corner;
        if (_mainBorder != null) _mainBorder.CornerRadius = corner;
        if (_topInnerShadow != null) _topInnerShadow.CornerRadius = corner;
        if (_sideInnerShadow != null) _sideInnerShadow.CornerRadius = corner;
        if (_glossLayer != null) _glossLayer.CornerRadius = new CornerRadius(6, 6, 0, 0);
        if (_longPressIndicator != null) _longPressIndicator.CornerRadius = corner;
    }

    protected override void OnClick()
    {
        if (IsLongPressEnabled) return;

        base.OnClick();
        RaiseClickEvent();
    }

    protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnPreviewMouseLeftButtonDown(e);
        if (!IsLongPressEnabled || !IsEnabled) return;

        StartLongPressAnimation();

        _longPressTimer ??= new DispatcherTimer();
        _longPressTimer.Stop();
        _longPressTimer.Interval = LongPressDuration;
        _longPressTimer.Tick -= OnLongPressTimerTick;
        _longPressTimer.Tick += OnLongPressTimerTick;
        _longPressTimer.Start();
    }

    protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        StopLongPressTimer();
        ResetLongPressAnimation();
        base.OnPreviewMouseLeftButtonUp(e);
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        StopLongPressTimer();
        ResetLongPressAnimation();
        base.OnMouseLeave(e);
    }

    private void StartLongPressAnimation()
    {
        if (_longPressIndicator == null) return;
        _longPressIndicator.BeginAnimation(OpacityProperty, null);
        _longPressIndicator.Opacity = 1;
        _longPressIndicator.BeginAnimation(WidthProperty, null);
        _longPressIndicator.Width = 0;
        var anim = new DoubleAnimation(0, ActualWidth, LongPressDuration)
        { EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
        _longPressIndicator.BeginAnimation(WidthProperty, anim);
    }

    private void ResetLongPressAnimation()
    {
        if (_longPressIndicator == null) return;
        var from = _longPressIndicator.Width;
        _longPressIndicator.BeginAnimation(WidthProperty, null);
        if (from <= 0)
        {
            _longPressIndicator.BeginAnimation(OpacityProperty, null);
            _longPressIndicator.Opacity = 0;
            return;
        }
        var widthAnim = new DoubleAnimation(from, 0, TimeSpan.FromMilliseconds(150));
        _longPressIndicator.BeginAnimation(WidthProperty, widthAnim);
        var fade = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(150));
        _longPressIndicator.BeginAnimation(OpacityProperty, fade);
    }

    private void OnLongPressTimerTick(object? sender, EventArgs e)
    {
        StopLongPressTimer();
        ResetLongPressAnimation();
        ResetPressedVisualState();
        RaiseClickEvent();
        RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice, Environment.TickCount, MouseButton.Left)
        { RoutedEvent = LongPressEvent, Source = this });
    }

    private void ResetPressedVisualState()
    {
        // 清除 XAML EventTrigger(PreviewMouseDown) 设置的按下动画，恢复到正常状态
        if (_pressedInnerShadow != null)
        {
            _pressedInnerShadow.BeginAnimation(OpacityProperty, null);
            _pressedInnerShadow.Opacity = 0;
        }
        if (_glossLayer != null)
        {
            _glossLayer.BeginAnimation(OpacityProperty, null);
            _glossLayer.Opacity = 1;
        }
        if (_contentPanel != null && _contentPanel.RenderTransform is ScaleTransform scale)
        {
            _contentPanel.BeginAnimation(OpacityProperty, null);
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            scale.ScaleX = 1;
            scale.ScaleY = 1;
        }
    }

    private void StopLongPressTimer() => _longPressTimer?.Stop();

    private void RaiseClickEvent() =>
        RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice, Environment.TickCount, MouseButton.Left)
        { RoutedEvent = ClickEvent, Source = this });

    public static readonly DependencyProperty IsCapsuleProperty =
        DependencyProperty.Register(nameof(IsCapsule), typeof(bool), typeof(EverythingButton),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, OnCapsuleChanged));

    public static readonly DependencyProperty IsLongPressEnabledProperty =
        DependencyProperty.Register(nameof(IsLongPressEnabled), typeof(bool), typeof(EverythingButton),
            new FrameworkPropertyMetadata(false));

    public static readonly DependencyProperty LongPressDurationProperty =
        DependencyProperty.Register(nameof(LongPressDuration), typeof(TimeSpan), typeof(EverythingButton),
            new FrameworkPropertyMetadata(TimeSpan.FromMilliseconds(700)));

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(EverythingButton),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty IconPlacementProperty =
        DependencyProperty.Register(nameof(IconPlacement), typeof(Dock), typeof(EverythingButton),
            new FrameworkPropertyMetadata(Dock.Left, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(EverythingButton),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public bool IsCapsule { get => (bool)GetValue(IsCapsuleProperty); set => SetValue(IsCapsuleProperty, value); }
    public bool IsLongPressEnabled { get => (bool)GetValue(IsLongPressEnabledProperty); set => SetValue(IsLongPressEnabledProperty, value); }
    public TimeSpan LongPressDuration { get => (TimeSpan)GetValue(LongPressDurationProperty); set => SetValue(LongPressDurationProperty, value); }
    public object Icon { get => GetValue(IconProperty); set => SetValue(IconProperty, value); }
    public Dock IconPlacement { get => (Dock)GetValue(IconPlacementProperty); set => SetValue(IconPlacementProperty, value); }
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
}
