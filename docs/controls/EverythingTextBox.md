# EverythingTextBox - 文本框控件

带拟物化内阴影效果的文本框控件，支持占位符、自定义圆角和焦点边框。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Placeholder | string | 空字符串 | 占位符文本 |
| PlaceholderBrush | Brush | 灰色 | 占位符颜色（模板未绑定，占位符实际使用 `TextSecondaryBrush` 动态资源） |
| CornerRadius | CornerRadius | 6 | 圆角半径 |

## 视觉样式

- **默认状态**：灰色边框 + 垂直渐变内层背景，三层模糊矩形模拟真实内阴影（顶部、左侧、右侧）
- **悬停**：边框不变，内层渐变加深
- **焦点**：边框切换为主题焦点色，内层渐变进一步加深
- **占位符**：文本为空时显示，使用次要文字色
- 文本框已移除统一白色光泽层，仅靠内层渐变与内阴影营造立体感

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
