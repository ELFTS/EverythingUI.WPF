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

## SideBarItemDisplayMode 枚举

| 值 | 描述 |
|----|------|
| TextOnly | 仅文字 |
| IconOnly | 仅图标 |
| IconLeft | 图标在左 |
| IconTop | 图标在上 |

## 视觉样式

- **选中项**：渐变背景高亮 + 顶部白色光泽层（GlossBrush）+ 阴影
- **悬停项**：浅灰色背景
- **滑动指示器**：选中项使用可滑动的渐变指示器，跟随全局主题实时变化

## 动画效果

- 切换选中项时，滑动指示器从旧位置平滑滑动到新位置

## 使用示例

```xml
<!-- 通过 ItemsSource 绑定菜单项，选中项通过 SelectedItem 双向绑定 -->
<everything:EverythingSideBar ItemDisplayMode="IconLeft">
    <everything:EverythingSideBar.ItemsSource>
        <x:Array Type="everything:EverythingSideBarItem">
            <everything:EverythingSideBarItem Text="首页">
                <everything:EverythingSideBarItem.Icon>
                    <BitmapImage UriSource="pack://application:,,,/YourAssembly;component/Assets/home.png"/>
                </everything:EverythingSideBarItem.Icon>
            </everything:EverythingSideBarItem>
            <everything:EverythingSideBarItem Text="统计"/>
            <everything:EverythingSideBarItem Text="设置"/>
        </x:Array>
    </everything:EverythingSideBar.ItemsSource>
</everything:EverythingSideBar>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
