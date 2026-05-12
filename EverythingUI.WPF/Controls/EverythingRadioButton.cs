using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls
{
    /// <summary>
    /// 万物界面库单选框控件
    /// </summary>
    public class EverythingRadioButton : RadioButton
    {
        static EverythingRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingRadioButton), new FrameworkPropertyMetadata(typeof(EverythingRadioButton)));
        }

        public EverythingRadioButton()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateColors();
        }

        private void UpdateColors()
        {
            SetCurrentValue(GradientStartColorProperty, ColorHelper.GetGradientStartColor(ColorName));
            SetCurrentValue(GradientEndColorProperty, ColorHelper.GetGradientEndColor(ColorName));
        }

        /// <summary>
        /// 单选框大小
        /// </summary>
        public double BoxSize
        {
            get => (double)GetValue(BoxSizeProperty);
            set => SetValue(BoxSizeProperty, value);
        }

        public static readonly DependencyProperty BoxSizeProperty =
            DependencyProperty.Register(nameof(BoxSize), typeof(double), typeof(EverythingRadioButton), new PropertyMetadata(22.0));

        /// <summary>
        /// 颜色名称 - 直接使用颜色英文名
        /// </summary>
        public ColorName ColorName
        {
            get => (ColorName)GetValue(ColorNameProperty);
            set => SetValue(ColorNameProperty, value);
        }

        public static readonly DependencyProperty ColorNameProperty =
            DependencyProperty.Register(nameof(ColorName), typeof(ColorName), typeof(EverythingRadioButton),
                new PropertyMetadata(ColorName.Blue, OnColorNameChanged));

        internal static readonly DependencyProperty GradientStartColorProperty =
            DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingRadioButton), new PropertyMetadata(default(Color)));

        internal static readonly DependencyProperty GradientEndColorProperty =
            DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingRadioButton), new PropertyMetadata(default(Color)));

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

        /// <summary>
        /// 圆点标记颜色
        /// </summary>
        public Brush DotBrush
        {
            get => (Brush)GetValue(DotBrushProperty);
            set => SetValue(DotBrushProperty, value);
        }

        public static readonly DependencyProperty DotBrushProperty =
            DependencyProperty.Register(nameof(DotBrush), typeof(Brush), typeof(EverythingRadioButton),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));

        private static void OnColorNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is EverythingRadioButton radioButton)
            {
                radioButton.UpdateColors();
            }
        }
    }
}
