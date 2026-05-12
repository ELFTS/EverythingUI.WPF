using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls
{
    public class EverythingToggleSwitch : ToggleButton
    {
        static EverythingToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingToggleSwitch), new FrameworkPropertyMetadata(typeof(EverythingToggleSwitch)));
        }

        public EverythingToggleSwitch()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateColors();
        }

        private void UpdateColors()
        {
            SetCurrentValue(CheckedGradientStartColorProperty, ColorHelper.GetGradientStartColor(ColorName));
            SetCurrentValue(CheckedGradientEndColorProperty, ColorHelper.GetGradientEndColor(ColorName));
        }

        #region 依赖属性

        /// <summary>
        /// 开关宽度
        /// </summary>
        public double SwitchWidth
        {
            get => (double)GetValue(SwitchWidthProperty);
            set => SetValue(SwitchWidthProperty, value);
        }

        public static readonly DependencyProperty SwitchWidthProperty =
            DependencyProperty.Register(nameof(SwitchWidth), typeof(double), typeof(EverythingToggleSwitch), new PropertyMetadata(50.0));

        /// <summary>
        /// 开关高度
        /// </summary>
        public double SwitchHeight
        {
            get => (double)GetValue(SwitchHeightProperty);
            set => SetValue(SwitchHeightProperty, value);
        }

        public static readonly DependencyProperty SwitchHeightProperty =
            DependencyProperty.Register(nameof(SwitchHeight), typeof(double), typeof(EverythingToggleSwitch), new PropertyMetadata(26.0));

        /// <summary>
        /// 滑块大小
        /// </summary>
        public double ThumbSize
        {
            get => (double)GetValue(ThumbSizeProperty);
            set => SetValue(ThumbSizeProperty, value);
        }

        public static readonly DependencyProperty ThumbSizeProperty =
            DependencyProperty.Register(nameof(ThumbSize), typeof(double), typeof(EverythingToggleSwitch), new PropertyMetadata(22.0));

        /// <summary>
        /// 颜色名称 - 直接使用颜色英文名
        /// </summary>
        public ColorName ColorName
        {
            get => (ColorName)GetValue(ColorNameProperty);
            set => SetValue(ColorNameProperty, value);
        }

        public static readonly DependencyProperty ColorNameProperty =
            DependencyProperty.Register(nameof(ColorName), typeof(ColorName), typeof(EverythingToggleSwitch),
                new PropertyMetadata(ColorName.Blue, OnColorNameChanged));

        internal static readonly DependencyProperty CheckedGradientStartColorProperty =
            DependencyProperty.Register(nameof(CheckedGradientStartColor), typeof(Color), typeof(EverythingToggleSwitch), new PropertyMetadata(default(Color)));

        internal static readonly DependencyProperty CheckedGradientEndColorProperty =
            DependencyProperty.Register(nameof(CheckedGradientEndColor), typeof(Color), typeof(EverythingToggleSwitch), new PropertyMetadata(default(Color)));

        internal Color CheckedGradientStartColor
        {
            get => (Color)GetValue(CheckedGradientStartColorProperty);
            set => SetValue(CheckedGradientStartColorProperty, value);
        }

        internal Color CheckedGradientEndColor
        {
            get => (Color)GetValue(CheckedGradientEndColorProperty);
            set => SetValue(CheckedGradientEndColorProperty, value);
        }

        /// <summary>
        /// 关闭状态背景色
        /// </summary>
        public Brush UncheckedBackground
        {
            get => (Brush)GetValue(UncheckedBackgroundProperty);
            set => SetValue(UncheckedBackgroundProperty, value);
        }

        public static readonly DependencyProperty UncheckedBackgroundProperty =
            DependencyProperty.Register(nameof(UncheckedBackground), typeof(Brush), typeof(EverythingToggleSwitch),
                new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCCCC"))));

        /// <summary>
        /// 滑块颜色
        /// </summary>
        public Brush ThumbBrush
        {
            get => (Brush)GetValue(ThumbBrushProperty);
            set => SetValue(ThumbBrushProperty, value);
        }

        public static readonly DependencyProperty ThumbBrushProperty =
            DependencyProperty.Register(nameof(ThumbBrush), typeof(Brush), typeof(EverythingToggleSwitch),
                new PropertyMetadata(Brushes.White));

        private static void OnColorNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is EverythingToggleSwitch toggleSwitch)
            {
                toggleSwitch.UpdateColors();
            }
        }

        #endregion
    }
}
