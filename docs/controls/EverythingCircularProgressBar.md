# EverythingCircularProgressBar - 圆形进度条控件

圆形进度条控件，支持渐变圆弧、自定义尺寸、百分比显示和阻力感进度动画。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Minimum | double | 0 | 最小值 |
| Maximum | double | 100 | 最大值 |
| Value | double | 0 | 当前目标值（变化时启动阻力感动画） |
| StrokeThickness | double | 8 | 线条粗细 |
| ShowPercentage | bool | false | 是否显示百分比 |
| AnimationDuration | Duration | 0:0:0.4 (400ms) | 阻力感进度动画持续时间 |

## 视觉样式

- **圆弧绘制**：使用 `CircularArcHelper` 生成圆弧几何，起点位于 12 点钟方向，顺时针绘制，端点圆头
- **渐变圆弧**：进度部分使用 `PrimaryVerticalBrush`（垂直三段渐变，跟随主题色）
- **圆形轨道**：完整的圆形背景轨道（`GlobalTrackBrush`）
- **中心百分比**：居中显示，格式 `{0:F0}%`
- **100% 时**：返回完整闭合圆，避免弧线接近 360 度时的渲染缺陷
- **0% 时**：隐藏进度路径

## 动画效果

- **阻力感进度动画**：`Value` 变化时通过 `AnimatedValue` 逐帧更新圆弧几何与百分比文本，带轻微过冲回弹（默认 400ms）
- **100% 显示完整圆**：动画到达 100% 时显示完整闭合圆

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
