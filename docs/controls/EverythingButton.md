# EverythingButton - 多功能渐变按钮控件

支持垂直三色渐变、顶部光泽效果、自定义颜色和流畅动画。

## 属性

| 属性                 | 类型           | 描述           |
| ------------------ | ------------ | ------------ |
| CornerRadius       | CornerRadius | 圆角半径         |
| Icon               | object       | 图标内容         |
| IconPlacement      | Dock         | 图标位置         |
| GradientStartColor | Color        | 渐变起始颜色（上下位置） |
| GradientEndColor   | Color        | 渐变中间颜色       |

## 视觉效果

- **垂直三色渐变**：起始色 → 中间色 → 起始色
- **顶部光泽效果**：半个透明白色渐变叠加，增强立体感
- **阴影效果**：外阴影（悬停）和内阴影（按下）

## 动画效果

- **悬停动画**：按钮轻微放大（1.02倍）并显示外阴影
- **按下动画**：按钮缩小（0.98倍）并显示内阴影（顶部和左右）
- **过渡时间**：悬停0.2秒，按下0.1秒，释放0.15秒

## 使用颜色资源

```xml
<!-- 使用预设颜色资源（推荐） -->
<everything:EverythingButton Content="红色按钮"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}"/>

<everything:EverythingButton Content="绿色按钮"
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}"/>

<everything:EverythingButton Content="橙色按钮"
    GradientStartColor="{StaticResource GradientOrangeStart}"
    GradientEndColor="{StaticResource GradientOrangeEnd}"/>

<!-- 默认蓝色（无需指定颜色） -->
<everything:EverythingButton Content="默认按钮"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色资源。

## 自定义颜色

```xml
<!-- 使用自定义颜色值 -->
<everything:EverythingButton 
    Content="自定义颜色" 
    GradientStartColor="#FF5833" 
    GradientEndColor="#D43030"/>
```
