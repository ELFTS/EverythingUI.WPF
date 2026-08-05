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

- **渐变轨道**：已填充部分使用垂直三色渐变 + 顶部白色光泽层（GlossBrush）
- **未填充轨道**：浅色背景（GlobalTrackBrush）
- **滑块**：圆角矩形 + 顶部半高白色光泽层，带柔和阴影

## 动画效果

- 悬停时滑块轻微放大

## 使用示例

```xml
<!-- 默认蓝色滑块 -->
<everything:EverythingSlider Minimum="0" Maximum="100" Value="50"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
