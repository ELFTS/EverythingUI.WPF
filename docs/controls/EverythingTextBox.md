# EverythingTextBox - 文本框控件

带渐变边框效果的文本框控件，支持自定义焦点颜色。

## 属性

| 属性                 | 类型   | 描述           |
| ------------------ | ---- | ------------ |
| GradientStartColor | Color | 渐变起始颜色（默认蓝色） |
| GradientEndColor   | Color | 渐变中间颜色（默认深蓝） |
| CornerRadius       | CornerRadius | 圆角半径       |
| PlaceholderText    | string | 占位符文本       |

## 视觉样式

- **默认状态**：灰色边框
- **焦点状态**：渐变边框，带光泽效果
- **圆角设计**：柔和的圆角

## 使用颜色资源

```xml
<!-- 默认蓝色文本框 -->
<everything:EverythingTextBox PlaceholderText="请输入内容..."/>

<!-- 使用预设颜色资源 -->
<everything:EverythingTextBox PlaceholderText="红色主题"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}"/>

<everything:EverythingTextBox PlaceholderText="绿色主题"
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}"/>

<everything:EverythingTextBox PlaceholderText="紫色主题"
    GradientStartColor="{StaticResource GradientPurpleStart}"
    GradientEndColor="{StaticResource GradientPurpleEnd}"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色资源。
