using System.Linq;
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
    private readonly ScaleTransform _borderOpenScaleTransform = new(1, 0);
    private readonly ScaleTransform _borderCloseScaleTransform = new(1, 1);
    private readonly CubicEase _easeOut = new() { EasingMode = EasingMode.EaseOut };
    private readonly CubicEase _easeIn = new() { EasingMode = EasingMode.EaseIn };

    static EverythingComboBox() =>
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingComboBox),
            new FrameworkPropertyMetadata(typeof(EverythingComboBox)));

    public EverythingComboBox() => Loaded += OnLoaded;

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _dropDownBorder = GetTemplateChild("dropDownBorder") as Border;
        _popup = GetTemplateChild("popup") as Popup;
        if (_popup != null) _popup.CustomPopupPlacementCallback = OnCustomPopupPlacement;
    }

    private CustomPopupPlacement[] OnCustomPopupPlacement(Size popupSize, Size targetSize, Point offset)
        => [new(new Point(0, targetSize.Height + 2), PopupPrimaryAxis.Vertical)];

    protected override void OnDropDownOpened(EventArgs e) { base.OnDropDownOpened(e); AnimateDropDownBlindsOpen(); }
    protected override void OnDropDownClosed(EventArgs e) { base.OnDropDownClosed(e); AnimateDropDownBlindsClose(); }

    private void OnLoaded(object sender, RoutedEventArgs e) => UpdateColors();

    private void UpdateColors()
    {
        SetCurrentValue(GradientStartColorProperty, ColorHelper.GetGradientStartColor(ColorName));
        SetCurrentValue(GradientEndColorProperty, ColorHelper.GetGradientEndColor(ColorName));
    }

    private void AnimateDropDownBlindsOpen()
    {
        if (_dropDownBorder == null) return;
        _dropDownBorder.RenderTransformOrigin = new Point(0.5, 0);
        _dropDownBorder.RenderTransform = _borderOpenScaleTransform;
        _dropDownBorder.ClipToBounds = true;

        _borderOpenScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty,
            new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.4)) { EasingFunction = _easeOut });
        _dropDownBorder.BeginAnimation(OpacityProperty,
            new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.15)) { EasingFunction = _easeOut });

        var items = Items.OfType<object>().ToList();
        var blindDelay = TimeSpan.FromSeconds(0.06);
        var blindDuration = TimeSpan.FromSeconds(0.3);

        for (var i = 0; i < items.Count; i++)
        {
            if (ItemContainerGenerator.ContainerFromItem(items[i]) is not ComboBoxItem container) continue;
            container.RenderTransformOrigin = new Point(0.5, 0);
            var scaleTransform = new ScaleTransform(1, 0);
            container.RenderTransform = scaleTransform;
            container.Opacity = 0;

            var delay = TimeSpan.FromMilliseconds(blindDelay.TotalMilliseconds * i) + TimeSpan.FromSeconds(0.05);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty,
                new DoubleAnimation(0, 1, blindDuration) { BeginTime = delay, EasingFunction = _easeOut });
            container.BeginAnimation(OpacityProperty,
                new DoubleAnimation(0, 1, blindDuration * 0.6) { BeginTime = delay, EasingFunction = _easeOut });
        }
    }

    private void AnimateDropDownBlindsClose()
    {
        if (_dropDownBorder == null) return;
        _dropDownBorder.RenderTransformOrigin = new Point(0.5, 1);
        _dropDownBorder.RenderTransform = _borderCloseScaleTransform;

        var items = Items.OfType<object>().ToList();
        var blindDelay = TimeSpan.FromSeconds(0.04);
        var blindDuration = TimeSpan.FromSeconds(0.25);

        for (var i = items.Count - 1; i >= 0; i--)
        {
            if (ItemContainerGenerator.ContainerFromItem(items[i]) is not ComboBoxItem container) continue;
            var reverseIndex = items.Count - 1 - i;
            var delay = TimeSpan.FromMilliseconds(blindDelay.TotalMilliseconds * reverseIndex);

            container.RenderTransformOrigin = new Point(0.5, 1);
            var scaleTransform = new ScaleTransform(1, 1);
            container.RenderTransform = scaleTransform;

            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty,
                new DoubleAnimation(1, 0, blindDuration) { BeginTime = delay, EasingFunction = _easeIn });
            container.BeginAnimation(OpacityProperty,
                new DoubleAnimation(1, 0, blindDuration * 0.8) { BeginTime = delay + (blindDuration * 0.2), EasingFunction = _easeIn });
        }

        var totalItemDuration = TimeSpan.FromMilliseconds(blindDelay.TotalMilliseconds * items.Count + blindDuration.TotalMilliseconds);
        _borderCloseScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty,
            new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.25)) { BeginTime = totalItemDuration - TimeSpan.FromSeconds(0.25), EasingFunction = _easeIn });
        _dropDownBorder.BeginAnimation(OpacityProperty,
            new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.1)) { BeginTime = totalItemDuration - TimeSpan.FromSeconds(0.1), EasingFunction = _easeIn });
    }

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(EverythingComboBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty PlaceholderBrushProperty =
        DependencyProperty.Register(nameof(PlaceholderBrush), typeof(Brush), typeof(EverythingComboBox),
            new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingComboBox),
            new FrameworkPropertyMetadata(new CornerRadius(6), FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty ColorNameProperty =
        DependencyProperty.Register(nameof(ColorName), typeof(ColorName), typeof(EverythingComboBox),
            new PropertyMetadata(ColorHelper.DefaultColorName, OnColorNameChanged));

    internal static readonly DependencyProperty GradientStartColorProperty =
        DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingComboBox),
            new FrameworkPropertyMetadata(default(Color), FrameworkPropertyMetadataOptions.AffectsRender));

    internal static readonly DependencyProperty GradientEndColorProperty =
        DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingComboBox),
            new FrameworkPropertyMetadata(default(Color), FrameworkPropertyMetadataOptions.AffectsRender));

    public string Placeholder { get => (string)GetValue(PlaceholderProperty); set => SetValue(PlaceholderProperty, value); }
    public Brush PlaceholderBrush { get => (Brush)GetValue(PlaceholderBrushProperty); set => SetValue(PlaceholderBrushProperty, value); }
    public CornerRadius CornerRadius { get => (CornerRadius)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }
    public ColorName ColorName { get => (ColorName)GetValue(ColorNameProperty); set => SetValue(ColorNameProperty, value); }
    internal Color GradientStartColor { get => (Color)GetValue(GradientStartColorProperty); set => SetValue(GradientStartColorProperty, value); }
    internal Color GradientEndColor { get => (Color)GetValue(GradientEndColorProperty); set => SetValue(GradientEndColorProperty, value); }

    private static void OnColorNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EverythingComboBox comboBox) comboBox.UpdateColors();
    }
}
