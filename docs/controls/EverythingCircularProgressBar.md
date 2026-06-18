# EverythingCircularProgressBar - 圆形进度条控件

圆形进度条控件，支持渐变效果和自定义尺寸。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Minimum | double | 0 | 最小值 |
| Maximum | double | 100 | 最大值 |
| Value | double | 0 | 当前值 |
| StrokeThickness | double | 8 | 线条粗细 |
| ShowPercentage | bool | false | 是否显示百分比 |

## 视觉样式

- **圆弧绘制方案**：使用 `ArcSegment` + `PathGeometry` 方案绘制进度圆弧
- **辅助类**：通过 `CircularArcHelper` 辅助类生成圆弧几何数据
- **渐变圆弧**：进度部分使用垂直三色渐变
- **圆形轨道**：完整的圆形背景轨道（`GlobalTrackBrush`）
- **中心区域**：可显示进度百分比
- **特殊处理**：
  - **0% 时**：隐藏进度路径（Visibility=Collapsed）
  - **99.9%-100% 时**：使用 `EllipseGeometry` 绘制完整闭合圆，避免 ArcSegment 在接近360度时的渲染缺陷

## 使用示例

```xml
<!-- 默认圆形进度条 -->
<everything:EverythingCircularProgressBar Value="65"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
