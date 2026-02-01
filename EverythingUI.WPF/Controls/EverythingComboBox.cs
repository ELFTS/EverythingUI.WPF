using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EverythingUI.WPF.Controls;

public class EverythingComboBox : ComboBox
{
    private Border? _dropDownBorder;

    static EverythingComboBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingComboBox),
            new FrameworkPropertyMetadata(typeof(EverythingComboBox)));
    }

    public EverythingComboBox()
    {
        Loaded += OnLoaded;
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _dropDownBorder = GetTemplateChild("dropDownBorder") as Border;
    }

    protected override void OnDropDownOpened(EventArgs e)
    {
        base.OnDropDownOpened(e);
        AnimateDropDownOpen();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // 初始动画
        var scaleTransform = new ScaleTransform(1, 1);
        RenderTransform = scaleTransform;
        RenderTransformOrigin = new Point(0.5, 0.5);

        var scaleAnim = new DoubleAnimation(0.95, 1, TimeSpan.FromSeconds(0.3))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnim);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnim);

        var opacityAnim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        BeginAnimation(OpacityProperty, opacityAnim);
    }

    private void AnimateDropDownOpen()
    {
        if (_dropDownBorder == null) return;

        // 清除之前的变换
        _dropDownBorder.RenderTransform = null;

        // 缩放动画
        var scaleTransform = new ScaleTransform(0.9, 0.9);
        _dropDownBorder.RenderTransform = scaleTransform;
        _dropDownBorder.RenderTransformOrigin = new Point(0.5, 0);

        var scaleXAnim = new DoubleAnimation(0.9, 1, TimeSpan.FromSeconds(0.2))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        var scaleYAnim = new DoubleAnimation(0.9, 1, TimeSpan.FromSeconds(0.2))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnim);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnim);

        // 透明度动画
        _dropDownBorder.Opacity = 0;
        var opacityAnim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        _dropDownBorder.BeginAnimation(OpacityProperty, opacityAnim);
    }

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(EverythingComboBox),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty PlaceholderBrushProperty =
        DependencyProperty.Register(nameof(PlaceholderBrush), typeof(Brush), typeof(EverythingComboBox),
            new PropertyMetadata(Brushes.Gray));

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(EverythingComboBox),
            new PropertyMetadata(null));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingComboBox),
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

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}
