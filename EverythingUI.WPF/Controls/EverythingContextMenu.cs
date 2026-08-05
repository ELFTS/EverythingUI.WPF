using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EverythingUI.WPF.Controls;

public class EverythingContextMenu : Control
{
    private Popup? _popup;
    private Border? _rootBorder;
    private UIElement? _attachedTarget;

    static EverythingContextMenu()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingContextMenu),
            new FrameworkPropertyMetadata(typeof(EverythingContextMenu)));

        EventManager.RegisterClassHandler(typeof(UIElement), UIElement.MouseLeftButtonUpEvent,
            new RoutedEventHandler(OnItemMouseUp));
    }

    private static void OnItemMouseUp(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement fe) return;
        if (fe.DataContext is not EverythingContextMenuItem item) return;
        if (item.IsSeparator || !item.IsEnabled) return;

        DependencyObject? current = fe;
        while (current != null && current is not EverythingContextMenu)
            current = VisualTreeHelper.GetParent(current) ?? LogicalTreeHelper.GetParent(current);
        if (current is not EverythingContextMenu menu) return;

        menu.RaiseItemClick(item);
        menu.Close();
        e.Handled = true;
    }

    public ObservableCollection<EverythingContextMenuItem> Items { get; } = new();

    public EverythingContextMenu()
    {
        Loaded += (_, _) => AttachToTarget(PlacementTarget);
        Unloaded += (_, _) => DetachFromTarget();
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_popup != null)
            _popup.Closed -= OnPopupClosed;

        _popup = GetTemplateChild("PART_Popup") as Popup;
        _rootBorder = GetTemplateChild("rootBorder") as Border;

        if (_popup != null)
        {
            _popup.Closed += OnPopupClosed;
            if (GetTemplateChild("PART_Items") is ItemsControl itemsHost)
                itemsHost.ItemsSource = Items;
        }

        if (IsLoaded)
            AttachToTarget(PlacementTarget);
    }

    private void OnPopupClosed(object? sender, EventArgs e)
    {
        if (_rootBorder != null)
            _rootBorder.Opacity = 0;
    }

    private static void OnPlacementTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not EverythingContextMenu menu) return;
        menu.DetachFromTarget();
        menu.AttachToTarget((UIElement?)e.NewValue);
    }

    private void AttachToTarget(UIElement? target)
    {
        if (target == null || ReferenceEquals(target, _attachedTarget)) return;
        _attachedTarget = target;
        target.PreviewMouseRightButtonUp += OnTargetRightButtonUp;
    }

    private void DetachFromTarget()
    {
        if (_attachedTarget == null) return;
        _attachedTarget.PreviewMouseRightButtonUp -= OnTargetRightButtonUp;
        _attachedTarget = null;
    }

    private void OnTargetRightButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (sender is UIElement target)
        {
            Show(target);
            e.Handled = true;
        }
    }

    public void Show(UIElement placementTarget)
    {
        if (_popup == null) ApplyTemplate();
        if (_popup == null) return;
        _popup.Placement = PlacementMode.MousePoint;
        _popup.PlacementTarget = placementTarget;
        _popup.IsOpen = true;
        PlayOpenAnimation();
    }

    public void Show(UIElement placementTarget, Point position)
    {
        if (_popup == null) ApplyTemplate();
        if (_popup == null) return;
        _popup.Placement = PlacementMode.RelativePoint;
        _popup.PlacementTarget = placementTarget;
        _popup.HorizontalOffset = position.X;
        _popup.VerticalOffset = position.Y;
        _popup.IsOpen = true;
        PlayOpenAnimation();
    }

    public void Close()
    {
        if (_popup == null) return;
        _popup.IsOpen = false;
        if (_rootBorder != null)
            _rootBorder.Opacity = 0;
    }

    private void PlayOpenAnimation()
    {
        if (_rootBorder == null) return;

        if (_rootBorder.RenderTransform is not ScaleTransform)
            _rootBorder.RenderTransform = new ScaleTransform(0.96, 0.96);

        var st = (ScaleTransform)_rootBorder.RenderTransform;
        var ease = new CubicEase { EasingMode = EasingMode.EaseOut };

        _rootBorder.BeginAnimation(OpacityProperty,
            new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(140)) { EasingFunction = ease });
        st.BeginAnimation(ScaleTransform.ScaleXProperty,
            new DoubleAnimation(0.96, 1, TimeSpan.FromMilliseconds(160)) { EasingFunction = ease });
        st.BeginAnimation(ScaleTransform.ScaleYProperty,
            new DoubleAnimation(0.96, 1, TimeSpan.FromMilliseconds(160)) { EasingFunction = ease });
    }

    public event EventHandler<EverythingContextMenuItemClickEventArgs>? ItemClick;

    internal void RaiseItemClick(EverythingContextMenuItem item)
    {
        ItemClick?.Invoke(this, new EverythingContextMenuItemClickEventArgs(item));
        if (item.Command != null && item.Command.CanExecute(item.CommandParameter))
            item.Command.Execute(item.CommandParameter);
    }

    public static readonly DependencyProperty PlacementTargetProperty =
        DependencyProperty.Register(nameof(PlacementTarget), typeof(UIElement), typeof(EverythingContextMenu),
            new FrameworkPropertyMetadata(null, OnPlacementTargetChanged));

    public UIElement? PlacementTarget
    {
        get => (UIElement?)GetValue(PlacementTargetProperty);
        set => SetValue(PlacementTargetProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingContextMenu),
            new FrameworkPropertyMetadata(new CornerRadius(10), FrameworkPropertyMetadataOptions.AffectsRender));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty ItemHeightProperty =
        DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(EverythingContextMenu),
            new FrameworkPropertyMetadata(34.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public double ItemHeight
    {
        get => (double)GetValue(ItemHeightProperty);
        set => SetValue(ItemHeightProperty, value);
    }

    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(EverythingContextMenu),
            new FrameworkPropertyMetadata(16.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public double IconSize
    {
        get => (double)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
}
