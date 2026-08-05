# EverythingContextMenu - 右键菜单控件

支持图标、快捷键提示和分隔线的拟物化右键菜单控件。基于 `Popup` 实现自动失焦关闭、轻量级打开/关闭动画和圆角卡片外观，适合承载编辑、文件、列表项等上下文操作。

设置 `PlacementTarget` 后自动监听其右键事件，用户只需在 XAML 中声明即可获得"在目标上右击 → 弹出菜单"的完整行为，无需编写任何代码后置。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Items | ObservableCollection&lt;EverythingContextMenuItem&gt; | 空集合 | 菜单项集合 |
| PlacementTarget | UIElement | null | 触发右击的元素；设置后自动监听其右键事件 |
| CornerRadius | CornerRadius | 10 | 菜单卡片圆角 |
| ItemHeight | double | 34 | 菜单项高度 |
| IconSize | double | 16 | 图标尺寸 |
| MinWidth | double | 150 | 菜单最小宽度 |
| MaxWidth | double | 280 | 菜单最大宽度 |
| Padding | Thickness | 6 | 菜单卡片内边距 |

## EverythingContextMenuItem 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Text | string | null | 显示文本 |
| Icon | ImageSource | null | 左侧图标（为 null 时自动隐藏图标列） |
| InputGestureText | string | null | 右侧快捷键提示文本 |
| IsSeparator | bool | false | 是否为分隔线项 |
| IsEnabled | bool | true | 是否启用（禁用项半透明且不可点击） |
| Tag | object | null | 自定义数据 |
| Command | ICommand | null | 点击时执行的命令 |
| CommandParameter | object | null | 命令参数 |

## 事件

| 事件 | 描述 |
|------|------|
| ItemClick | 点击菜单项时触发，参数为 `EverythingContextMenuItemClickEventArgs` |

## 方法

| 方法 | 描述 |
|------|------|
| Show(UIElement placementTarget) | 在当前鼠标位置显示菜单 |
| Show(UIElement placementTarget, Point position) | 在相对于 placementTarget 的指定位置处显示菜单 |
| Close() | 关闭菜单 |

## 视觉样式

- **卡片背景**：垂直渐变 + 柔和阴影，圆角设计
- **项悬停**：浅灰渐变背景
- **分隔线**：细线分隔
- **禁用项**：半透明，不可点击
- **无图标自动适配**：`Icon` 为 null 时自动隐藏图标列

## 动画效果

打开时卡片淡入并轻微放大；关闭时直接隐藏。点击菜单外部或菜单项后自动关闭。

## 使用示例

### 推荐用法：声明式绑定 PlacementTarget

```xml
<Grid Margin="20">
    <Border x:Name="Target" Background="#F5F7FA" Height="180" CornerRadius="8">
        <TextBlock Text="在区域内右击" HorizontalAlignment="Center" VerticalAlignment="Center"/>
    </Border>
    <controls:EverythingContextMenu PlacementTarget="{Binding ElementName=Target}"
                                    MinWidth="150" ItemHeight="34" IconSize="16"
                                    ItemClick="Menu_ItemClick">
        <controls:EverythingContextMenu.Items>
            <controls:EverythingContextMenuItem Text="撤销" InputGestureText="Ctrl+Z"/>
            <controls:EverythingContextMenuItem Text="重做" InputGestureText="Ctrl+Y"/>
            <controls:EverythingContextMenuItem IsSeparator="True"/>
            <controls:EverythingContextMenuItem Text="复制" InputGestureText="Ctrl+C"/>
            <controls:EverythingContextMenuItem Text="粘贴" InputGestureText="Ctrl+V"/>
        </controls:EverythingContextMenu.Items>
    </controls:EverythingContextMenu>
</Grid>
```

```csharp
private void Menu_ItemClick(object sender, EverythingContextMenuItemClickEventArgs e)
{
    MessageBox.Show($"点击了：{e.ClickedItem.Text}");
}
```

### 配合 Command 使用

```xml
<controls:EverythingContextMenu PlacementTarget="{Binding ElementName=Target}">
    <controls:EverythingContextMenu.Items>
        <controls:EverythingContextMenuItem Text="复制" Command="{Binding CopyCommand}"/>
        <controls:EverythingContextMenuItem Text="粘贴" Command="{Binding PasteCommand}"/>
    </controls:EverythingContextMenu.Items>
</controls:EverythingContextMenu>
```

### 手动控制 Show / Close

```csharp
myContextMenu.Show(targetElement);
myContextMenu.Show(targetElement, e.GetPosition(targetElement));
myContextMenu.Close();
```

## 使用要点

- **承载位置**：将 `EverythingContextMenu` 与触发元素放在同一 `Grid` 中，通过 `PlacementTarget` 绑定即可。
- **自动管理挂载**：控件通过 `Loaded`/`Unloaded` 自动管理右键挂载，导航离开后回来无需手动重新绑定。
- **配合 NotifyIcon**：作为 `EverythingNotifyIcon` 的右键菜单时，需作为 NotifyIcon 的兄弟节点放入可视树，通过 `ContextMenuControl` 绑定，不能作为属性值嵌套在 NotifyIcon 内部。
- **仅文字菜单**：不给 `EverythingContextMenuItem` 设置 `Icon` 属性即可，图标列自动隐藏。

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
