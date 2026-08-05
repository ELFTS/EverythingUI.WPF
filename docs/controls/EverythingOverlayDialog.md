# EverythingOverlayDialog - 遮罩对话框控件

全屏遮罩对话框控件，遮罩层带毛玻璃效果，对话框内容直接使用 `EverythingCard` 承载，适合确认提示、表单编辑、重要操作等场景。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| IsOpen | bool | false | 是否显示遮罩对话框，支持双向绑定 |
| BlurRadius | double | 18.0 | 毛玻璃模糊半径 |
| DialogWidth | double | NaN | 对话框固定宽度 |
| DialogMaxWidth | double | 520.0 | 对话框最大宽度 |
| DialogMaxHeight | double | 720.0 | 对话框最大高度 |
| DialogPadding | Thickness | 24 | 对话框卡片内边距 |
| DialogCornerRadius | CornerRadius | 16 | 对话框卡片圆角 |
| SystemSound | EverythingDialogSound | None | 打开对话框时播放的系统音效 |
| SystemIcon | EverythingDialogIcon | None | 对话框顶部显示的系统图标 |
| IconSize | double | 48.0 | 系统图标尺寸 |

## 视觉样式

- **全屏遮罩**：控件铺满父容器，遮罩固定为半透明黑色 `#99000000`（不可自定义颜色）。
- **毛玻璃效果**：遮罩层使用 `BlurEffect`，可通过 `BlurRadius` 调整强度。
- **卡片承载**：对话框主体直接使用 `EverythingCard`（`CardVariant="Elevated"`），保持统一的圆角、阴影和内容布局。
- **图标布局**：`SystemIcon` 非 `None` 时，图标居中显示于内容上方；为 `None` 时自动折叠。

## 动画效果

- **打开动画**：遮罩淡入，对话框卡片淡入、放大并上移到中心。
- **关闭动画**：遮罩淡出，对话框卡片淡出、缩小并轻微下移，动画结束后再隐藏根层。

## 使用示例

```xml
<Grid>
    <Grid>
        <!-- 页面内容 -->
    </Grid>

    <everything:EverythingOverlayDialog IsOpen="{Binding IsDialogOpen}"
                                        DialogWidth="420">
        <StackPanel>
            <TextBlock Text="提示"
                       FontSize="22"
                       FontWeight="SemiBold"/>
            <TextBlock Text="这里是对话框内容。"
                       TextWrapping="Wrap"
                       Margin="0,12,0,20"/>
            <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                <everything:EverythingButton Text="关闭"/>
            </StackPanel>
        </StackPanel>
    </everything:EverythingOverlayDialog>
</Grid>
```

### 自定义尺寸与毛玻璃强度

```xml
<everything:EverythingOverlayDialog IsOpen="{Binding IsDialogOpen}"
                                    DialogWidth="520"
                                    DialogPadding="28"
                                    DialogCornerRadius="24"
                                    BlurRadius="24">
    <TextBlock Text="自定义对话框"/>
</everything:EverythingOverlayDialog>
```

### 系统音效与图标

- **SystemSound**：`None`（默认）、`Asterisk`、`Beep`、`Exclamation`、`Hand`、`Question`。`IsOpen` 变为 `True` 时立即播放对应系统音效。
- **SystemIcon**：`None`（默认）、`Information`、`Warning`、`Error`、`Question`。非 `None` 时在对话框内容上方居中显示对应的 Windows 系统图标。
- **IconSize**：控制系统图标的显示尺寸，默认 `48`。

```xml
<everything:EverythingOverlayDialog IsOpen="{Binding IsInfoOpen}"
                                    DialogWidth="380"
                                    SystemIcon="Information"
                                    SystemSound="Asterisk"
                                    IconSize="48">
    <StackPanel>
        <TextBlock Text="操作成功" FontSize="22" FontWeight="SemiBold" HorizontalAlignment="Center"/>
        <TextBlock Text="数据已保存。" TextWrapping="Wrap" TextAlignment="Center" Margin="0,12,0,20"/>
        <everything:EverythingButton Text="知道了" HorizontalAlignment="Center"/>
    </StackPanel>
</everything:EverythingOverlayDialog>
```

## 使用要点

- 建议放在页面根 `Grid` 的最后一个子元素，保证遮罩覆盖当前页面内容。
- 如果需要覆盖整个窗口，应放在窗口根布局的最后一层。
- 关闭逻辑可通过按钮事件或绑定 `IsOpen` 实现。

查看 [卡片控件文档](EverythingCard.md) 了解对话框内容容器的视觉样式。

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
