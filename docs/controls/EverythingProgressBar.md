# EverythingProgressBar - 进度条控件

带渐变效果的进度条控件，支持自定义颜色、光泽层、扫光动画和阻力感宽度动画。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Value | double | 0 | 当前进度值 |
| CornerRadius | CornerRadius | 6 | 圆角半径 |
| ShowPercentage | bool | false | 是否显示百分比 |
| AnimationDuration | Duration | 0:0:0.4 | 阻力感宽度动画持续时间 |

## 视觉样式

- **渐变填充**：进度部分使用垂直三色渐变 + 统一白色光泽层（GlossBrush）
- **轨道背景**：未填充部分使用灰色背景（`GlobalTrackBrush`）
- **圆角设计**：两端圆角，视觉更柔和
- **阴影效果**：进度条带有柔和阴影（BlurRadius: 6, Opacity: 0.3）

## 动画效果

- **阻力感宽度动画**：进度值变化时通过 `BackEase EaseOut` 驱动宽度变化（默认 400ms，带轻微过冲回弹）
- **扫光循环动画**：水平方向循环扫过的光线动画（2.8秒/周期），光线从左到右反复扫过进度条区域，`Value >= Maximum` 时自动隐藏扫光

## 使用示例

```xml
<everything:EverythingProgressBar Value="65"/>

<!-- 显示百分比 -->
<everything:EverythingProgressBar Value="80" ShowPercentage="True"/>

<!-- 自定义动画时长 -->
<everything:EverythingProgressBar Value="50" AnimationDuration="0:0:0.5"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
