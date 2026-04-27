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
            // 如果未设置颜色，则使用资源字典中的默认颜色
            if (GradientStartColor == default)
            {
                SetCurrentValue(GradientStartColorProperty, (Color)FindResource("GradientBlueStart"));
            }
            if (GradientEndColor == default)
            {
                SetCurrentValue(GradientEndColorProperty, (Color)FindResource("GradientBlueEnd"));
            }
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
        /// 选中状态渐变起始颜色（顶部和底部）
        /// </summary>
        public Color GradientStartColor
        {
            get => (Color)GetValue(GradientStartColorProperty);
            set => SetValue(GradientStartColorProperty, value);
        }

        public static readonly DependencyProperty GradientStartColorProperty =
            DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingRadioButton), new PropertyMetadata(default(Color)));

        /// <summary>
        /// 选中状态渐变中间颜色
        /// </summary>
        public Color GradientEndColor
        {
            get => (Color)GetValue(GradientEndColorProperty);
            set => SetValue(GradientEndColorProperty, value);
        }

        public static readonly DependencyProperty GradientEndColorProperty =
            DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingRadioButton), new PropertyMetadata(default(Color)));

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
    }
}
