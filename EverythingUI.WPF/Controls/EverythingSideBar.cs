using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EverythingUI.WPF.Controls;

public enum SideBarItemDisplayMode { TextOnly, IconOnly, IconLeft, IconTop }

public class EverythingSideBar : Control
{
    private ListBox? _menuListBox;
    private ScrollViewer? _scrollViewer;
    private Grid? _selectionSlider;
    private Border? _sliderBackground;
    private readonly CubicEase _easeOut = new() { EasingMode = EasingMode.EaseOut };
    private readonly CubicEase _easeIn = new() { EasingMode = EasingMode.EaseIn };
    private ThicknessAnimation? _cachedMarginAnimation;
    private DoubleAnimation? _cachedOpacityAnimation;

    static EverythingSideBar() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingSideBar), new FrameworkPropertyMetadata(typeof(EverythingSideBar)));

    public EverythingSideBar()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        SelectFirstItem();
        Themes.ThemeManager.ColorChanged -= OnThemeColorChanged;
        Themes.ThemeManager.ColorChanged += OnThemeColorChanged;
        AttachTemplateEvents();
        Dispatcher.BeginInvoke(UpdateSliderPosition, System.Windows.Threading.DispatcherPriority.Render);
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Themes.ThemeManager.ColorChanged -= OnThemeColorChanged;
        DetachTemplateEvents();
    }

    private void OnThemeColorChanged(object? sender, ColorName colorName)
    {
        Dispatcher.BeginInvoke(() =>
        {
            _sliderBackground?.ClearValue(Border.BackgroundProperty);
            if (_menuListBox?.SelectedIndex >= 0) UpdateSliderPosition();
        }, System.Windows.Threading.DispatcherPriority.Render);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        DetachTemplateEvents();
        _menuListBox = GetTemplateChild("menuListBox") as ListBox;
        _scrollViewer = GetTemplateChild("scrollViewer") as ScrollViewer;
        _selectionSlider = GetTemplateChild("selectionSlider") as Grid;
        _sliderBackground = GetTemplateChild("sliderBackground") as Border;

        AttachTemplateEvents();
        SetupScrollSync();
    }

    private void AttachTemplateEvents()
    {
        if (_menuListBox == null) return;

        _menuListBox.ItemContainerGenerator.StatusChanged -= OnItemContainerGeneratorStatusChanged;
        _menuListBox.SelectionChanged -= OnMenuListBoxSelectionChanged;
        _menuListBox.PreviewMouseWheel -= OnMenuListBoxPreviewMouseWheel;
        _menuListBox.ItemContainerGenerator.StatusChanged += OnItemContainerGeneratorStatusChanged;
        _menuListBox.SelectionChanged += OnMenuListBoxSelectionChanged;
        _menuListBox.PreviewMouseWheel += OnMenuListBoxPreviewMouseWheel;
    }

    private void DetachTemplateEvents()
    {
        if (_menuListBox == null) return;

        _menuListBox.ItemContainerGenerator.StatusChanged -= OnItemContainerGeneratorStatusChanged;
        _menuListBox.SelectionChanged -= OnMenuListBoxSelectionChanged;
        _menuListBox.PreviewMouseWheel -= OnMenuListBoxPreviewMouseWheel;
    }

    private void OnMenuListBoxSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_menuListBox == null || _selectionSlider == null) return;
        var newIndex = _menuListBox.SelectedIndex;
        if (newIndex >= 0 && _menuListBox.ItemContainerGenerator.ContainerFromIndex(newIndex) is ListBoxItem newItem)
            AnimateSliderToItem(newItem);
        else if (newIndex < 0)
            AnimateSliderOpacity(0);
    }

    private void AnimateSliderToItem(ListBoxItem item)
    {
        if (_selectionSlider == null || _menuListBox == null) return;
        var itemTransform = item.TransformToVisual(_menuListBox);
        var itemPosition = itemTransform.Transform(new Point(0, 0));
        double targetTop = itemPosition.Y + 8, targetLeft = itemPosition.X,
               targetWidth = item.ActualWidth, targetHeight = item.ActualHeight;
        _sliderBackground?.ClearValue(Border.BackgroundProperty);
        _selectionSlider.Width = targetWidth;
        _selectionSlider.Height = targetHeight;
        AnimateSliderOpacity(1);
        _cachedMarginAnimation ??= new ThicknessAnimation { Duration = TimeSpan.FromSeconds(0.25), EasingFunction = _easeOut };
        _cachedMarginAnimation.To = new Thickness(targetLeft, targetTop, 0, 0);
        _selectionSlider.BeginAnimation(MarginProperty, _cachedMarginAnimation);
    }

    private void AnimateSliderOpacity(double targetOpacity)
    {
        if (_selectionSlider == null) return;
        _cachedOpacityAnimation ??= new DoubleAnimation { Duration = TimeSpan.FromSeconds(0.15) };
        _cachedOpacityAnimation.To = targetOpacity;
        _cachedOpacityAnimation.EasingFunction = targetOpacity > 0 ? _easeOut : _easeIn;
        _selectionSlider.BeginAnimation(UIElement.OpacityProperty, _cachedOpacityAnimation);
    }

    private void SetupScrollSync()
    {
        if (_scrollViewer == null) return;
        _scrollViewer.ScrollChanged += (s, e) =>
        {
            if (e.VerticalChange != 0) UpdateSliderPosition();
        };
    }

    private void OnMenuListBoxPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (_scrollViewer == null)
            return;

        _scrollViewer.ScrollToVerticalOffset(_scrollViewer.VerticalOffset - e.Delta);
        e.Handled = true;
    }

    private void UpdateSliderPosition()
    {
        if (_menuListBox == null || _selectionSlider == null || _menuListBox.SelectedIndex < 0) return;
        if (_menuListBox.ItemContainerGenerator.ContainerFromIndex(_menuListBox.SelectedIndex) is ListBoxItem selectedItem)
            AnimateSliderToItem(selectedItem);
    }

    private void OnItemContainerGeneratorStatusChanged(object? sender, EventArgs e)
    {
        if (_menuListBox?.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated) return;
        if (_menuListBox.SelectedIndex >= 0 &&
            _menuListBox.ItemContainerGenerator.ContainerFromIndex(_menuListBox.SelectedIndex) is ListBoxItem selectedItem)
        {
            AnimateSliderToItem(selectedItem);
            _selectionSlider?.BeginAnimation(UIElement.OpacityProperty, null);
            if (_selectionSlider != null) _selectionSlider.Opacity = 1;
        }
    }

    private void SelectFirstItem()
    {
        if (SelectedItem == null && ItemsSource is IEnumerable items)
        {
            var first = items.Cast<object>().FirstOrDefault();
            if (first != null) SelectedItem = first;
        }
    }

    public double SideBarWidth { get => (double)GetValue(SideBarWidthProperty); set => SetValue(SideBarWidthProperty, value); }
    public static readonly DependencyProperty SideBarWidthProperty =
        DependencyProperty.Register(nameof(SideBarWidth), typeof(double), typeof(EverythingSideBar),
            new FrameworkPropertyMetadata(250.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public double SideBarHeight { get => (double)GetValue(SideBarHeightProperty); set => SetValue(SideBarHeightProperty, value); }
    public static readonly DependencyProperty SideBarHeightProperty =
        DependencyProperty.Register(nameof(SideBarHeight), typeof(double), typeof(EverythingSideBar),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public double ItemHeight { get => (double)GetValue(ItemHeightProperty); set => SetValue(ItemHeightProperty, value); }
    public static readonly DependencyProperty ItemHeightProperty =
        DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(EverythingSideBar),
            new FrameworkPropertyMetadata(44.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public object Header { get => GetValue(HeaderProperty); set => SetValue(HeaderProperty, value); }
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(object), typeof(EverythingSideBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public DataTemplate HeaderTemplate { get => (DataTemplate)GetValue(HeaderTemplateProperty); set => SetValue(HeaderTemplateProperty, value); }
    public static readonly DependencyProperty HeaderTemplateProperty =
        DependencyProperty.Register(nameof(HeaderTemplate), typeof(DataTemplate), typeof(EverythingSideBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public object ItemsSource { get => GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(EverythingSideBar), new FrameworkPropertyMetadata(null));

    public DataTemplate ItemTemplate { get => (DataTemplate)GetValue(ItemTemplateProperty); set => SetValue(ItemTemplateProperty, value); }
    public static readonly DependencyProperty ItemTemplateProperty =
        DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(EverythingSideBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public object SelectedItem { get => GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }
    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(EverythingSideBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public CornerRadius CornerRadius { get => (CornerRadius)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingSideBar),
            new FrameworkPropertyMetadata(new CornerRadius(0, 16, 16, 0), FrameworkPropertyMetadataOptions.AffectsRender));

    public object Content { get => GetValue(ContentProperty); set => SetValue(ContentProperty, value); }
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(EverythingSideBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public DataTemplate ContentTemplate { get => (DataTemplate)GetValue(ContentTemplateProperty); set => SetValue(ContentTemplateProperty, value); }
    public static readonly DependencyProperty ContentTemplateProperty =
        DependencyProperty.Register(nameof(ContentTemplate), typeof(DataTemplate), typeof(EverythingSideBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public SideBarItemDisplayMode ItemDisplayMode { get => (SideBarItemDisplayMode)GetValue(ItemDisplayModeProperty); set => SetValue(ItemDisplayModeProperty, value); }
    public static readonly DependencyProperty ItemDisplayModeProperty =
        DependencyProperty.Register(nameof(ItemDisplayMode), typeof(SideBarItemDisplayMode), typeof(EverythingSideBar),
            new FrameworkPropertyMetadata(SideBarItemDisplayMode.TextOnly, FrameworkPropertyMetadataOptions.AffectsMeasure));
}
