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

- **选中项**：渐变背景高亮 + 白色光泽层 + DropShadowEffect 阴影
- **悬停项**：浅灰色背景
- **图标支持**：每个导航项可配置图标
- **滑动指示器**：选中状态使用可滑动的渐变指示器，带平滑过渡动画

## 统一光泽层

选中滑动指示器使用全局统一的白色光泽层资源 `GlossBrush`：

- **渐变规格**：二段式半高，顶部 `#CCFFFFFF`(80%白) → 底部 `#33FFFFFF`(20%白)
- **实现方式**：`{DynamicResource GlossBrush}` + `HalfHeightConverter`
- **跟随动画**：光泽层随滑动指示器一起移动

## 滑动选中动画

侧边栏支持平滑的选中项滑动过渡效果：

- **动画类型**：`ThicknessAnimation` 对 Margin 属性做动画
- **持续时间**：0.25 秒
- **缓动函数**：`CubicEase` (EaseOut)
- **效果**：旧选中项的指示器平滑滑动到新选中项位置
- **指示器组成**：渐变背景 Border + 白色光泽层 Border + DropShadowEffect 阴影

## 使用示例

```xml
<everything:EverythingSideBar ColorName="Blue">
    <everything:EverythingSideBarItem Icon="🏠" Text="首页" IsSelected="True"/>
    <everything:EverythingSideBarItem Icon="📊" Text="统计"/>
    <everything:EverythingSideBarItem Icon="⚙️" Text="设置"/>
</everything:EverythingSideBar>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色。
