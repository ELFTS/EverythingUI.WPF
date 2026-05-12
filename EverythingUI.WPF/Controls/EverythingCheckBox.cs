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
        /// 颜色名称 - 直接使用颜色英文名
        /// </summary>
        public ColorName ColorName
        {
            get => (ColorName)GetValue(ColorNameProperty);
            set => SetValue(ColorNameProperty, value);
        }

        public static readonly DependencyProperty ColorNameProperty =
            DependencyProperty.Register(nameof(ColorName), typeof(ColorName), typeof(EverythingCheckBox),
                new PropertyMetadata(ColorName.Blue, OnColorNameChanged));

        internal static readonly DependencyProperty GradientStartColorProperty =
            DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingCheckBox), new PropertyMetadata(default(Color)));

        internal static readonly DependencyProperty GradientEndColorProperty =
            DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingCheckBox), new PropertyMetadata(default(Color)));

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

        private static void OnColorNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is EverythingCheckBox checkBox)
            {
                checkBox.UpdateColors();
            }
        }
    }
}
