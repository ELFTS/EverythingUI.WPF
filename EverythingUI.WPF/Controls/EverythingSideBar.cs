using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EverythingUI.WPF.Controls;

public enum SideBarItemDisplayMode
{
    TextOnly,
    IconOnly,
    IconLeft,
    IconTop
}

public class EverythingSideBar : Control
{
    private ListBox? _menuListBox;
    private ScrollViewer? _scrollViewer;
    private ScrollBar? _scrollBar;
    private Grid? _selectionSlider;
    private Border? _sliderBackground;
    private int _lastSelectedIndex = -1;
    private LinearGradientBrush? _cachedSliderBrush;
    private Color _cachedStartColor;
    private Color _cachedEndColor;
    private readonly CubicEase _easeOut = new() { EasingMode = EasingMode.EaseOut };
    private readonly CubicEase _easeIn = new() { EasingMode = EasingMode.EaseIn };

    static EverythingSideBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingSideBar), new FrameworkPropertyMetadata(typeof(EverythingSideBar)));
    }

    public EverythingSideBar()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ColorManager.UpdateColors(this);
        SelectFirstItem();
        Themes.ThemeManager.ColorChanged += OnThemeColorChanged;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Themes.ThemeManager.ColorChanged -= OnThemeColorChanged;
        Loaded -= OnLoaded;
        Unloaded -= OnUnloaded;

        if (_menuListBox != null)
        {
            _menuListBox.ItemContainerGenerator.StatusChanged -= OnItemContainerGeneratorStatusChanged;
            _menuListBox.SelectionChanged -= OnMenuListBoxSelectionChanged;
        }
    }

    private void OnThemeColorChanged(object? sender, ColorName colorName)
    {
        Dispatcher.BeginInvoke(() =>
        {
            _cachedSliderBrush = null;
            UpdateSliderColor();
            if (_menuListBox?.SelectedIndex >= 0)
            {
                UpdateSliderPosition();
            }
        }, System.Windows.Threading.DispatcherPriority.Render);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _menuListBox = GetTemplateChild("menuListBox") as ListBox;
        _scrollViewer = GetTemplateChild("scrollViewer") as ScrollViewer;
        _scrollBar = GetTemplateChild("PART_VerticalScrollBar") as ScrollBar;
        _selectionSlider = GetTemplateChild("selectionSlider") as Grid;
        _sliderBackground = GetTemplateChild("sliderBackground") as Border;

        if (_menuListBox != null)
        {
            _menuListBox.ItemContainerGenerator.StatusChanged += OnItemContainerGeneratorStatusChanged;
            _menuListBox.SelectionChanged += OnMenuListBoxSelectionChanged;
            AttachItemEvents();
            UpdateSliderColor();
        }

        SetupScrollSync();
    }

    private void OnMenuListBoxSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_menuListBox == null || _selectionSlider == null) return;

        var newIndex = _menuListBox.SelectedIndex;

        if (newIndex >= 0 && _menuListBox.ItemContainerGenerator.ContainerFromIndex(newIndex) is ListBoxItem newItem)
        {
            AnimateSliderToItem(newItem);
        }
        else if (newIndex < 0)
        {
            AnimateSliderOpacity(0);
        }

        _lastSelectedIndex = newIndex;
    }

    private void AnimateSliderToItem(ListBoxItem item)
    {
        if (_selectionSlider == null || _menuListBox == null) return;

        var itemTransform = item.TransformToVisual(_menuListBox);
        var itemPosition = itemTransform.Transform(new Point(0, 0));

        double targetTop = itemPosition.Y + 8;
        double targetLeft = itemPosition.X;
        double targetWidth = item.ActualWidth;
        double targetHeight = item.ActualHeight;

        SetSliderGradient();

        _selectionSlider.Width = targetWidth;
        _selectionSlider.Height = targetHeight;

        AnimateSliderOpacity(1);

        var marginAnim = new ThicknessAnimation(
            new Thickness(targetLeft, targetTop, 0, 0),
            TimeSpan.FromSeconds(0.25))
        {
            EasingFunction = _easeOut
        };
        _selectionSlider.BeginAnimation(MarginProperty, marginAnim);
    }

    private void AnimateSliderOpacity(double targetOpacity)
    {
        if (_selectionSlider == null) return;

        var opacityAnim = new DoubleAnimation(targetOpacity, TimeSpan.FromSeconds(0.15))
        {
            EasingFunction = targetOpacity > 0 ? _easeOut : _easeIn
        };
        _selectionSlider.BeginAnimation(UIElement.OpacityProperty, opacityAnim);
    }

    private void SetSliderGradient()
    {
        if (_sliderBackground == null) return;

        var startColor = Application.Current?.Resources["GlobalGradientStartColor"] as Color? ?? ColorHelper.GetGradientStartColor(ColorName.Blue);
        var endColor = Application.Current?.Resources["GlobalGradientEndColor"] as Color? ?? ColorHelper.GetGradientEndColor(ColorName.Blue);

        if (_cachedSliderBrush != null && _cachedStartColor == startColor && _cachedEndColor == endColor)
        {
            _sliderBackground.Background = _cachedSliderBrush;
            return;
        }

        _cachedStartColor = startColor;
        _cachedEndColor = endColor;

        var gradient = new LinearGradientBrush
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(0, 1)
        };

        gradient.GradientStops.Add(new GradientStop(startColor, 0));
        gradient.GradientStops.Add(new GradientStop(endColor, 0.5));
        gradient.GradientStops.Add(new GradientStop(startColor, 1));
        gradient.Freeze();

        _cachedSliderBrush = gradient;
        _sliderBackground.Background = gradient;
    }

    private void UpdateSliderColor()
    {
        _cachedSliderBrush = null;
        SetSliderGradient();
    }

    private void SetupScrollSync()
    {
        if (_scrollViewer == null || _scrollBar == null) return;

        _scrollBar.ValueChanged += (s, e) =>
        {
            _scrollViewer.ScrollToVerticalOffset(_scrollBar.Value);
            UpdateSliderPosition();
        };

        _scrollViewer.ScrollChanged += (s, e) =>
        {
            if (e.VerticalChange != 0)
            {
                _scrollBar.Value = _scrollViewer.VerticalOffset;
                UpdateSliderPosition();
            }
        };
    }

    private void UpdateSliderPosition()
    {
        if (_menuListBox == null || _selectionSlider == null || _menuListBox.SelectedIndex < 0) return;

        if (_menuListBox.ItemContainerGenerator.ContainerFromIndex(_menuListBox.SelectedIndex) is ListBoxItem selectedItem)
        {
            AnimateSliderToItem(selectedItem);
        }
    }

    private void OnItemContainerGeneratorStatusChanged(object? sender, EventArgs e)
    {
        if (_menuListBox?.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
        {
            AttachItemEvents();

            if (_menuListBox.SelectedIndex >= 0 &&
                _menuListBox.ItemContainerGenerator.ContainerFromIndex(_menuListBox.SelectedIndex) is ListBoxItem selectedItem)
            {
                AnimateSliderToItem(selectedItem);
                _selectionSlider?.BeginAnimation(UIElement.OpacityProperty, null);
                if (_selectionSlider != null) _selectionSlider.Opacity = 1;
            }
        }
    }

    private void AttachItemEvents()
    {
        if (_menuListBox == null) return;

        for (int i = 0; i < _menuListBox.Items.Count; i++)
        {
            if (_menuListBox.ItemContainerGenerator.ContainerFromIndex(i) is ListBoxItem item)
            {
                item.MouseEnter -= OnItemMouseEnter;
                item.MouseLeave -= OnItemMouseLeave;

                item.MouseEnter += OnItemMouseEnter;
                item.MouseLeave += OnItemMouseLeave;
            }
        }
    }

    private void OnItemMouseEnter(object sender, System.Windows.Input.MouseEventArgs e) { }
    private void OnItemMouseLeave(object sender, System.Windows.Input.MouseEventArgs e) { }

    private void SelectFirstItem()
    {
        if (SelectedItem == null && ItemsSource is IEnumerable items)
        {
            foreach (var item in items)
            {
                SelectedItem = item;
                break;
            }
        }
    }

    #region 依赖属性

    public double SideBarWidth
    {
        get => (double)GetValue(SideBarWidthProperty);
        set => SetValue(SideBarWidthProperty, value);
    }

    public static readonly DependencyProperty SideBarWidthProperty =
        DependencyProperty.Register(nameof(SideBarWidth), typeof(double), typeof(EverythingSideBar), new PropertyMetadata(250.0));

    public double SideBarHeight
    {
        get => (double)GetValue(SideBarHeightProperty);
        set => SetValue(SideBarHeightProperty, value);
    }

    public static readonly DependencyProperty SideBarHeightProperty =
        DependencyProperty.Register(nameof(SideBarHeight), typeof(double), typeof(EverythingSideBar), new PropertyMetadata(double.NaN));

    public double ItemHeight
    {
        get => (double)GetValue(ItemHeightProperty);
        set => SetValue(ItemHeightProperty, value);
    }

    public static readonly DependencyProperty ItemHeightProperty =
        DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(EverythingSideBar), new PropertyMetadata(44.0));

    public object Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(object), typeof(EverythingSideBar));

    public DataTemplate HeaderTemplate
    {
        get => (DataTemplate)GetValue(HeaderTemplateProperty);
        set => SetValue(HeaderTemplateProperty, value);
    }

    public static readonly DependencyProperty HeaderTemplateProperty =
        DependencyProperty.Register(nameof(HeaderTemplate), typeof(DataTemplate), typeof(EverythingSideBar));

    public object ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(EverythingSideBar));

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    public static readonly DependencyProperty ItemTemplateProperty =
        DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(EverythingSideBar));

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(EverythingSideBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingSideBar),
            new PropertyMetadata(new CornerRadius(0, 16, 16, 0)));

    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(EverythingSideBar));

    public DataTemplate ContentTemplate
    {
        get => (DataTemplate)GetValue(ContentTemplateProperty);
        set => SetValue(ContentTemplateProperty, value);
    }

    public static readonly DependencyProperty ContentTemplateProperty =
        DependencyProperty.Register(nameof(ContentTemplate), typeof(DataTemplate), typeof(EverythingSideBar));

    public SideBarItemDisplayMode ItemDisplayMode
    {
        get => (SideBarItemDisplayMode)GetValue(ItemDisplayModeProperty);
        set => SetValue(ItemDisplayModeProperty, value);
    }

    public static readonly DependencyProperty ItemDisplayModeProperty =
        DependencyProperty.Register(nameof(ItemDisplayMode), typeof(SideBarItemDisplayMode), typeof(EverythingSideBar),
            new PropertyMetadata(SideBarItemDisplayMode.TextOnly));

    public ColorName ColorName
    {
        get => ColorManager.GetColorName(this);
        set => ColorManager.SetColorName(this, value);
    }

    internal Color GradientStartColor
    {
        get => ColorManager.GetGradientStartColor(this);
        set => ColorManager.SetGradientStartColor(this, value);
    }

    internal Color GradientEndColor
    {
        get => ColorManager.GetGradientEndColor(this);
        set => ColorManager.SetGradientEndColor(this, value);
    }

    #endregion
}
