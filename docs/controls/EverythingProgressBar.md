# EverythingProgressBar - 进度条控件

带渐变效果的进度条控件，支持自定义颜色和动画。

## 属性

| 属性                 | 类型   | 描述           |
| ------------------ | ---- | ------------ |
| GradientStartColor | Color | 渐变起始颜色（默认蓝色） |
| GradientEndColor   | Color | 渐变中间颜色（默认深蓝） |

## 视觉样式

- **渐变填充**：进度部分使用垂直三色渐变
- **轨道背景**：未填充部分使用灰色背景
- **圆角设计**：两端圆角，视觉更柔和

## 使用颜色资源

```xml
<!-- 默认蓝色进度条 -->
<everything:EverythingProgressBar Value="65"/>

<!-- 使用预设颜色资源 -->
<everything:EverythingProgressBar Value="75"
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}"/>

<everything:EverythingProgressBar Value="45"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}"/>

<everything:EverythingProgressBar Value="90"
    GradientStartColor="{StaticResource GradientOrangeStart}"
    GradientEndColor="{StaticResource GradientOrangeEnd}"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色资源。
