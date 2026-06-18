# EverythingSlider - 滑块控件

自定义样式的滑块控件，支持渐变轨道和自定义滑块颜色。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Minimum | double | 0 | 最小值 |
| Maximum | double | 100 | 最大值 |
| Value | double | 0 | 当前值 |
| IsSnapToTickEnabled | bool | false | 是否吸附到刻度 |
| TickFrequency | double | 1 | 刻度频率 |
| TickPlacement | TickPlacement | None | 刻度位置 |

## 视觉样式

- **渐变轨道**：已填充部分使用垂直三色渐变 + 统一白色光泽层（GlossBrush）
- **滑块样式**：圆角矩形滑块 + 统一白色光泽层（GlossBrush），带阴影
- **轨道样式**：未填充部分使用灰色背景（`GlobalTrackBrush`）
- **阴影效果**：滑块带有柔和阴影，悬停时阴影加深

> 轨道和滑块两处都有光泽层，轨道填满高度、滑块固定8px半高。

## 动画效果

- **悬停动画**：滑块放大（1.15倍），阴影加深（BlurRadius: 6→10, Opacity: 0.35→0.5）
- **过渡时间**：0.15秒，CubicEase 缓动

## 使用示例

```xml
<!-- 默认蓝色滑块 -->
<everything:EverythingSlider Minimum="0" Maximum="100" Value="50"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
