# EverythingTextBox - 文本框控件

带渐变边框效果的文本框控件，支持自定义焦点颜色和统一白色光泽层（GlossBrush）。

## 属性

| 属性                 | 类型   | 描述           |
| ------------------ | ---- | ------------ |
| Placeholder        | string | 占位符文本（默认空） |
| PlaceholderBrush   | Brush | 占位符颜色（默认灰色） |
| CornerRadius       | CornerRadius | 圆角半径（默认 6） |

## 视觉样式

- **默认状态**：灰色边框 + 内阴影渐变背景
- **悬停状态**：边框颜色加深，背景渐变调整
- **焦点状态**：主题色边框，背景渐变调整
- **禁用状态**：灰色背景，禁用文字颜色
- **圆角设计**：柔和的圆角

## 统一光泽层

本控件使用全局统一的白色光泽层资源 `GlossBrush`（定义在 `Styles/GradientColors.xaml`）：

- **渐变规格**：二段式半高，顶部 `#CCFFFFFF`(80%白) → 底部 `#33FFFFFF`(20%白)
- **实现方式**：通过 `{DynamicResource GlossBrush}` 引用，配合 `HalfHeightConverter` 实现高度减半
- **定位**：`VerticalAlignment="Top"` 顶部对齐
- **裁剪**：`ClipToBounds="True"` 防止溢出

## 使用示例

```xml
<!-- 默认文本框 -->
<everything:EverythingTextBox Placeholder="请输入内容..."/>

<!-- 带圆角的文本框 -->
<everything:EverythingTextBox Placeholder="圆角文本框"
    CornerRadius="12"/>

<!-- 自定义占位符颜色 -->
<everything:EverythingTextBox Placeholder="灰色占位符"
    PlaceholderBrush="Gray"/>
```
