# EverythingToolBar - 工具栏控件

带渐变效果和浮动指示器的工具栏控件，支持多种显示模式。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| ToolBarHeight | double | 48 | 工具栏高度 |
| ItemHeight | double | 36 | 项高度 |
| ItemsSource | object | null | 数据源 |
| ItemTemplate | DataTemplate | null | 项模板 |
| SelectedItem | object | null | 选中项（双向绑定） |
| ItemDisplayMode | ToolBarItemDisplayMode | TextOnly | 显示模式 |

## ToolBarItemDisplayMode 枚举

| 值 | 描述 |
|----|------|
| TextOnly | 仅文字 |
| IconOnly | 仅图标 |
| IconLeft | 图标在左 |
| IconTop | 图标在上 |

## 视觉样式

- **水平布局**：默认水平排列（StackPanel Orientation=Horizontal），支持水平滚动
- **浮动指示器**：选中项上方覆盖渐变背景 + 顶部光泽层（GlossBrush, Opacity=0.6）+ 阴影（BlurRadius: 12, Opacity: 0.25），带平滑滑动过渡动画
- **悬停项**：浅灰背景（#F0F0F0, Opacity=0.8）+ 轻微阴影（BlurRadius: 3, Opacity: 0.08）
- **选中项**：白色文字（视觉由浮动指示器提供高亮）
- **内置滚动条**：集成 EverythingScrollBar 水平滚动条
- **4种显示模式**：TextOnly / IconOnly / IconLeft / IconTop，每种模式有对应的项模板

> 光泽层固定以 Opacity=0.6 半透明显示。

## 动画效果

- **指示器滑动动画**：选中项切换时浮动指示器平滑滑动（ThicknessAnimation, 0.25s, CubicEase EaseOut）
- **主题响应**：浮动指示器渐变跟随全局主题变化，`ThemeManager.ColorChanged` 后实时刷新
- **悬停动画**：未选中项悬停时背景色淡入 + 阴影淡入（Opacity 动画）

## 使用示例

```xml
<everything:EverythingToolBar>
    <everything:EverythingToolBar.ItemsSource>
        <x:Array Type="everything:EverythingToolBarItem">
            <everything:EverythingToolBarItem Text="保存" Icon="💾"/>
            <everything:EverythingToolBarItem Text="打开" Icon="📂"/>
            <everything:EverythingToolBarItem Text="剪切" Icon="✂️"/>
            <everything:EverythingToolBarItem Text="复制" Icon="📋"/>
            <everything:EverythingToolBarItem Text="粘贴" Icon="📌"/>
        </x:Array>
    </everything:EverythingToolBar.ItemsSource>
</everything:EverythingToolBar>
```

### 图标模式示例

```xml
<!-- 仅图标模式 -->
<everything:EverythingToolBar ItemDisplayMode="IconOnly">
    <!-- 数据源... -->
</everything:EverythingToolBar>

<!-- 图标在上模式 -->
<everything:EverythingToolBar ItemDisplayMode="IconTop">
    <!-- 数据源... -->
</everything:EverythingToolBar>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
