using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EverythingUI.WPF.Controls;

public class EverythingListViewColumn : DependencyObject
{
    public string Header { get => (string)GetValue(HeaderProperty); set => SetValue(HeaderProperty, value); }
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(string), typeof(EverythingListViewColumn), new PropertyMetadata(string.Empty));

    public string FieldName { get => (string)GetValue(FieldNameProperty); set => SetValue(FieldNameProperty, value); }
    public static readonly DependencyProperty FieldNameProperty =
        DependencyProperty.Register(nameof(FieldName), typeof(string), typeof(EverythingListViewColumn), new PropertyMetadata(string.Empty));

    public double Width { get => (double)GetValue(WidthProperty); set => SetValue(WidthProperty, value); }
    public static readonly DependencyProperty WidthProperty =
        DependencyProperty.Register(nameof(Width), typeof(double), typeof(EverythingListViewColumn), new PropertyMetadata(140.0));

    public HorizontalAlignment HorizontalContentAlignment { get => (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); set => SetValue(HorizontalContentAlignmentProperty, value); }
    public static readonly DependencyProperty HorizontalContentAlignmentProperty =
        DependencyProperty.Register(nameof(HorizontalContentAlignment), typeof(HorizontalAlignment), typeof(EverythingListViewColumn), new PropertyMetadata(HorizontalAlignment.Left));

    public DataTemplate? CellTemplate { get => (DataTemplate?)GetValue(CellTemplateProperty); set => SetValue(CellTemplateProperty, value); }
    public static readonly DependencyProperty CellTemplateProperty =
        DependencyProperty.Register(nameof(CellTemplate), typeof(DataTemplate), typeof(EverythingListViewColumn));

    public EverythingListViewColumn() { }
    public EverythingListViewColumn(string header) => Header = header;
    public EverythingListViewColumn(string header, string fieldName) { Header = header; FieldName = fieldName; }
    public override string ToString() => string.IsNullOrEmpty(Header) ? base.ToString()! : Header;
}
