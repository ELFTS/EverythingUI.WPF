# EverythingTextBox - 文本框控件

带渐变边框效果的文本框控件，支持自定义焦点颜色和统一白色光泽层（GlossBrush）。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Placeholder | string | null | 占位符文本 |
| PlaceholderBrush | Brush | 灰色 | 占位符颜色 |
| CornerRadius | CornerRadius | 6 | 圆角半径 |

## 视觉样式

- **默认状态**：灰色边框（Gray400）+ 内阴影渐变背景
- **悬停状态**：内阴影渐变加深，边框保持不变
- **焦点状态**：主题色边框（BorderFocusBrush），内阴影渐变调整
- **禁用状态**：浅灰背景（Gray100），灰色边框（Gray300），光泽层隐藏
- **圆角设计**：柔和的圆角（默认 6px）
- **最小高度**：40px
- **统一白色光泽层（GlossBrush）**：全局统一的白色光泽层资源，增强立体感

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
