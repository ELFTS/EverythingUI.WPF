# EverythingSlider - 滑块控件

自定义样式的滑块控件，支持渐变轨道和自定义滑块颜色。

## 属性

| 属性                 | 类型   | 描述                |
| ------------------ | ---- | ----------------- |
| Minimum            | double | 最小值（默认 0）      |
| Maximum            | double | 最大值（默认 100）    |
| Value              | double | 当前值              |
| ColorName          | ColorName | 颜色名称（默认 Blue） |
| IsSnapToTickEnabled | bool | 是否吸附到刻度（默认 false） |
| TickFrequency      | double | 刻度频率（默认 1）    |
| TickPlacement      | TickPlacement | 刻度位置（默认 None） |

## 视觉样式

- **渐变轨道**：已填充部分使用垂直三色渐变 + 统一白色光泽层（GlossBrush）
- **滑块样式**：圆角矩形滑块，带阴影和统一白色光泽层（GlossBrush）
- **轨道样式**：未填充部分使用灰色背景
- **阴影效果**：滑块带有柔和阴影，悬停时阴影加深

## 动画效果

- **悬停动画**：滑块放大（1.15倍），阴影加深（BlurRadius: 6→10, Opacity: 0.35→0.5）
- **过渡时间**：0.15秒，CubicEase 缓动

## 轨道样式

- **已填充部分**：垂直三色渐变 + 统一白色光泽层（GlossBrush）
- **未填充部分**：灰色轨道背景（`GlobalTrackBrush`）
- **滑块样式**：圆角矩形，垂直三色渐变 + 阴影 + 光泽层

## 统一光泽层

本控件使用全局统一的白色光泽层资源 `GlossBrush`（定义在 `Styles/GradientColors.xaml`）：

- **渐变规格**：二段式半高，顶部 `#CCFFFFFF`(80%白) → 底部 `#33FFFFFF`(20%白)
- **实现方式**：通过 `{DynamicResource GlossBrush}` 引用，配合 `HalfHeightConverter` 实现高度减半
- **定位**：`VerticalAlignment="Top"` 顶部对齐
- **裁剪**：`ClipToBounds="True"` 防止溢出

> **Slider 特殊说明**：轨道和滑块两处都有光泽层，轨道填满高度、滑块固定8px半高。

## 使用示例

```xml
<!-- 默认蓝色滑块 -->
<everything:EverythingSlider Minimum="0" Maximum="100" Value="50"/>

<!-- 使用颜色名称 -->
<everything:EverythingSlider Value="30" ColorName="Red"/>
<everything:EverythingSlider Value="60" ColorName="Green"/>
<everything:EverythingSlider Value="75" ColorName="Orange"/>
<everything:EverythingSlider Value="45" ColorName="Purple"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色。
