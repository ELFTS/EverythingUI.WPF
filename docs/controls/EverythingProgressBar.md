# EverythingProgressBar - 进度条控件

带渐变效果的进度条控件，支持自定义颜色和动画。

## 属性

| 属性                 | 类型   | 描述           |
| ------------------ | ---- | ------------ |
| Minimum            | double | 最小值（默认 0）  |
| Maximum            | double | 最大值（默认 100）|
| Value              | double | 当前值          |
| ColorName          | ColorName | 颜色名称（默认 Blue） |
| CornerRadius       | CornerRadius | 圆角半径（默认 6） |
| ShowPercentage     | bool | 是否显示百分比（默认 false） |
| AnimationDuration  | Duration | 动画持续时间（默认 300ms） |

## 视觉样式

- **渐变填充**：进度部分使用垂直三色渐变
- **轨道背景**：未填充部分使用灰色背景（`GlobalTrackBrush`）
- **圆角设计**：两端圆角，视觉更柔和
- **阴影效果**：进度条带有柔和阴影
- **无光泽层**：进度条控件不使用白色光泽层效果（线性进度条不需要光泽增强）

## 动画效果

- **宽度变化动画**：进度值变化时宽度平滑过渡（默认300ms，CubicEase）

## 使用示例

```xml
<!-- 默认蓝色进度条 -->
<everything:EverythingProgressBar Value="65"/>

<!-- 使用颜色名称 -->
<everything:EverythingProgressBar Value="75" ColorName="Green"/>
<everything:EverythingProgressBar Value="45" ColorName="Red"/>
<everything:EverythingProgressBar Value="90" ColorName="Orange"/>
<everything:EverythingProgressBar Value="50" ColorName="Purple"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色。
