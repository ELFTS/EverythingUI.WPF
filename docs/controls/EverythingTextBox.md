# EverythingTextBox - 文本框控件

带拟物化内阴影效果的文本框控件，支持占位符、自定义圆角和焦点边框。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Placeholder | string | null | 占位符文本 |
| PlaceholderBrush | Brush | 灰色 | 占位符颜色 |
| CornerRadius | CornerRadius | 6 | 圆角半径 |

## 视觉样式

- **默认状态**：灰色边框（Gray400）+ 垂直浅色背景 + 柔和内阴影
- **内阴影效果**：顶部、左侧、右侧使用模糊黑色阴影源，裁剪后形成真实内阴影
- **悬停状态**：内层背景略微加深，边框保持不变
- **焦点状态**：主题色边框（BorderFocusBrush），内层背景调整
- **圆角设计**：柔和的圆角（默认 6px）
- **最小高度**：40px
- **无光泽层**：文本框已移除统一白色光泽层，避免与内阴影叠加造成白边

## 使用示例

```xml
<!-- 默认文本框 -->
<everything:EverythingTextBox Placeholder="请输入内容..."/>

<!-- 带圆角的文本框 -->
<everything:EverythingTextBox Placeholder="圆角文本框" CornerRadius="12"/>

<!-- 自定义占位符颜色 -->
<everything:EverythingTextBox Placeholder="灰色占位符" PlaceholderBrush="Gray"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
