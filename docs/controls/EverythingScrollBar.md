# EverythingScrollBar - 滚动条控件

拟物化风格的滚动条控件，提供垂直和水平两种方向的滚动支持。

> 本控件不使用统一的 GlossBrush 资源（滚动条有自己独立的拟物化样式体系）。

## 样式资源

| 资源键 | 描述 |
|--------|------|
| `EverythingScrollViewerStyle` | ScrollViewer 整体样式 |
| `EverythingVerticalScrollBar` | 垂直滚动条样式 |
| `EverythingHorizontalScrollBar` | 水平滚动条样式 |

## 视觉样式

### 垂直滚动条
- 宽度：22px
- 滑块渐变：白色 → 浅灰 → 灰色（从上到下）
- 三个横杆装饰

### 水平滚动条
- 高度：22px
- 滑块渐变：白色 → 浅灰 → 灰色（从左到右）
- 三个竖杆装饰

### 通用特性
- **垂直三色渐变**：滑块采用三色渐变效果
- **滑块槽设计**：带渐变背景和阴影效果的轨道槽
- **箭头按钮**：上下/左右箭头按钮支持精确滚动
- **阴影效果**：滑块和轨道槽均带有立体阴影
- **圆角设计**：8px 圆角，视觉柔和

## 交互状态

| 状态 | 效果 |
|------|------|
| 默认 | 白色三色渐变，灰色边框 |
| 悬停 | 颜色加深，边框变深 |
| 按下 | 颜色进一步加深 |

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

## 注意事项

- 滚动条宽度/高度固定为 22px，不支持自定义尺寸
- 滑块槽背景使用渐变效果，增强立体感
- 箭头按钮支持连续点击滚动

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
