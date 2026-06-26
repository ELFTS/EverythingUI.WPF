using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EverythingUI.WPF.Controls;

public enum ToolBarItemDisplayMode { TextOnly, IconOnly, IconLeft, IconTop }

public class EverythingToolBar : Control
{
    private ListBox? _menuListBox;
    private ScrollViewer? _scrollViewer;
    private ScrollBar? _scrollBar;
    private Grid? _selectionIndicator;
    private Border? _indicatorBackground;

    private readonly CubicEase _easeOut = new() { EasingMode = EasingMode.EaseOut };
    private readonly CubicEase _easeIn = new() { EasingMode = EasingMode.EaseIn };
    private ThicknessAnimation? _cachedMarginAnimation;
    private DoubleAnimation? _cachedOpacityAnimation, _cachedHoverInAnimation, _cachedHoverOutAnimation;

    static EverythingToolBar() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingToolBar), new FrameworkPropertyMetadata(typeof(EverythingToolBar)));

    public EverythingToolBar()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ColorManager.SetColorName(this, Themes.ThemeManager.CurrentColorName);
        ColorManager.UpdateColors(this);
        SelectFirstItem();
        Themes.ThemeManager.ColorChanged -= OnThemeColorChanged;
        Themes.ThemeManager.ColorChanged += OnThemeColorChanged;
        RestoreIndicatorPosition();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Themes.ThemeManager.ColorChanged -= OnThemeColorChanged;
        if (_scrollViewer != null) _scrollViewer.ScrollChanged -= OnScrollViewerScrollChanged;
        _selectionIndicator?.BeginAnimation(MarginProperty, null);
        _selectionIndicator?.BeginAnimation(UIElement.OpacityProperty, null);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _menuListBox = GetTemplateChild("menuListBox") as ListBox;
        _scrollViewer = GetTemplateChild("scrollViewer") as ScrollViewer;
        _scrollBar = GetTemplateChild("PART_HorizontalScrollBar") as ScrollBar;
        _selectionIndicator = GetTemplateChild("selectionIndicator") as Grid;
        _indicatorBackground = GetTemplateChild("indicatorBackground") as Border;

        if (_menuListBox != null)
        {
            _menuListBox.ItemContainerGenerator.StatusChanged += OnItemContainerGeneratorStatusChanged;
            _menuListBox.SelectionChanged += OnMenuListBoxSelectionChanged;
            AttachItemEvents();
        }
        SetupScrollSync();
        UpdateIndicatorColor();
    }

    private void SetupScrollSync()
    {
        if (_scrollViewer == null || _scrollBar == null) return;
        _scrollBar.ValueChanged += (s, e) => _scrollViewer.ScrollToHorizontalOffset(_scrollBar.Value);
        _scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
    }

    private void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (e.HorizontalChange != 0) _scrollBar!.Value = _scrollViewer!.HorizontalOffset;
        SyncIndicatorPosition();
    }

    private void OnItemContainerGeneratorStatusChanged(object? sender, EventArgs e)
    {
        if (_menuListBox?.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated) return;
        AttachItemEvents();
        RestoreIndicatorToSelected();
    }

    private void OnMenuListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_menuListBox == null) return;
        if (_menuListBox.SelectedIndex >= 0 &&
            _menuListBox.ItemContainerGenerator.ContainerFromIndex(_menuListBox.SelectedIndex) is ListBoxItem item)
            AnimateIndicatorToItem(item);
        else
            AnimateIndicatorOpacity(0);
    }

    private void AttachItemEvents()
    {
        if (_menuListBox == null) return;
        for (int i = 0; i < _menuListBox.Items.Count; i++)
        {
            if (_menuListBox.ItemContainerGenerator.ContainerFromIndex(i) is not ListBoxItem item) continue;
            item.MouseEnter -= OnItemMouseEnter;
            item.MouseLeave -= OnItemMouseLeave;
            item.MouseEnter += OnItemMouseEnter;
            item.MouseLeave += OnItemMouseLeave;
        }
    }

    private void OnItemMouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is ListBoxItem item && !item.IsSelected) AnimateItemHover(item, true);
    }
    private void OnItemMouseLeave(object sender, MouseEventArgs e)
    {
        if (sender is ListBoxItem item && !item.IsSelected) AnimateItemHover(item, false);
    }

    private void AnimateItemHover(ListBoxItem item, bool isHovering)
    {
        var border = FindChild<Border>(item, "border");
        if (border == null) return;
        if (isHovering)
        {
            _cachedHoverInAnimation ??= new DoubleAnimation(1, TimeSpan.FromSeconds(0.2)) { EasingFunction = _easeOut };
            border.BeginAnimation(OpacityProperty, _cachedHoverInAnimation);
        }
        else
        {
            _cachedHoverOutAnimation ??= new DoubleAnimation(0, TimeSpan.FromSeconds(0.15)) { EasingFunction = _easeIn };
            border.BeginAnimation(OpacityProperty, _cachedHoverOutAnimation);
        }
    }

    private void ApplySizeAndMargin(double left, double top, double width, double height)
    {
        _selectionIndicator!.Width = width;
        _selectionIndicator.Height = height;
        _selectionIndicator.Margin = new Thickness(left, top, 0, 0);
    }

    private void RestoreIndicatorPosition()
    {
        if (_menuListBox == null || _selectionIndicator == null) return;
        if (_menuListBox.SelectedIndex >= 0 &&
            _menuListBox.ItemContainerGenerator.ContainerFromIndex(_menuListBox.SelectedIndex) is ListBoxItem item)
            SyncIndicatorToItem(item);
        else if (_menuListBox.SelectedIndex < 0)
            _selectionIndicator.Opacity = 0;
    }

    private void RestoreIndicatorToSelected()
    {
        if (_menuListBox == null || _selectionIndicator == null || _menuListBox.SelectedIndex < 0) return;
        if (_menuListBox.ItemContainerGenerator.ContainerFromIndex(_menuListBox.SelectedIndex) is ListBoxItem item)
            SyncIndicatorToItem(item);
    }

    private void SyncIndicatorToItem(ListBoxItem item)
    {
        if (_selectionIndicator == null || _menuListBox == null) return;
        var pos = GetItemPositionInListBox(item);
        if (!pos.HasValue) return;
        _selectionIndicator.BeginAnimation(MarginProperty, null);
        _selectionIndicator.BeginAnimation(UIElement.OpacityProperty, null);
        ApplySizeAndMargin(pos.Value.left, pos.Value.top, pos.Value.width, pos.Value.height);
        _selectionIndicator.Opacity = 1;
    }

    private void AnimateIndicatorToItem(ListBoxItem item)
    {
        if (_selectionIndicator == null || _menuListBox == null) return;
        var pos = GetItemPositionInListBox(item);
        if (!pos.HasValue) return;
        _selectionIndicator.Width = pos.Value.width;
        _selectionIndicator.Height = pos.Value.height;
        _selectionIndicator.BeginAnimation(UIElement.OpacityProperty, null);
        _selectionIndicator.Opacity = 1;
        _cachedMarginAnimation ??= new ThicknessAnimation { Duration = TimeSpan.FromSeconds(0.25), EasingFunction = _easeOut };
        _cachedMarginAnimation.To = new Thickness(pos.Value.left, pos.Value.top, 0, 0);
        _selectionIndicator.BeginAnimation(MarginProperty, _cachedMarginAnimation);
    }

    private void AnimateIndicatorOpacity(double targetOpacity)
    {
        if (_selectionIndicator == null) return;
        _cachedOpacityAnimation ??= new DoubleAnimation { Duration = TimeSpan.FromSeconds(0.15), EasingFunction = _easeOut };
        _cachedOpacityAnimation.To = targetOpacity;
        _selectionIndicator.BeginAnimation(UIElement.OpacityProperty, _cachedOpacityAnimation);
    }

    private void SyncIndicatorPosition()
    {
        if (_menuListBox == null || _selectionIndicator == null || _menuListBox.SelectedIndex < 0) return;
        if (_menuListBox.ItemContainerGenerator.ContainerFromIndex(_menuListBox.SelectedIndex) is not ListBoxItem item) return;
        var pos = GetItemPositionInListBox(item);
        if (!pos.HasValue) return;
        _selectionIndicator.BeginAnimation(MarginProperty, null);
        ApplySizeAndMargin(pos.Value.left, pos.Value.top, pos.Value.width, pos.Value.height);
    }

    private (double left, double top, double width, double height)? GetItemPositionInListBox(ListBoxItem item)
    {
        if (_menuListBox == null) return null;
        try
        {
            var transform = item.TransformToVisual(_menuListBox);
            var pos = transform.Transform(new Point(0, 0));
            return (pos.X, pos.Y, item.ActualWidth, item.ActualHeight);
        }
        catch (InvalidOperationException) { return null; }
    }

    private void OnThemeColorChanged(object? sender, ColorName colorName)
    {
        Dispatcher.BeginInvoke(() =>
        {
            ColorManager.SetColorName(this, colorName);
            ColorManager.UpdateColors(this);
            UpdateIndicatorColor();
        }, System.Windows.Threading.DispatcherPriority.Render);
    }

    private void UpdateIndicatorColor()
    {
        _indicatorBackground?.ClearValue(Border.BackgroundProperty);
    }

    private static T? FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
    {
        if (parent == null) return null;
        int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < childrenCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is FrameworkElement fe && fe.Name == childName && child is T result) return result;
            var nested = FindChild<T>(child, childName);
            if (nested != null) return nested;
        }
        return null;
    }

    private void SelectFirstItem()
    {
        if (SelectedItem == null && ItemsSource is IEnumerable items)
        {
            var first = items.Cast<object>().FirstOrDefault();
            if (first != null) SelectedItem = first;
        }
    }

    public double ToolBarHeight { get => (double)GetValue(ToolBarHeightProperty); set => SetValue(ToolBarHeightProperty, value); }
    public static readonly DependencyProperty ToolBarHeightProperty =
        DependencyProperty.Register(nameof(ToolBarHeight), typeof(double), typeof(EverythingToolBar), new PropertyMetadata(48.0));

    public double ItemHeight { get => (double)GetValue(ItemHeightProperty); set => SetValue(ItemHeightProperty, value); }
    public static readonly DependencyProperty ItemHeightProperty =
        DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(EverythingToolBar), new PropertyMetadata(36.0));

    public object ItemsSource { get => GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(EverythingToolBar));

    public DataTemplate ItemTemplate { get => (DataTemplate)GetValue(ItemTemplateProperty); set => SetValue(ItemTemplateProperty, value); }
    public static readonly DependencyProperty ItemTemplateProperty =
        DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(EverythingToolBar));

    public object SelectedItem { get => GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }
    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(EverythingToolBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public ToolBarItemDisplayMode ItemDisplayMode { get => (ToolBarItemDisplayMode)GetValue(ItemDisplayModeProperty); set => SetValue(ItemDisplayModeProperty, value); }
    public static readonly DependencyProperty ItemDisplayModeProperty =
        DependencyProperty.Register(nameof(ItemDisplayMode), typeof(ToolBarItemDisplayMode), typeof(EverythingToolBar),
            new FrameworkPropertyMetadata(ToolBarItemDisplayMode.TextOnly,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));
}
