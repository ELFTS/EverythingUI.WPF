# EverythingSideBar - 侧边栏控件

带渐变效果的侧边导航栏控件。

## 属性

| 属性                 | 类型   | 描述           |
| ------------------ | ---- | ------------ |
| ColorName          | ColorName | 颜色名称（默认 Blue） |
| SideBarWidth       | double | 侧边栏宽度（默认 250） |
| SideBarHeight      | double | 侧边栏高度（默认 Auto） |
| ItemHeight         | double | 菜单项高度（默认 44） |
| Header             | object | 标题内容 |
| HeaderTemplate     | DataTemplate | 标题模板 |
| ItemsSource        | object | 菜单项源 |
| ItemTemplate       | DataTemplate | 菜单项模板 |
| SelectedItem       | object | 选中项 |
| CornerRadius       | CornerRadius | 圆角半径（默认 0,16,16,0） |
| Content            | object | 内容区域 |
| ContentTemplate    | DataTemplate | 内容模板 |
| ItemDisplayMode    | SideBarItemDisplayMode | 显示模式（默认 TextOnly） |

## 视觉样式

- **选中项**：渐变背景高亮
- **悬停项**：浅灰色背景
- **图标支持**：每个导航项可配置图标

## 使用示例

```xml
<everything:EverythingSideBar ColorName="Blue">
    <everything:EverythingSideBarItem Icon="🏠" Text="首页" IsSelected="True"/>
    <everything:EverythingSideBarItem Icon="📊" Text="统计"/>
    <everything:EverythingSideBarItem Icon="⚙️" Text="设置"/>
</everything:EverythingSideBar>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色。
