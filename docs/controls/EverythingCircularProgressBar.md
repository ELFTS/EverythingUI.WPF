# EverythingCircularProgressBar - 圆形进度条控件

圆形进度条控件，支持渐变效果和自定义尺寸。

## 属性

| 属性                 | 类型   | 描述           |
| ------------------ | ---- | ------------ |
| GradientStartColor | Color | 渐变起始颜色（默认蓝色） |
| GradientEndColor   | Color | 渐变中间颜色（默认深蓝） |
| StrokeThickness    | double | 线条粗细（默认8）   |

## 视觉样式

- **渐变圆弧**：进度部分使用渐变色
- **圆形轨道**：完整的圆形背景轨道
- **中心区域**：可显示进度百分比

## 使用颜色资源

```xml
<!-- 默认圆形进度条 -->
<everything:EverythingCircularProgressBar Value="65"/>

<!-- 使用预设颜色资源 -->
<everything:EverythingCircularProgressBar Value="80" Width="120" Height="120"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}"/>

<everything:EverythingCircularProgressBar Value="60" Width="100" Height="100"
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}"/>

<everything:EverythingCircularProgressBar Value="90" Width="150" Height="150"
    StrokeThickness="10"
    GradientStartColor="{StaticResource GradientPurpleStart}"
    GradientEndColor="{StaticResource GradientPurpleEnd}"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色资源。
