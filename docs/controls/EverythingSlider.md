# EverythingSlider - 滑块控件

自定义样式的滑块控件，支持渐变轨道和自定义滑块颜色。

## 属性

| 属性                 | 类型   | 描述                |
| ------------------ | ---- | ----------------- |
| GradientStartColor | Color | 渐变起始颜色（默认蓝色）      |
| GradientEndColor   | Color | 渐变中间颜色（默认深蓝）      |

## 视觉样式

- **渐变轨道**：已填充部分使用垂直三色渐变
- **滑块样式**：圆形滑块，带阴影效果
- **轨道样式**：未填充部分使用灰色背景

## 使用颜色资源

```xml
<!-- 默认蓝色滑块 -->
<everything:EverythingSlider Minimum="0" Maximum="100" Value="50"/>

<!-- 使用预设颜色资源 -->
<everything:EverythingSlider Minimum="0" Maximum="100" Value="30"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}"/>

<everything:EverythingSlider Minimum="0" Maximum="100" Value="60"
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}"/>

<everything:EverythingSlider Minimum="0" Maximum="100" Value="75"
    GradientStartColor="{StaticResource GradientOrangeStart}"
    GradientEndColor="{StaticResource GradientOrangeEnd}"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色资源。
