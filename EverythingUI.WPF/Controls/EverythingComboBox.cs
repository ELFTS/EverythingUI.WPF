using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EverythingUI.WPF.Controls;

public class EverythingComboBox : ComboBox
{
    private Border? _dropDownBorder;
    private Popup? _popup;
    private Border? _border;

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
        _popup = GetTemplateChild("popup") as Popup;
        _border = GetTemplateChild("border") as Border;

        if (_popup != null)
        {
            _popup.CustomPopupPlacementCallback = OnCustomPopupPlacement;
        }
    }

    private CustomPopupPlacement[] OnCustomPopupPlacement(Size popupSize, Size targetSize, Point offset)
    {
        // 精确计算位置：与目标元素左对齐，顶部紧贴目标元素底部
        var placement = new CustomPopupPlacement(
            new Point(0, targetSize.Height + 2), // X=0 左对齐, Y=目标高度+2px间隙
            PopupPrimaryAxis.Vertical
        );
        return new[] { placement };
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
            new PropertyMetadata(Brushes.White));

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(EverythingComboBox),
            new PropertyMetadata(null));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingComboBox),
            new PropertyMetadata(new CornerRadius(6)));

    public static readonly DependencyProperty GradientStartColorProperty =
        DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingComboBox),
            new PropertyMetadata(Color.FromRgb(156, 163, 175)));

    public static readonly DependencyProperty GradientEndColorProperty =
        DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingComboBox),
            new PropertyMetadata(Color.FromRgb(107, 114, 128)));

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

    public Color GradientStartColor
    {
        get => (Color)GetValue(GradientStartColorProperty);
        set => SetValue(GradientStartColorProperty, value);
    }

    public Color GradientEndColor
    {
        get => (Color)GetValue(GradientEndColorProperty);
        set => SetValue(GradientEndColorProperty, value);
    }
}
