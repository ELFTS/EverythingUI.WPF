# EverythingCircularProgressBar - 圆形进度条控件

圆形进度条控件，支持渐变圆弧、自定义尺寸、百分比显示和阻力感进度动画。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Minimum | double | 0 | 最小值 |
| Maximum | double | 100 | 最大值 |
| Value | double | 0 | 当前值 |
| AnimatedValue | double | 0 | 动画中的当前值（只读使用，用于模板显示） |
| StrokeThickness | double | 8 | 线条粗细 |
| ShowPercentage | bool | false | 是否显示百分比 |
| AnimationDuration | Duration | 0:0:0.4 | 阻力感进度动画持续时间 |

## 视觉样式

- **圆弧绘制方案**：控件代码逐帧生成 `PathGeometry`，模板只负责显示 `ProgressPath`
- **辅助类**：通过 `CircularArcHelper` 根据 `AnimatedValue`、范围、尺寸和线宽生成圆弧几何
- **渐变圆弧**：进度部分使用垂直三色渐变
- **圆形轨道**：完整的圆形背景轨道（`GlobalTrackBrush`）
- **中心区域**：可显示进度百分比，文本绑定到 `AnimatedValue`
- **特殊处理**：
  - **0% 时**：隐藏进度路径（Visibility=Collapsed）
  - **100% 时**：使用 `EllipseGeometry` 绘制完整闭合圆，避免 `ArcSegment` 接近 360 度时的渲染缺陷

## 动画效果

- **阻力感进度动画**：`Value` 变化时通过 `CompositionTarget.Rendering` 逐帧更新 `AnimatedValue`，使用 EaseOutBack 曲线产生轻微过冲回弹
- **模板同步刷新**：每帧根据 `AnimatedValue` 重建圆弧 Geometry，弧线和百分比文本同步变化
- **生命周期管理**：控件卸载时停止渲染回调，避免后台继续刷新

## 使用示例

```xml
<!-- 默认圆形进度条 -->
<everything:EverythingCircularProgressBar Value="65"/>

<!-- 显示百分比 -->
<everything:EverythingCircularProgressBar Value="80" ShowPercentage="True"/>

<!-- 自定义尺寸、线宽和动画时长 -->
<everything:EverythingCircularProgressBar Value="50"
                                      Width="120"
                                      Height="120"
                                      StrokeThickness="10"
                                      AnimationDuration="0:0:0.6"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
