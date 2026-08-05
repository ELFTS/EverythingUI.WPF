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

- **水平布局**：默认水平排列，支持水平滚动
- **悬停项**：浅灰背景 + 轻微阴影
- **选中项**：白色文字，由浮动指示器提供高亮
- **浮动指示器**：选中项上方覆盖渐变背景 + 顶部光泽层（GlossBrush）+ 阴影，跟随全局主题实时变化
- **内置滚动条**：集成 EverythingScrollBar 水平滚动条
- **4种显示模式**：TextOnly / IconOnly / IconLeft / IconTop

## 动画效果

- 切换选中项时浮动指示器平滑滑动
- 未选中项悬停时背景色与阴影淡入

## 使用示例

```xml
<!-- 默认 TextOnly 模式 -->
<everything:EverythingToolBar>
    <everything:EverythingToolBar.ItemsSource>
        <x:Array Type="everything:EverythingToolBarItem">
            <everything:EverythingToolBarItem Text="保存"/>
            <everything:EverythingToolBarItem Text="打开"/>
            <everything:EverythingToolBarItem Text="剪切"/>
            <everything:EverythingToolBarItem Text="复制"/>
            <everything:EverythingToolBarItem Text="粘贴"/>
        </x:Array>
    </everything:EverythingToolBar.ItemsSource>
</everything:EverythingToolBar>
```

### 图标模式示例

```xml
<!-- 仅图标模式（Icon 为 ImageSource 类型） -->
<everything:EverythingToolBar ItemDisplayMode="IconOnly">
    <everything:EverythingToolBar.ItemsSource>
        <x:Array Type="everything:EverythingToolBarItem">
            <everything:EverythingToolBarItem Text="保存">
                <everything:EverythingToolBarItem.Icon>
                    <BitmapImage UriSource="pack://application:,,,/YourAssembly;component/Assets/save.png"/>
                </everything:EverythingToolBarItem.Icon>
            </everything:EverythingToolBarItem>
        </x:Array>
    </everything:EverythingToolBar.ItemsSource>
</everything:EverythingToolBar>

<!-- 图标在上模式 -->
<everything:EverythingToolBar ItemDisplayMode="IconTop">
    <!-- 数据源... -->
</everything:EverythingToolBar>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
