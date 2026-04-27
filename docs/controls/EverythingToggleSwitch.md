# EverythingToggleSwitch - 开关控件

拟物化风格的开关控件，支持渐变效果和流畅动画。

## 属性

| 属性                 | 类型    | 描述           |
| ------------------ | ----- | ------------ |
| GradientStartColor | Color | 渐变起始颜色（默认蓝色） |
| GradientEndColor   | Color | 渐变中间颜色（默认深蓝） |
| IsChecked          | bool  | 是否选中         |

## 视觉样式

- **开启状态**：渐变背景，滑块在右侧
- **关闭状态**：灰色背景，滑块在左侧
- **滑块效果**：圆形滑块，带阴影

## 动画效果

- **切换动画**：滑块平滑移动（0.2秒）
- **背景渐变**：颜色平滑过渡

## 使用颜色资源

```xml
<!-- 默认蓝色开关 -->
<everything:EverythingToggleSwitch IsChecked="True"/>

<!-- 使用预设颜色资源 -->
<everything:EverythingToggleSwitch IsChecked="True"
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}"/>

<everything:EverythingToggleSwitch IsChecked="True"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}"/>

<everything:EverythingToggleSwitch IsChecked="True"
    GradientStartColor="{StaticResource GradientOrangeStart}"
    GradientEndColor="{StaticResource GradientOrangeEnd}"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色资源。
