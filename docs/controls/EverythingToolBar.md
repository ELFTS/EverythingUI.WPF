# EverythingToolBar - 工具栏控件

带渐变效果的工具栏控件。

## 属性

| 属性                 | 类型   | 描述           |
| ------------------ | ---- | ------------ |
| ColorName          | ColorName | 颜色名称（默认 Blue） |
| ToolBarHeight      | double | 工具栏高度（默认 48） |
| ItemHeight         | double | 项高度（默认 36） |
| ItemsSource        | object | 数据源 |
| ItemTemplate       | DataTemplate | 项模板 |
| SelectedItem       | object | 选中项 |
| ItemDisplayMode    | ToolBarItemDisplayMode | 显示模式（默认 TextOnly） |

## 视觉样式

- **工具按钮**：渐变背景
- **分隔线**：细线分隔
- **紧凑布局**：适合放置常用操作按钮

## 使用示例

```xml
<everything:EverythingToolBar ColorName="Blue">
    <everything:EverythingToolBarButton Icon="💾" Text="保存"/>
    <everything:EverythingToolBarButton Icon="📂" Text="打开"/>
    <Separator/>
    <everything:EverythingToolBarButton Icon="✂️" Text="剪切"/>
    <everything:EverythingToolBarButton Icon="📋" Text="复制"/>
    <everything:EverythingToolBarButton Icon="📌" Text="粘贴"/>
</everything:EverythingToolBar>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色。
