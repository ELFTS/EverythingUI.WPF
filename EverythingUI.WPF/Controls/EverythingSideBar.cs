using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace EverythingUI.WPF.Controls
{
    /// <summary>
    /// 侧边栏项显示模式
    /// </summary>
    public enum SideBarItemDisplayMode
    {
        /// <summary>
        /// 仅显示文字
        /// </summary>
        TextOnly,

        /// <summary>
        /// 仅显示图标
        /// </summary>
        IconOnly,

        /// <summary>
        /// 图标在左，文字在右
        /// </summary>
        IconLeft,

        /// <summary>
        /// 图标在上，文字在下
        /// </summary>
        IconTop
    }

    public class EverythingSideBar : Control
    {
        private ListBox? _menuListBox;
        private ScrollViewer? _scrollViewer;
        private ScrollBar? _scrollBar;
        private readonly Dictionary<ListBoxItem, Storyboard> _hoverStoryboards = new();

        static EverythingSideBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingSideBar), new FrameworkPropertyMetadata(typeof(EverythingSideBar)));
        }

        public EverythingSideBar()
        {
            Loaded += OnLoaded;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _menuListBox = GetTemplateChild("menuListBox") as ListBox;
            _scrollViewer = GetTemplateChild("scrollViewer") as ScrollViewer;
            _scrollBar = GetTemplateChild("PART_VerticalScrollBar") as ScrollBar;

            if (_menuListBox != null)
            {
                _menuListBox.ItemContainerGenerator.StatusChanged += OnItemContainerGeneratorStatusChanged;
                AttachItemEvents();
            }

            // 设置滚动条同步
            SetupScrollSync();
        }

        private void SetupScrollSync()
        {
            if (_scrollViewer == null || _scrollBar == null) return;

            // 滚动条值变化时同步到 ScrollViewer
            _scrollBar.ValueChanged += (s, e) =>
            {
                _scrollViewer.ScrollToVerticalOffset(_scrollBar.Value);
            };

            // ScrollViewer 滚动时同步到滚动条
            _scrollViewer.ScrollChanged += (s, e) =>
            {
                if (e.VerticalChange != 0)
                {
                    _scrollBar.Value = _scrollViewer.VerticalOffset;
                }
            };
        }

        private void OnItemContainerGeneratorStatusChanged(object? sender, EventArgs e)
        {
            if (_menuListBox?.ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
            {
                AttachItemEvents();
            }
        }

        private void AttachItemEvents()
        {
            if (_menuListBox == null) return;

            for (int i = 0; i < _menuListBox.Items.Count; i++)
            {
                if (_menuListBox.ItemContainerGenerator.ContainerFromIndex(i) is ListBoxItem item)
                {
                    // 移除旧事件
                    item.MouseEnter -= OnItemMouseEnter;
                    item.MouseLeave -= OnItemMouseLeave;
                    item.Selected -= OnItemSelected;
                    item.Unselected -= OnItemUnselected;

                    // 添加新事件
                    item.MouseEnter += OnItemMouseEnter;
                    item.MouseLeave += OnItemMouseLeave;
                    item.Selected += OnItemSelected;
                    item.Unselected += OnItemUnselected;

                    // 初始化状态 - 根据选中状态更新
                    UpdateItemVisualState(item, item.IsSelected);
                }
            }
        }

        private void OnItemSelected(object sender, RoutedEventArgs e)
        {
            if (sender is ListBoxItem item)
            {
                UpdateItemVisualState(item, true);
            }
        }

        private void OnItemUnselected(object sender, RoutedEventArgs e)
        {
            if (sender is ListBoxItem item)
            {
                UpdateItemVisualState(item, false);
            }
        }

        private void OnItemMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is ListBoxItem item && !item.IsSelected)
            {
                AnimateItemHover(item, true);
            }
        }

        private void OnItemMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is ListBoxItem item && !item.IsSelected)
            {
                AnimateItemHover(item, false);
            }
        }

        private void UpdateItemVisualState(ListBoxItem item, bool isSelected)
        {
            var border = FindChild<Border>(item, "border");
            var contentPresenter = FindChild<ContentPresenter>(item, "contentPresenter");

            if (border == null || contentPresenter == null) return;

            // 清除所有动画
            border.BeginAnimation(OpacityProperty, null);
            contentPresenter.BeginAnimation(OpacityProperty, null);

            if (isSelected)
            {
                // 选中状态
                border.Opacity = 1;
                border.Effect = new DropShadowEffect
                {
                    ShadowDepth = 2,
                    BlurRadius = 6,
                    Opacity = 0.3,
                    Color = Colors.Black
                };
                contentPresenter.SetValue(TextElement.ForegroundProperty, Brushes.White);
                contentPresenter.Opacity = 1;
            }
            else
            {
                // 默认状态
                border.Opacity = 0;
                border.Effect = null;
                contentPresenter.SetValue(TextElement.ForegroundProperty, FindResource("TextPrimaryBrush") as Brush ?? Brushes.Black);
                contentPresenter.Opacity = 1;
            }
        }

        private void AnimateItemHover(ListBoxItem item, bool isHovering)
        {
            var border = FindChild<Border>(item, "border");
            var contentPresenter = FindChild<ContentPresenter>(item, "contentPresenter");

            if (border == null || contentPresenter == null) return;

            if (isHovering)
            {
                // 悬停进入动画 - 背景淡入
                var opacityAnim = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2))
                {
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };
                border.BeginAnimation(OpacityProperty, opacityAnim);

                // 文字颜色变化
                contentPresenter.SetValue(TextElement.ForegroundProperty, Brushes.White);
            }
            else
            {
                // 悬停退出动画 - 背景淡出
                var opacityAnim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.15))
                {
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                };
                border.BeginAnimation(OpacityProperty, opacityAnim);

                // 文字颜色恢复
                contentPresenter.SetValue(TextElement.ForegroundProperty, FindResource("TextPrimaryBrush") as Brush ?? Brushes.Black);
            }
        }

        private static T? FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is FrameworkElement frameworkElement && frameworkElement.Name == childName && child is T result)
                {
                    return result;
                }

                var childOfChild = FindChild<T>(child, childName);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // 从资源字典加载默认颜色
            if (GradientStartColor == default)
            {
                SetCurrentValue(GradientStartColorProperty, (Color)FindResource("GradientBlueStart"));
            }
            if (GradientEndColor == default)
            {
                SetCurrentValue(GradientEndColorProperty, (Color)FindResource("GradientBlueEnd"));
            }
            // 默认选中第一项
            SelectFirstItem();
        }

        private void SelectFirstItem()
        {
            if (SelectedItem == null && ItemsSource is IEnumerable items)
            {
                foreach (var item in items)
                {
                    SelectedItem = item;
                    break;
                }
            }
        }

        #region 依赖属性

        /// <summary>
        /// 渐变起始颜色（上下位置）
        /// </summary>
        public Color GradientStartColor
        {
            get => (Color)GetValue(GradientStartColorProperty);
            set => SetValue(GradientStartColorProperty, value);
        }

        public static readonly DependencyProperty GradientStartColorProperty =
            DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingSideBar), new PropertyMetadata(default(Color)));

        /// <summary>
        /// 渐变中间颜色
        /// </summary>
        public Color GradientEndColor
        {
            get => (Color)GetValue(GradientEndColorProperty);
            set => SetValue(GradientEndColorProperty, value);
        }

        public static readonly DependencyProperty GradientEndColorProperty =
            DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingSideBar), new PropertyMetadata(default(Color)));

        /// <summary>
        /// 侧边栏宽度
        /// </summary>
        public double SideBarWidth
        {
            get => (double)GetValue(SideBarWidthProperty);
            set => SetValue(SideBarWidthProperty, value);
        }

        public static readonly DependencyProperty SideBarWidthProperty =
            DependencyProperty.Register(nameof(SideBarWidth), typeof(double), typeof(EverythingSideBar), new PropertyMetadata(250.0));

        /// <summary>
        /// 侧边栏高度（Auto 或具体数值）
        /// </summary>
        public double SideBarHeight
        {
            get => (double)GetValue(SideBarHeightProperty);
            set => SetValue(SideBarHeightProperty, value);
        }

        public static readonly DependencyProperty SideBarHeightProperty =
            DependencyProperty.Register(nameof(SideBarHeight), typeof(double), typeof(EverythingSideBar), new PropertyMetadata(double.NaN));

        /// <summary>
        /// 菜单项高度（默认44）
        /// </summary>
        public double ItemHeight
        {
            get => (double)GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(EverythingSideBar), new PropertyMetadata(44.0));

        /// <summary>
        /// 标题
        /// </summary>
        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(object), typeof(EverythingSideBar));

        /// <summary>
        /// 标题模板
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate)GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }

        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register(nameof(HeaderTemplate), typeof(DataTemplate), typeof(EverythingSideBar));

        /// <summary>
        /// 菜单项源
        /// </summary>
        public object ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(EverythingSideBar));

        /// <summary>
        /// 菜单项模板
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(EverythingSideBar));

        /// <summary>
        /// 选中项
        /// </summary>
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(EverythingSideBar),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 圆角半径
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(EverythingSideBar),
                new PropertyMetadata(new CornerRadius(0, 16, 16, 0)));

        /// <summary>
        /// 内容区域
        /// </summary>
        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(EverythingSideBar));

        /// <summary>
        /// 内容模板
        /// </summary>
        public DataTemplate ContentTemplate
        {
            get => (DataTemplate)GetValue(ContentTemplateProperty);
            set => SetValue(ContentTemplateProperty, value);
        }

        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register(nameof(ContentTemplate), typeof(DataTemplate), typeof(EverythingSideBar));

        /// <summary>
        /// 菜单项显示模式
        /// </summary>
        public SideBarItemDisplayMode ItemDisplayMode
        {
            get => (SideBarItemDisplayMode)GetValue(ItemDisplayModeProperty);
            set => SetValue(ItemDisplayModeProperty, value);
        }

        public static readonly DependencyProperty ItemDisplayModeProperty =
            DependencyProperty.Register(nameof(ItemDisplayMode), typeof(SideBarItemDisplayMode), typeof(EverythingSideBar),
                new PropertyMetadata(SideBarItemDisplayMode.TextOnly));

        #endregion
    }
}
