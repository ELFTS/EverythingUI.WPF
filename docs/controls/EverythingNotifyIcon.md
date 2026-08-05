# EverythingNotifyIcon - 系统托盘图标控件

系统托盘图标控件，支持自定义图标、提示文本、气泡通知、右键菜单（集成 `EverythingContextMenu`）和鼠标事件。纯 P/Invoke 实现，无 WindowsForms 依赖。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| IconSource | ImageSource | null | 托盘图标（BitmapSource，自动转换为 HICON） |
| ToolTipText | string | "" | 鼠标悬停时的提示文本 |
| Visible | bool | false | 是否显示托盘图标 |
| ContextMenuControl | EverythingContextMenu | null | 右击时弹出的右键菜单 |

## 事件

| 事件 | 参数 | 描述 |
|------|------|------|
| MouseClick | NotifyIconMouseEventArgs | 鼠标点击托盘图标（左键/右键抬起时触发） |
| MouseDoubleClick | NotifyIconMouseEventArgs | 鼠标双击托盘图标（左键/右键双击均触发） |

## 方法

| 方法 | 描述 |
|------|------|
| ShowBalloonTip(string title, string message, int timeoutMs = 5000) | 显示气泡通知，`timeoutMs` 限制在 0~30000ms |
| Dispose() | 释放资源，移除托盘图标并销毁消息窗口 |

## 使用示例

### 基础用法

> **注意**：`EverythingContextMenu` 必须放在视觉树中（如 Grid 内），通过 `ElementName` 绑定到 `ContextMenuControl`，不能作为属性值嵌套在 `EverythingNotifyIcon` 内部。

```xml
<Grid>
    <controls:EverythingNotifyIcon x:Name="NotifyIcon"
                                   IconSource="{StaticResource AppIcon}"
                                   ToolTipText="我的应用"
                                   ContextMenuControl="{Binding ElementName=TrayMenu}"
                                   MouseClick="NotifyIcon_MouseClick"
                                   MouseDoubleClick="NotifyIcon_MouseDoubleClick"/>
    <controls:EverythingContextMenu x:Name="TrayMenu" ItemClick="TrayMenu_ItemClick">
        <controls:EverythingContextMenu.Items>
            <controls:EverythingContextMenuItem Text="打开主窗口"/>
            <controls:EverythingContextMenuItem IsSeparator="True"/>
            <controls:EverythingContextMenuItem Text="退出"/>
        </controls:EverythingContextMenu.Items>
    </controls:EverythingContextMenu>
</Grid>
```

```csharp
// 显示托盘图标
NotifyIcon.Visible = true;

// 隐藏
NotifyIcon.Visible = false;

// 显示气泡通知
NotifyIcon.ShowBalloonTip("标题", "消息内容");

// 鼠标事件
private void NotifyIcon_MouseClick(object sender, NotifyIconMouseEventArgs e)
{
    if (e.Button == MouseButton.Left)
        MessageBox.Show("左键点击");
}

// 菜单点击
private void TrayMenu_ItemClick(object sender, EverythingContextMenuItemClickEventArgs e)
{
    switch (e.ClickedItem.Text)
    {
        case "打开主窗口": Activate(); break;
        case "退出": NotifyIcon.Visible = false; break;
    }
}
```

## 使用要点

- **纯 P/Invoke 实现**：直接调用 Win32 Shell API，不依赖 WinForms 或 System.Drawing。
- **tooltip 更新**：更新提示文字需带 `NIF_SHOWTIP` 标志（Vista+ 必需），否则提示不显示。
- **右键菜单视觉树**：`ContextMenuControl` 必须在视觉树中（作为 `EverythingNotifyIcon` 的兄弟节点放入 Grid 等），通过 `ElementName` 绑定，不能作为属性值嵌套在 `EverythingNotifyIcon` 内部。
- **图标尺寸**：建议使用 16×16 或 32×32 的 PNG/ICO 图片，支持透明通道。
- **资源释放**：控件 `Unloaded` 时自动移除托盘图标；窗口关闭时调用 `Dispose()` 或将 `Visible` 设为 `false`，避免托盘图标残留。
- **双击行为**：通常将双击设为打开主窗口的快捷操作。

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
