# EverythingTextBox - 文本框控件

带渐变边框效果的文本框控件，支持自定义焦点颜色。

## 属性

| 属性                 | 类型   | 描述           |
| ------------------ | ---- | ------------ |
| Placeholder        | string | 占位符文本（默认空） |
| PlaceholderBrush   | Brush | 占位符颜色（默认灰色） |
| CornerRadius       | CornerRadius | 圆角半径（默认 6） |

## 视觉样式

- **默认状态**：灰色边框
- **焦点状态**：渐变边框，带光泽效果
- **圆角设计**：柔和的圆角

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
