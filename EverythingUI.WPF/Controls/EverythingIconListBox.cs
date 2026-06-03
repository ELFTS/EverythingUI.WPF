using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public class EverythingIconListBox : Control
{
    private ListBox? _listBox;
    private DateTime _lastClickTime;
    private object? _lastClickedItem;
    private DataTemplate? _cachedItemTemplate;
    private double _cachedIconSize;
    private double _cachedTextFontSize;
    private double _cachedIconTextSpacing;
    private double _cachedItemWidth;
    private const int DoubleClickTime = 300;

    static EverythingIconListBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingIconListBox), new FrameworkPropertyMetadata(typeof(EverythingIconListBox)));
    }

    public EverythingIconListBox()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ColorManager.UpdateColors(this);
        if (_listBox?.SelectedItem == null && ItemsSource is IEnumerable items && _listBox != null)
        {
            foreach (var item in items)
            {
                _listBox.SelectedItem = item;
                break;
            }
        }
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;
        Unloaded -= OnUnloaded;

        if (_listBox != null)
        {
            _listBox.MouseLeftButtonUp -= OnMouseLeftButtonUp;
            _listBox.MouseRightButtonUp -= OnMouseRightButtonUp;
        }
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _listBox = GetTemplateChild("PART_ListBox") as ListBox;

        if (_listBox != null)
        {
            _listBox.ItemsSource = ItemsSource as IEnumerable;
            _listBox.SelectionChanged += (_, _) =>
            {
                SelectedItem = _listBox.SelectedItem;
                SelectedIndex = _listBox.SelectedIndex;
            };
            _listBox.MouseLeftButtonUp += OnMouseLeftButtonUp;
            _listBox.MouseRightButtonUp += OnMouseRightButtonUp;
            _listBox.ItemTemplate = GetOrCreateItemTemplate();
        }
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (_listBox == null) return;

        var source = e.OriginalSource as DependencyObject;
        var item = GetItemFromVisual(source);
        if (item == null) return;

        var now = DateTime.Now;
        var diff = (now - _lastClickTime).TotalMilliseconds;

        if (diff < DoubleClickTime && item == _lastClickedItem)
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
                if ((DateTime.Now - _lastClickTime).TotalMilliseconds >= DoubleClickTime && _lastClickedItem == capturedItem)
                {
                    ItemClick?.Invoke(this, new(capturedItem));
                }
            }, System.Windows.Threading.DispatcherPriority.Background);
        }
    }

    private void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (_listBox == null) return;

        var source = e.OriginalSource as DependencyObject;
        var item = GetItemFromVisual(source);
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
        {
            return _cachedItemTemplate;
        }

        _cachedIconSize = IconSize;
        _cachedTextFontSize = TextFontSize;
        _cachedIconTextSpacing = IconTextSpacing;
        _cachedItemWidth = ItemWidth;

        var template = new DataTemplate();

        var stackPanel = new FrameworkElementFactory(typeof(StackPanel));
        stackPanel.SetValue(StackPanel.OrientationProperty, Orientation.Vertical);
        stackPanel.SetValue(StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Center);
        stackPanel.SetValue(StackPanel.VerticalAlignmentProperty, VerticalAlignment.Center);

        var image = new FrameworkElementFactory(typeof(Image));
        image.SetBinding(Image.SourceProperty, new Binding("Icon"));
        image.SetValue(Image.WidthProperty, IconSize);
        image.SetValue(Image.HeightProperty, IconSize);
        image.SetValue(Image.StretchProperty, Stretch.Uniform);

        var text = new FrameworkElementFactory(typeof(TextBlock));
        text.SetBinding(TextBlock.TextProperty, new Binding("Text"));
        text.SetValue(TextBlock.FontSizeProperty, TextFontSize);
        text.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
        text.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
        text.SetValue(TextBlock.MarginProperty, new Thickness(4, IconTextSpacing, 4, 0));
        text.SetValue(TextBlock.MaxWidthProperty, ItemWidth - 16);

        stackPanel.AppendChild(image);
        stackPanel.AppendChild(text);
        template.VisualTree = stackPanel;

        _cachedItemTemplate = template;
        return template;
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (_listBox != null && (e.Property == IconSizeProperty || e.Property == TextFontSizeProperty ||
            e.Property == IconTextSpacingProperty || e.Property == ItemWidthProperty))
        {
            _listBox.ItemTemplate = GetOrCreateItemTemplate();
        }
    }

    #region Events

    public event EventHandler<EverythingIconListBoxItemClickEventArgs>? ItemClick;
    public event EventHandler<EverythingIconListBoxItemClickEventArgs>? ItemDoubleClick;
    public event EventHandler<EverythingIconListBoxItemClickEventArgs>? ItemRightClick;

    #endregion

    #region Dependency Properties

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(EverythingIconListBox),
            new PropertyMetadata(null, (d, e) =>
            {
                if (d is EverythingIconListBox { _listBox: not null } control)
                    control._listBox.ItemsSource = e.NewValue as IEnumerable;
            }));

    public object ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(EverythingIconListBox),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly DependencyProperty SelectedIndexProperty =
        DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(EverythingIconListBox),
            new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    public static readonly DependencyProperty ItemWidthProperty =
        DependencyProperty.Register(nameof(ItemWidth), typeof(double), typeof(EverythingIconListBox),
            new PropertyMetadata(80.0));

    public double ItemWidth
    {
        get => (double)GetValue(ItemWidthProperty);
        set => SetValue(ItemWidthProperty, value);
    }

    public static readonly DependencyProperty ItemHeightProperty =
        DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(EverythingIconListBox),
            new PropertyMetadata(80.0));

    public double ItemHeight
    {
        get => (double)GetValue(ItemHeightProperty);
        set => SetValue(ItemHeightProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingIconListBox),
            new PropertyMetadata(new CornerRadius(12)));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty ItemCornerRadiusProperty =
        DependencyProperty.Register(nameof(ItemCornerRadius), typeof(CornerRadius), typeof(EverythingIconListBox),
            new PropertyMetadata(new CornerRadius(8)));

    public CornerRadius ItemCornerRadius
    {
        get => (CornerRadius)GetValue(ItemCornerRadiusProperty);
        set => SetValue(ItemCornerRadiusProperty, value);
    }

    public static readonly DependencyProperty IconTextSpacingProperty =
        DependencyProperty.Register(nameof(IconTextSpacing), typeof(double), typeof(EverythingIconListBox),
            new PropertyMetadata(6.0));

    public double IconTextSpacing
    {
        get => (double)GetValue(IconTextSpacingProperty);
        set => SetValue(IconTextSpacingProperty, value);
    }

    public static readonly DependencyProperty TextFontSizeProperty =
        DependencyProperty.Register(nameof(TextFontSize), typeof(double), typeof(EverythingIconListBox),
            new PropertyMetadata(12.0));

    public double TextFontSize
    {
        get => (double)GetValue(TextFontSizeProperty);
        set => SetValue(TextFontSizeProperty, value);
    }

    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(EverythingIconListBox),
            new PropertyMetadata(28.0));

    public double IconSize
    {
        get => (double)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

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
