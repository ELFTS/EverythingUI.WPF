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
    private ScaleTransform? _scaleTransform;

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
        UpdateColors();

        // 初始化缩放变换
        _scaleTransform = new ScaleTransform(1, 1);
        RenderTransform = _scaleTransform;
        RenderTransformOrigin = new Point(0.5, 0.5);

        // 入场动画 - 从下方弹入
        var translateTransform = new TranslateTransform(0, 20);
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(_scaleTransform);
        transformGroup.Children.Add(translateTransform);
        RenderTransform = transformGroup;

        // 缩放动画
        var scaleXAnim = new DoubleAnimation(0.9, 1, TimeSpan.FromSeconds(0.4))
        {
            EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.3 }
        };
        var scaleYAnim = new DoubleAnimation(0.9, 1, TimeSpan.FromSeconds(0.4))
        {
            EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.3 }
        };
        _scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnim);
        _scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnim);

        // 位移动画
        var translateAnim = new DoubleAnimation(20, 0, TimeSpan.FromSeconds(0.4))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        translateTransform.BeginAnimation(TranslateTransform.YProperty, translateAnim);

        // 透明度动画
        var opacityAnim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        BeginAnimation(OpacityProperty, opacityAnim);
    }

    private void UpdateColors()
    {
        SetCurrentValue(GradientStartColorProperty, ColorHelper.GetGradientStartColor(ColorName));
        SetCurrentValue(GradientEndColorProperty, ColorHelper.GetGradientEndColor(ColorName));
    }

    private void AnimateDropDownOpen()
    {
        if (_dropDownBorder == null) return;

        // 清除之前的变换
        _dropDownBorder.RenderTransform = null;

        // 创建变换组
        var scaleTransform = new ScaleTransform(0.8, 0.8);
        var translateTransform = new TranslateTransform(0, -10);
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(scaleTransform);
        transformGroup.Children.Add(translateTransform);
        _dropDownBorder.RenderTransform = transformGroup;
        _dropDownBorder.RenderTransformOrigin = new Point(0.5, 0);

        // 缩放动画 - 弹性效果
        var scaleXAnim = new DoubleAnimation(0.8, 1, TimeSpan.FromSeconds(0.35))
        {
            EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.5 }
        };
        var scaleYAnim = new DoubleAnimation(0.8, 1, TimeSpan.FromSeconds(0.35))
        {
            EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.5 }
        };
        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnim);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnim);

        // 位移动画 - 从上方滑入
        var translateAnim = new DoubleAnimation(-10, 0, TimeSpan.FromSeconds(0.35))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        translateTransform.BeginAnimation(TranslateTransform.YProperty, translateAnim);

        // 透明度动画
        _dropDownBorder.Opacity = 0;
        var opacityAnim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.25))
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

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingComboBox),
            new PropertyMetadata(new CornerRadius(6)));

    public static readonly DependencyProperty ColorNameProperty =
        DependencyProperty.Register(nameof(ColorName), typeof(ColorName), typeof(EverythingComboBox),
            new PropertyMetadata(ColorName.Blue, OnColorNameChanged));

    internal static readonly DependencyProperty GradientStartColorProperty =
        DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingComboBox),
            new PropertyMetadata(default(Color)));

    internal static readonly DependencyProperty GradientEndColorProperty =
        DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingComboBox),
            new PropertyMetadata(default(Color)));

    private static void OnColorNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingComboBox comboBox)
        {
            comboBox.UpdateColors();
        }
    }

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

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// 颜色名称 - 直接使用颜色英文名
    /// </summary>
    public ColorName ColorName
    {
        get => (ColorName)GetValue(ColorNameProperty);
        set => SetValue(ColorNameProperty, value);
    }

    internal Color GradientStartColor
    {
        get => (Color)GetValue(GradientStartColorProperty);
        set => SetValue(GradientStartColorProperty, value);
    }

    internal Color GradientEndColor
    {
        get => (Color)GetValue(GradientEndColorProperty);
        set => SetValue(GradientEndColorProperty, value);
    }
}
