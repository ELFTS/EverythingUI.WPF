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
            // 默认使用蓝色渐变
            CheckedGradientStartColor = Color.FromRgb(0, 172, 240);
            CheckedGradientEndColor = Color.FromRgb(0, 120, 212);
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
        /// 开启状态渐变起始颜色
        /// </summary>
        public Color CheckedGradientStartColor
        {
            get => (Color)GetValue(CheckedGradientStartColorProperty);
            set => SetValue(CheckedGradientStartColorProperty, value);
        }

        public static readonly DependencyProperty CheckedGradientStartColorProperty =
            DependencyProperty.Register(nameof(CheckedGradientStartColor), typeof(Color), typeof(EverythingToggleSwitch));

        /// <summary>
        /// 开启状态渐变中间颜色
        /// </summary>
        public Color CheckedGradientEndColor
        {
            get => (Color)GetValue(CheckedGradientEndColorProperty);
            set => SetValue(CheckedGradientEndColorProperty, value);
        }

        public static readonly DependencyProperty CheckedGradientEndColorProperty =
            DependencyProperty.Register(nameof(CheckedGradientEndColor), typeof(Color), typeof(EverythingToggleSwitch));

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
                new PropertyMetadata(new SolidColorBrush(Color.FromRgb(204, 204, 204))));

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

        /// <summary>
        /// 显示文本
        /// </summary>
        public string OnText
        {
            get => (string)GetValue(OnTextProperty);
            set => SetValue(OnTextProperty, value);
        }

        public static readonly DependencyProperty OnTextProperty =
            DependencyProperty.Register(nameof(OnText), typeof(string), typeof(EverythingToggleSwitch), new PropertyMetadata(""));

        /// <summary>
        /// 关闭文本
        /// </summary>
        public string OffText
        {
            get => (string)GetValue(OffTextProperty);
            set => SetValue(OffTextProperty, value);
        }

        public static readonly DependencyProperty OffTextProperty =
            DependencyProperty.Register(nameof(OffText), typeof(string), typeof(EverythingToggleSwitch), new PropertyMetadata(""));

        /// <summary>
        /// 文本位置
        /// </summary>
        public Dock TextPlacement
        {
            get => (Dock)GetValue(TextPlacementProperty);
            set => SetValue(TextPlacementProperty, value);
        }

        public static readonly DependencyProperty TextPlacementProperty =
            DependencyProperty.Register(nameof(TextPlacement), typeof(Dock), typeof(EverythingToggleSwitch), new PropertyMetadata(Dock.Right));

        #endregion
    }
}
