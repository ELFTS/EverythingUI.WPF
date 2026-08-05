# EverythingScrollBar - 滚动条控件

拟物化风格的滚动条控件，提供垂直和水平两种方向的滚动支持。

> 本控件为纯 XAML 样式资源实现（无对应 `.cs` 代码类），通过 `EverythingScrollBar.xaml` 中定义的 Style 资源应用。

## 样式资源

| 资源键 | 描述 |
|--------|------|
| `EverythingScrollViewerStyle` | ScrollViewer 整体样式 |
| `EverythingVerticalScrollBar` | 垂直滚动条样式 |
| `EverythingHorizontalScrollBar` | 水平滚动条样式 |
| `EverythingVerticalScrollBarThumb` / `EverythingHorizontalScrollBarThumb` | 滑块样式 |
| `ScrollBarButtonStyle` | 箭头按钮样式 |
| `ThumbVerticalGradientBrush` / `ThumbVerticalHoverGradientBrush` / `ThumbVerticalPressedGradientBrush` | 垂直滑块三态渐变画刷 |
| `ThumbHorizontalGradientBrush` / `ThumbHorizontalHoverGradientBrush` / `ThumbHorizontalPressedGradientBrush` | 水平滑块三态渐变画刷 |
| `TrackBackgroundBrush` | 滑块槽背景渐变画刷 |

## 视觉样式

- **滑块**：三色渐变 + 圆角 + 立体阴影，带装饰横杆/竖杆
- **滑块槽**：渐变背景 + 阴影，圆角设计
- **箭头按钮**：透明背景，圆角箭头路径，悬停/按下时叠加半透明白色背景
- 滑块三态（默认/悬停/按下）通过渐变画刷由浅至深切换

| 状态 | 外观 |
|------|------|
| 默认 | 浅色渐变滑块 |
| 悬停（IsMouseOver） | 渐变加深 |
| 按下（IsDragging） | 渐变进一步加深 |

## 使用示例

滚动条通常通过 `ScrollViewer` 样式自动应用，SideBar、ToolBar、IconListBox 等复合控件已内置使用该滚动条样式：

```xml
<ScrollViewer Style="{DynamicResource EverythingScrollViewerStyle}">
    <!-- 内容 -->
</ScrollViewer>
```

### 完整示例

```xml
<Window xmlns:everything="clr-namespace:EverythingUI.WPF.Controls;assembly=EverythingUI.WPF">
    <ScrollViewer Style="{DynamicResource EverythingScrollViewerStyle}">
        <StackPanel>
            <TextBlock Text="内容区域" FontSize="16"/>
            <!-- 更多内容 -->
        </StackPanel>
    </ScrollViewer>
</Window>
```

## 使用要点

- **全局默认样式**：通过 `Generic.xaml` 全局默认样式自动应用到所有 `ScrollViewer`/`ListBox`/`ComboBox`/`TreeView` 中的滚动条，无需手动指定。
- 滚动条宽度/高度固定，不支持自定义尺寸。
- 箭头按钮支持连续点击滚动。

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
