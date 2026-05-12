# EverythingCircularProgressBar - 圆形进度条控件

圆形进度条控件，支持渐变效果和自定义尺寸。

## 属性

| 属性                 | 类型   | 描述           |
| ------------------ | ---- | ------------ |
| Minimum            | double | 最小值（默认 0）  |
| Maximum            | double | 最大值（默认 100）|
| Value              | double | 当前值          |
| ColorName          | ColorName | 颜色名称（默认 Blue） |
| StrokeThickness    | double | 线条粗细（默认 8） |
| ShowPercentage     | bool | 是否显示百分比（默认 false） |
| AnimationDuration  | Duration | 动画持续时间（默认 300ms） |
| AnimatedAngle      | double | 当前动画角度（只读） |

## 视觉样式

- **渐变圆弧**：进度部分使用渐变色
- **圆形轨道**：完整的圆形背景轨道
- **中心区域**：可显示进度百分比

## 使用示例

```xml
<!-- 默认圆形进度条 -->
<everything:EverythingCircularProgressBar Value="65"/>

<!-- 使用颜色名称 -->
<everything:EverythingCircularProgressBar Value="80" ColorName="Red"/>
<everything:EverythingCircularProgressBar Value="60" ColorName="Green"/>
<everything:EverythingCircularProgressBar Value="90" ColorName="Purple"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色。
