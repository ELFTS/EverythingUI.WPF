# EverythingSideBar - 侧边栏控件

带渐变效果的侧边导航栏控件，支持平滑的选中项滑动指示器。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| SideBarWidth | double | 250 | 侧边栏宽度 |
| SideBarHeight | double | Auto | 侧边栏高度 |
| ItemHeight | double | 44 | 菜单项高度 |
| Header | object | null | 标题内容 |
| HeaderTemplate | DataTemplate | null | 标题模板 |
| ItemsSource | object | null | 菜单项源 |
| ItemTemplate | DataTemplate | null | 菜单项模板 |
| SelectedItem | object | null | 选中项 |
| CornerRadius | CornerRadius | 0,16,16,0 | 圆角半径 |
| Content | object | null | 内容区域 |
| ContentTemplate | DataTemplate | null | 内容模板 |
| ItemDisplayMode | SideBarItemDisplayMode | TextOnly | 显示模式 |

## 视觉样式

- **选中项**：渐变背景高亮 + 白色光泽层（GlossBrush）+ DropShadowEffect 阴影
- **悬停项**：浅灰色背景
- **图标支持**：每个导航项可配置图标
- **滑动指示器**：选中状态使用可滑动的渐变指示器，带平滑过渡动画

## 动画效果

- **滑动选中动画**：`ThicknessAnimation` 对 Margin 属性做动画，旧选中项的指示器平滑滑动到新选中项位置
- **持续时间**：0.25 秒，`CubicEase` (EaseOut)
- **指示器组成**：渐变背景 Border + 白色光泽层 Border + DropShadowEffect 阴影
- **主题响应**：订阅 `ThemeManager.ColorChanged`，主题切换后选中滑动指示器渐变会实时更新并重新定位

## 使用示例

```xml
<everything:EverythingSideBar>
    <everything:EverythingSideBarItem Icon="🏠" Text="首页" IsSelected="True"/>
    <everything:EverythingSideBarItem Icon="📊" Text="统计"/>
    <everything:EverythingSideBarItem Icon="⚙️" Text="设置"/>
</everything:EverythingSideBar>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
