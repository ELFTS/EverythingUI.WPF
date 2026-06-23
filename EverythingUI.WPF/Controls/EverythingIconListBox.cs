using System.Collections;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EverythingUI.WPF.Controls;

public class EverythingIconListBox : Control
{
    private ListBox? _listBox;
    private Grid? _selectionIndicator;
    private Border? _indicatorBackground;
    private ScrollViewer? _scrollViewer;

    private DateTime _lastClickTime;
    private object? _lastClickedItem;

    private DataTemplate? _cachedItemTemplate;
    private double _cachedIconSize, _cachedTextFontSize, _cachedIconTextSpacing, _cachedItemWidth;

    private readonly CubicEase _easeOut = new() { EasingMode = EasingMode.EaseOut };
    private static readonly int DoubleClickTime = (int)GetDoubleClickTime();

    private ThicknessAnimation? _cachedMarginAnimation;
    private DoubleAnimation? _cachedOpacityAnimation;

    [DllImport("user32.dll")]
    private static extern uint GetDoubleClickTime();

    static EverythingIconListBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingIconListBox), new FrameworkPropertyMetadata(typeof(EverythingIconListBox)));
    }

    public EverythingIconListBox()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        DetachListBox();

        _listBox = GetTemplateChild("PART_ListBox") as ListBox;
        _scrollViewer = GetTemplateChild("PART_ScrollViewer") as ScrollViewer;
        _selectionIndicator = GetTemplateChild("selectionIndicator") as Grid;
        _indicatorBackground = GetTemplateChild("indicatorBackground") as Border;

        if (_listBox != null)
        {
            AttachListBox();
            _listBox.ItemTemplate = GetOrCreateItemTemplate();
        }

        UpdateIndicatorColor();
    }

    private void AttachListBox()
    {
        _listBox!.SelectionChanged += OnListBoxSelectionChanged;
        _listBox.ItemContainerGenerator.StatusChanged += OnContainerStatusChanged;
        _listBox.MouseLeftButtonUp += OnListBoxMouseLeftButtonUp;
        _listBox.MouseRightButtonUp += OnListBoxMouseRightButtonUp;
        if (_scrollViewer != null)
            _scrollViewer.ScrollChanged += OnScrollChanged;
    }

    private void DetachListBox()
    {
        if (_listBox != null)
        {
            _listBox.SelectionChanged -= OnListBoxSelectionChanged;
            _listBox.ItemContainerGenerator.StatusChanged -= OnContainerStatusChanged;
            _listBox.MouseLeftButtonUp -= OnListBoxMouseLeftButtonUp;
            _listBox.MouseRightButtonUp -= OnListBoxMouseRightButtonUp;
        }
        if (_scrollViewer != null)
            _scrollViewer.ScrollChanged -= OnScrollChanged;
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
        _selectionIndicator?.BeginAnimation(MarginProperty, null);
        _selectionIndicator?.BeginAnimation(UIElement.OpacityProperty, null);
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

    private void SelectFirstItem()
    {
        if (SelectedItem == null && ItemsSource is IEnumerable items)
            SelectedItem = items.Cast<object>().FirstOrDefault();
    }

    private void RestoreIndicatorPosition()
    {
        if (_listBox == null || _selectionIndicator == null) return;

        if (_listBox.SelectedIndex >= 0 &&
            _listBox.ItemContainerGenerator.ContainerFromIndex(_listBox.SelectedIndex) is ListBoxItem item)
            SyncIndicatorToItem(item);
        else if (_listBox.SelectedIndex < 0)
            _selectionIndicator.Opacity = 0;
    }

    private void OnListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_listBox == null) return;
        SelectedIndex = _listBox.SelectedIndex;

        if (_listBox.SelectedIndex >= 0 &&
            _listBox.ItemContainerGenerator.ContainerFromIndex(_listBox.SelectedIndex) is ListBoxItem item)
            AnimateIndicatorToItem(item);
        else
            AnimateIndicatorOpacity(0);
    }

    private void OnContainerStatusChanged(object? sender, EventArgs e)
    {
        if (_listBox?.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
            return;
        RestoreIndicatorPosition();
    }

    private void ApplyIndicatorSizeAndMargin(double left, double top, double width, double height)
    {
        _selectionIndicator!.Width = width;
        _selectionIndicator.Height = height;
        _selectionIndicator.Margin = new Thickness(left, top, 0, 0);
    }

    private void SyncIndicatorToItem(ListBoxItem item)
    {
        if (_selectionIndicator == null || _listBox == null) return;
        var pos = GetItemPositionInListBox(item);
        if (!pos.HasValue) return;

        _selectionIndicator.BeginAnimation(MarginProperty, null);
        _selectionIndicator.BeginAnimation(UIElement.OpacityProperty, null);
        ApplyIndicatorSizeAndMargin(pos.Value.left, pos.Value.top, pos.Value.width, pos.Value.height);
        _selectionIndicator.Opacity = 1;
    }

    private void AnimateIndicatorToItem(ListBoxItem item)
    {
        if (_selectionIndicator == null || _listBox == null) return;
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

    private void OnScrollChanged(object sender, ScrollChangedEventArgs e) => SyncIndicatorPosition();

    private void SyncIndicatorPosition()
    {
        if (_listBox == null || _selectionIndicator == null || _listBox.SelectedIndex < 0) return;
        if (_listBox.ItemContainerGenerator.ContainerFromIndex(_listBox.SelectedIndex) is not ListBoxItem item) return;

        var pos = GetItemPositionInListBox(item);
        if (!pos.HasValue) return;

        _selectionIndicator.BeginAnimation(MarginProperty, null);
        ApplyIndicatorSizeAndMargin(pos.Value.left, pos.Value.top, pos.Value.width, pos.Value.height);
    }

    private (double left, double top, double width, double height)? GetItemPositionInListBox(ListBoxItem item)
    {
        if (_listBox == null) return null;
        try
        {
            var transform = item.TransformToVisual(_listBox);
            var pos = transform.Transform(new Point(0, 0));
            return (pos.X, pos.Y, item.ActualWidth, item.ActualHeight);
        }
        catch (InvalidOperationException) { return null; }
    }

    private void UpdateIndicatorColor()
    {
        _indicatorBackground?.ClearValue(Border.BackgroundProperty);
    }

    private void OnListBoxMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (_listBox == null) return;
        var item = GetItemFromVisual(e.OriginalSource as DependencyObject);
        if (item == null) return;

        var now = DateTime.Now;
        var diff = (now - _lastClickTime).TotalMilliseconds;

        if (diff < DoubleClickTime && ReferenceEquals(item, _lastClickedItem))
        {
            ItemDoubleClick?.Invoke(this, new(item));
            _lastClickTime = DateTime.MinValue;
            _lastClickedItem = null;
        }
        else
        {
            _lastClickTime = now;
            _lastClickedItem = item;
            var capturedItem = item;
            Dispatcher.BeginInvoke(() =>
            {
                if ((DateTime.Now - _lastClickTime).TotalMilliseconds >= DoubleClickTime && ReferenceEquals(_lastClickedItem, capturedItem))
                    ItemClick?.Invoke(this, new(capturedItem));
            }, System.Windows.Threading.DispatcherPriority.Background);
        }
    }

    private void OnListBoxMouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (_listBox == null) return;
        var item = GetItemFromVisual(e.OriginalSource as DependencyObject);
        if (item == null) return;
        ItemRightClick?.Invoke(this, new(item, e));
    }

    private static object? GetItemFromVisual(DependencyObject? visual)
    {
        while (visual != null)
        {
            if (visual is FrameworkElement { DataContext: EverythingIconListBoxItem item })
                return item;
            visual = VisualTreeHelper.GetParent(visual);
        }
        return null;
    }

    private DataTemplate GetOrCreateItemTemplate()
    {
        if (_cachedItemTemplate != null &&
            _cachedIconSize == IconSize &&
            _cachedTextFontSize == TextFontSize &&
            _cachedIconTextSpacing == IconTextSpacing &&
            _cachedItemWidth == ItemWidth)
            return _cachedItemTemplate;

        _cachedIconSize = IconSize;
        _cachedTextFontSize = TextFontSize;
        _cachedIconTextSpacing = IconTextSpacing;
        _cachedItemWidth = ItemWidth;

        var sp = new FrameworkElementFactory(typeof(StackPanel));
        sp.SetValue(StackPanel.OrientationProperty, Orientation.Vertical);
        sp.SetValue(StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Center);
        sp.SetValue(StackPanel.VerticalAlignmentProperty, VerticalAlignment.Center);

        var img = new FrameworkElementFactory(typeof(Image));
        img.SetBinding(Image.SourceProperty, new Binding("Icon"));
        img.SetValue(Image.WidthProperty, IconSize);
        img.SetValue(Image.HeightProperty, IconSize);
        img.SetValue(Image.StretchProperty, Stretch.Uniform);

        var txt = new FrameworkElementFactory(typeof(TextBlock));
        txt.SetBinding(TextBlock.TextProperty, new Binding("Text"));
        txt.SetValue(TextBlock.FontSizeProperty, TextFontSize);
        txt.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
        txt.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
        txt.SetValue(TextBlock.MarginProperty, new Thickness(4, IconTextSpacing, 4, 0));
        txt.SetValue(TextBlock.MaxWidthProperty, ItemWidth - 16);

        sp.AppendChild(img);
        sp.AppendChild(txt);

        var template = new DataTemplate { VisualTree = sp };
        _cachedItemTemplate = template;
        return template;
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (_listBox != null && (e.Property == IconSizeProperty || e.Property == TextFontSizeProperty ||
            e.Property == IconTextSpacingProperty || e.Property == ItemWidthProperty))
            _listBox.ItemTemplate = GetOrCreateItemTemplate();
    }

    public event EventHandler<EverythingIconListBoxItemClickEventArgs>? ItemClick;
    public event EventHandler<EverythingIconListBoxItemClickEventArgs>? ItemDoubleClick;
    public event EventHandler<EverythingIconListBoxItemClickEventArgs>? ItemRightClick;

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(EverythingIconListBox),
            new PropertyMetadata(null, (d, e) =>
            {
                if (d is EverythingIconListBox { _listBox: not null } control)
                    control._listBox.ItemsSource = e.NewValue as IEnumerable;
            }));

    public object ItemsSource { get => GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(EverythingIconListBox),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public object SelectedItem { get => GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }

    public static readonly DependencyProperty SelectedIndexProperty =
        DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(EverythingIconListBox),
            new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public int SelectedIndex { get => (int)GetValue(SelectedIndexProperty); set => SetValue(SelectedIndexProperty, value); }

    public static readonly DependencyProperty ItemWidthProperty =
        DependencyProperty.Register(nameof(ItemWidth), typeof(double), typeof(EverythingIconListBox),
            new FrameworkPropertyMetadata(80.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public double ItemWidth { get => (double)GetValue(ItemWidthProperty); set => SetValue(ItemWidthProperty, value); }

    public static readonly DependencyProperty ItemHeightProperty =
        DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(EverythingIconListBox),
            new FrameworkPropertyMetadata(80.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public double ItemHeight { get => (double)GetValue(ItemHeightProperty); set => SetValue(ItemHeightProperty, value); }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingIconListBox),
            new FrameworkPropertyMetadata(new CornerRadius(12), FrameworkPropertyMetadataOptions.AffectsRender));

    public CornerRadius CornerRadius { get => (CornerRadius)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }

    public static readonly DependencyProperty ItemCornerRadiusProperty =
        DependencyProperty.Register(nameof(ItemCornerRadius), typeof(CornerRadius), typeof(EverythingIconListBox),
            new FrameworkPropertyMetadata(new CornerRadius(8), FrameworkPropertyMetadataOptions.AffectsRender));

    public CornerRadius ItemCornerRadius { get => (CornerRadius)GetValue(ItemCornerRadiusProperty); set => SetValue(ItemCornerRadiusProperty, value); }

    public static readonly DependencyProperty IconTextSpacingProperty =
        DependencyProperty.Register(nameof(IconTextSpacing), typeof(double), typeof(EverythingIconListBox),
            new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public double IconTextSpacing { get => (double)GetValue(IconTextSpacingProperty); set => SetValue(IconTextSpacingProperty, value); }

    public static readonly DependencyProperty TextFontSizeProperty =
        DependencyProperty.Register(nameof(TextFontSize), typeof(double), typeof(EverythingIconListBox),
            new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public double TextFontSize { get => (double)GetValue(TextFontSizeProperty); set => SetValue(TextFontSizeProperty, value); }

    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(EverythingIconListBox),
            new FrameworkPropertyMetadata(28.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public double IconSize { get => (double)GetValue(IconSizeProperty); set => SetValue(IconSizeProperty, value); }

    public ColorName ColorName { get => ColorManager.GetColorName(this); set => ColorManager.SetColorName(this, value); }
    internal Color GradientStartColor { get => ColorManager.GetGradientStartColor(this); set => ColorManager.SetGradientStartColor(this, value); }
    internal Color GradientEndColor { get => ColorManager.GetGradientEndColor(this); set => ColorManager.SetGradientEndColor(this, value); }
}
