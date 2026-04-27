using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls
{
    /// <summary>
    /// 万物界面库复选框控件
    /// </summary>
    public class EverythingCheckBox : CheckBox
    {
        static EverythingCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingCheckBox), new FrameworkPropertyMetadata(typeof(EverythingCheckBox)));
        }

        public EverythingCheckBox()
        {
            // 从资源字典加载默认颜色
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
        /// 复选框大小
        /// </summary>
        public double BoxSize
        {
            get => (double)GetValue(BoxSizeProperty);
            set => SetValue(BoxSizeProperty, value);
        }

        public static readonly DependencyProperty BoxSizeProperty =
            DependencyProperty.Register(nameof(BoxSize), typeof(double), typeof(EverythingCheckBox), new PropertyMetadata(22.0));

        /// <summary>
        /// 圆角半径
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingCheckBox), new PropertyMetadata(new CornerRadius(6)));

        /// <summary>
        /// 选中状态渐变起始颜色（顶部和底部）
        /// </summary>
        public Color GradientStartColor
        {
            get => (Color)GetValue(GradientStartColorProperty);
            set => SetValue(GradientStartColorProperty, value);
        }

        public static readonly DependencyProperty GradientStartColorProperty =
            DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingCheckBox), new PropertyMetadata(default(Color)));

        /// <summary>
        /// 选中状态渐变中间颜色
        /// </summary>
        public Color GradientEndColor
        {
            get => (Color)GetValue(GradientEndColorProperty);
            set => SetValue(GradientEndColorProperty, value);
        }

        public static readonly DependencyProperty GradientEndColorProperty =
            DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingCheckBox), new PropertyMetadata(default(Color)));

        /// <summary>
        /// 勾选标记颜色
        /// </summary>
        public Brush CheckMarkBrush
        {
            get => (Brush)GetValue(CheckMarkBrushProperty);
            set => SetValue(CheckMarkBrushProperty, value);
        }

        public static readonly DependencyProperty CheckMarkBrushProperty =
            DependencyProperty.Register(nameof(CheckMarkBrush), typeof(Brush), typeof(EverythingCheckBox),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));
    }
}
