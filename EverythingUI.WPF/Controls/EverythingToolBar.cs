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
    public enum ToolBarItemDisplayMode
    {
        TextOnly,
        IconOnly,
        IconLeft,
        IconTop
    }

    public class EverythingToolBar : Control
    {
        private ListBox? _menuListBox;
        private ScrollViewer? _scrollViewer;
        private ScrollBar? _scrollBar;

        static EverythingToolBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EverythingToolBar), new FrameworkPropertyMetadata(typeof(EverythingToolBar)));
        }

        public EverythingToolBar()
        {
            Loaded += OnLoaded;
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _menuListBox = GetTemplateChild("menuListBox") as ListBox;
            _scrollViewer = GetTemplateChild("scrollViewer") as ScrollViewer;
            _scrollBar = GetTemplateChild("PART_HorizontalScrollBar") as ScrollBar;

            if (_menuListBox != null)
            {
                _menuListBox.ItemContainerGenerator.StatusChanged += OnItemContainerGeneratorStatusChanged;
                AttachItemEvents();
            }

            SetupScrollSync();
        }

        private void SetupScrollSync()
        {
            if (_scrollViewer == null || _scrollBar == null) return;

            _scrollBar.ValueChanged += (s, e) =>
            {
                _scrollViewer.ScrollToHorizontalOffset(_scrollBar.Value);
            };

            _scrollViewer.ScrollChanged += (s, e) =>
            {
                if (e.HorizontalChange != 0)
                {
                    _scrollBar.Value = _scrollViewer.HorizontalOffset;
                }
            };
        }

        private void OnItemContainerGeneratorStatusChanged(object? sender, EventArgs e)
        {
            if (_menuListBox?.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
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
                    item.MouseEnter -= OnItemMouseEnter;
                    item.MouseLeave -= OnItemMouseLeave;
                    item.Selected -= OnItemSelected;
                    item.Unselected -= OnItemUnselected;

                    item.MouseEnter += OnItemMouseEnter;
                    item.MouseLeave += OnItemMouseLeave;
                    item.Selected += OnItemSelected;
                    item.Unselected += OnItemUnselected;

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

                if (item.IsMouseOver)
                {
                    AnimateItemHover(item, true);
                }
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

            border.BeginAnimation(OpacityProperty, null);
            contentPresenter.BeginAnimation(OpacityProperty, null);

            if (isSelected)
            {
                border.Opacity = 1;
                border.Effect = new DropShadowEffect
                {
                    ShadowDepth = 1,
                    BlurRadius = 4,
                    Opacity = 0.25,
                    Color = Colors.Black
                };
                contentPresenter.SetValue(TextElement.ForegroundProperty, Brushes.White);
                contentPresenter.Opacity = 1;
            }
            else
            {
                var opacityAnim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.15))
                {
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                };

                border.BeginAnimation(OpacityProperty, opacityAnim);
                border.Effect = null;
                contentPresenter.SetValue(TextElement.ForegroundProperty, Brushes.Black);
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
                var opacityAnim = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2))
                {
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                border.BeginAnimation(OpacityProperty, opacityAnim);
                border.Effect = new DropShadowEffect
                {
                    ShadowDepth = 0,
                    BlurRadius = 3,
                    Opacity = 0.12,
                    Color = Colors.Black
                };
                contentPresenter.SetValue(TextElement.ForegroundProperty, Brushes.Black);
            }
            else
            {
                var opacityAnim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.15))
                {
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                };

                border.BeginAnimation(OpacityProperty, opacityAnim);
                border.Effect = null;
                contentPresenter.SetValue(TextElement.ForegroundProperty, Brushes.Black);
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

        public Color GradientStartColor
        {
            get => (Color)GetValue(GradientStartColorProperty);
            set => SetValue(GradientStartColorProperty, value);
        }

        public static readonly DependencyProperty GradientStartColorProperty =
            DependencyProperty.Register(nameof(GradientStartColor), typeof(Color), typeof(EverythingToolBar), new PropertyMetadata(default(Color)));

        public Color GradientEndColor
        {
            get => (Color)GetValue(GradientEndColorProperty);
            set => SetValue(GradientEndColorProperty, value);
        }

        public static readonly DependencyProperty GradientEndColorProperty =
            DependencyProperty.Register(nameof(GradientEndColor), typeof(Color), typeof(EverythingToolBar), new PropertyMetadata(default(Color)));

        public double ToolBarHeight
        {
            get => (double)GetValue(ToolBarHeightProperty);
            set => SetValue(ToolBarHeightProperty, value);
        }

        public static readonly DependencyProperty ToolBarHeightProperty =
            DependencyProperty.Register(nameof(ToolBarHeight), typeof(double), typeof(EverythingToolBar), new PropertyMetadata(48.0));

        public double ItemHeight
        {
            get => (double)GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(EverythingToolBar), new PropertyMetadata(36.0));

        public object ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(EverythingToolBar));

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(EverythingToolBar));

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(EverythingToolBar),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ToolBarItemDisplayMode ItemDisplayMode
        {
            get => (ToolBarItemDisplayMode)GetValue(ItemDisplayModeProperty);
            set => SetValue(ItemDisplayModeProperty, value);
        }

        public static readonly DependencyProperty ItemDisplayModeProperty =
            DependencyProperty.Register(nameof(ItemDisplayMode), typeof(ToolBarItemDisplayMode), typeof(EverythingToolBar),
                new PropertyMetadata(ToolBarItemDisplayMode.TextOnly));

        #endregion
    }
}