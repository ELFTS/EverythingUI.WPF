using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EverythingUI.WPF.Controls;

public class EverythingListView : Control
{
    private ListView? _listView;
    private ScrollViewer? _scrollViewer;
    private GridView? _gridView;

    private DateTime _lastClickTime;
    private object? _lastClickedItem;

    private static readonly int DoubleClickTime = (int)GetDoubleClickTime();

    [DllImport("user32.dll")]
    private static extern uint GetDoubleClickTime();

    static EverythingListView()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingListView), new FrameworkPropertyMetadata(typeof(EverythingListView)));
    }

    public EverythingListView()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        _columns.CollectionChanged += OnColumnsCollectionChanged;
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        DetachListBox();

        _listView = GetTemplateChild("PART_ListView") as ListView;
        _scrollViewer = GetTemplateChild("PART_ScrollViewer") as ScrollViewer;

        if (_listView != null)
        {
            AttachListBox();
            ApplyColumns();
            if (ItemsSource is IEnumerable items)
                _listView.ItemsSource = items;
        }
    }

    private void AttachListBox()
    {
        _listView!.SelectionChanged += OnListViewSelectionChanged;
        _listView.MouseLeftButtonUp += OnListViewMouseLeftButtonUp;
        _listView.MouseRightButtonUp += OnListViewMouseRightButtonUp;
    }

    private void DetachListBox()
    {
        if (_listView != null)
        {
            _listView.SelectionChanged -= OnListViewSelectionChanged;
            _listView.MouseLeftButtonUp -= OnListViewMouseLeftButtonUp;
            _listView.MouseRightButtonUp -= OnListViewMouseRightButtonUp;
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        SelectFirstItem();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
    }

    private void SelectFirstItem()
    {
        if (SelectedItem == null && ItemsSource is IEnumerable items)
        {
            var first = items.Cast<object>().FirstOrDefault();
            if (first != null) SelectedItem = first;
        }
    }

    private void OnListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_listView == null) return;
        SelectedIndex = _listView.SelectedIndex;
        if (_listView.SelectedItem != null)
            SelectedItem = _listView.SelectedItem;
    }

    private void OnListViewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (_listView == null) return;
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

    private void OnListViewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (_listView == null) return;
        var item = GetItemFromVisual(e.OriginalSource as DependencyObject);
        if (item == null) return;
        ItemRightClick?.Invoke(this, new(item, e));
    }

    private static object? GetItemFromVisual(DependencyObject? visual)
    {
        var lvi = FindListViewItem(visual);
        if (lvi != null)
            return lvi.Content ?? lvi.DataContext;
        return null;
    }

    private static ListViewItem? FindListViewItem(DependencyObject? visual)
    {
        while (visual != null)
        {
            if (visual is ListViewItem lvi) return lvi;
            visual = VisualTreeHelper.GetParent(visual);
        }
        return null;
    }

    private void OnColumnsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => ApplyColumns();

    private void ApplyColumns()
    {
        if (_listView == null) return;

        if (_columns.Count > 0)
        {
            _gridView = new GridView();
            foreach (var col in _columns)
            {
                var gvc = new GridViewColumn { Header = col.Header, Width = col.Width };
                if (col.CellTemplate != null)
                    gvc.CellTemplate = col.CellTemplate;
                else
                    gvc.CellTemplate = CreateDefaultCellTemplate(col);
                _gridView.Columns.Add(gvc);
            }
            var headerStyleKey = ShowHeader ? "EverythingListViewHeaderStyle" : "EverythingListViewHeaderHiddenStyle";
            if (TryFindResource(headerStyleKey) is Style headerStyle)
                _gridView.ColumnHeaderContainerStyle = headerStyle;
            _listView.View = _gridView;
            _listView.ItemTemplate = null;
        }
        else
        {
            _listView.View = null;
            _listView.ItemTemplate = GetDefaultItemTemplate();
        }
    }

    private DataTemplate CreateDefaultCellTemplate(EverythingListViewColumn col)
    {
        var tb = new FrameworkElementFactory(typeof(TextBlock));
        if (!string.IsNullOrEmpty(col.FieldName))
            tb.SetBinding(TextBlock.TextProperty, new Binding(col.FieldName));
        tb.SetValue(TextBlock.FontSizeProperty, TextFontSize);
        tb.SetValue(TextBlock.HorizontalAlignmentProperty, col.HorizontalContentAlignment);
        tb.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
        tb.SetValue(TextBlock.PaddingProperty, new Thickness(12, 0, 12, 0));
        tb.SetValue(TextBlock.TextTrimmingProperty, TextTrimming.CharacterEllipsis);
        return new DataTemplate { VisualTree = tb };
    }

    private DataTemplate GetDefaultItemTemplate()
    {
        var sp = new FrameworkElementFactory(typeof(StackPanel));
        sp.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
        sp.SetValue(StackPanel.VerticalAlignmentProperty, VerticalAlignment.Center);

        var img = new FrameworkElementFactory(typeof(Image));
        img.SetBinding(Image.SourceProperty, new Binding("Icon"));
        img.SetValue(Image.WidthProperty, IconSize);
        img.SetValue(Image.HeightProperty, IconSize);
        img.SetValue(Image.StretchProperty, Stretch.Uniform);
        img.SetValue(Image.VerticalAlignmentProperty, VerticalAlignment.Center);
        img.SetValue(Image.MarginProperty, new Thickness(12, 0, 10, 0));

        var txt = new FrameworkElementFactory(typeof(TextBlock));
        txt.SetBinding(TextBlock.TextProperty, new Binding("Text"));
        txt.SetValue(TextBlock.FontSizeProperty, TextFontSize);
        txt.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
        txt.SetValue(TextBlock.TextTrimmingProperty, TextTrimming.CharacterEllipsis);

        sp.AppendChild(img);
        sp.AppendChild(txt);

        return new DataTemplate { VisualTree = sp };
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (_listView == null) return;
        if ((e.Property == IconSizeProperty || e.Property == TextFontSizeProperty) && _columns.Count == 0)
            _listView.ItemTemplate = GetDefaultItemTemplate();
        else if (e.Property == ShowHeaderProperty && _gridView != null)
            ApplyColumns();
    }

    public event EventHandler<EverythingListViewItemEventArgs>? ItemClick;
    public event EventHandler<EverythingListViewItemEventArgs>? ItemDoubleClick;
    public event EventHandler<EverythingListViewItemEventArgs>? ItemRightClick;

    private readonly ObservableCollection<EverythingListViewColumn> _columns = new();
    public ObservableCollection<EverythingListViewColumn> Columns => _columns;

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(EverythingListView),
            new PropertyMetadata(null, (d, e) =>
            {
                if (d is EverythingListView { _listView: not null } control)
                    control._listView.ItemsSource = e.NewValue as IEnumerable;
            }));

    public object ItemsSource { get => GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(EverythingListView),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
            {
                if (d is EverythingListView { _listView: not null } control && !Equals(control._listView.SelectedItem, e.NewValue))
                    control._listView.SelectedItem = e.NewValue;
            }));

    public object SelectedItem { get => GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }

    public static readonly DependencyProperty SelectedIndexProperty =
        DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(EverythingListView),
            new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
            {
                if (d is EverythingListView { _listView: not null } control && control._listView.SelectedIndex != (int)e.NewValue)
                    control._listView.SelectedIndex = (int)e.NewValue;
            }));

    public int SelectedIndex { get => (int)GetValue(SelectedIndexProperty); set => SetValue(SelectedIndexProperty, value); }

    public static readonly DependencyProperty ItemHeightProperty =
        DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(EverythingListView),
            new FrameworkPropertyMetadata(44.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public double ItemHeight { get => (double)GetValue(ItemHeightProperty); set => SetValue(ItemHeightProperty, value); }

    public static readonly DependencyProperty ShowHeaderProperty =
        DependencyProperty.Register(nameof(ShowHeader), typeof(bool), typeof(EverythingListView),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

    public bool ShowHeader { get => (bool)GetValue(ShowHeaderProperty); set => SetValue(ShowHeaderProperty, value); }

    public static readonly DependencyProperty ShowGridLinesProperty =
        DependencyProperty.Register(nameof(ShowGridLines), typeof(bool), typeof(EverythingListView),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

    public bool ShowGridLines { get => (bool)GetValue(ShowGridLinesProperty); set => SetValue(ShowGridLinesProperty, value); }

    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(EverythingListView),
            new FrameworkPropertyMetadata(20.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public double IconSize { get => (double)GetValue(IconSizeProperty); set => SetValue(IconSizeProperty, value); }

    public static readonly DependencyProperty TextFontSizeProperty =
        DependencyProperty.Register(nameof(TextFontSize), typeof(double), typeof(EverythingListView),
            new FrameworkPropertyMetadata(13.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public double TextFontSize { get => (double)GetValue(TextFontSizeProperty); set => SetValue(TextFontSizeProperty, value); }

    public static readonly DependencyProperty HeaderBackgroundProperty =
        DependencyProperty.Register(nameof(HeaderBackground), typeof(Brush), typeof(EverythingListView),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    public Brush HeaderBackground { get => (Brush)GetValue(HeaderBackgroundProperty); set => SetValue(HeaderBackgroundProperty, value); }
}
