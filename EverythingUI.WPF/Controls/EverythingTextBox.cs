using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public enum TextBoxVariant
{
    Default,
    Filled,
    Outlined
}

public class EverythingTextBox : TextBox
{
    static EverythingTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingTextBox),
            new FrameworkPropertyMetadata(typeof(EverythingTextBox)));
    }

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(EverythingTextBox),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty PlaceholderBrushProperty =
        DependencyProperty.Register(nameof(PlaceholderBrush), typeof(Brush), typeof(EverythingTextBox),
            new PropertyMetadata(Brushes.Gray));

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(EverythingTextBox),
            new PropertyMetadata(null));

    public static readonly DependencyProperty ClearButtonVisibleProperty =
        DependencyProperty.Register(nameof(ClearButtonVisible), typeof(bool), typeof(EverythingTextBox),
            new PropertyMetadata(true));

    public static readonly DependencyProperty TextBoxVariantProperty =
        DependencyProperty.Register(nameof(TextBoxVariant), typeof(TextBoxVariant), typeof(EverythingTextBox),
            new PropertyMetadata(TextBoxVariant.Default));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingTextBox),
            new PropertyMetadata(new CornerRadius(6)));

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public Brush PlaceholderBrush
    {
        get => (Brush)GetValue(PlaceholderBrushProperty);
        set => SetValue(PlaceholderBrushProperty, value);
    }

    public object Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool ClearButtonVisible
    {
        get => (bool)GetValue(ClearButtonVisibleProperty);
        set => SetValue(ClearButtonVisibleProperty, value);
    }

    public TextBoxVariant TextBoxVariant
    {
        get => (TextBoxVariant)GetValue(TextBoxVariantProperty);
        set => SetValue(TextBoxVariantProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}
