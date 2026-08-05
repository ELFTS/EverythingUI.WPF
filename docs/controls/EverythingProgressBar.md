# EverythingProgressBar - 进度条控件

带渐变效果的进度条控件，支持自定义颜色、光泽层、扫光动画和阻力感宽度动画。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Value | double | 0 | 当前进度值（继承自 ProgressBar） |
| Minimum | double | 0 | 最小值（继承自 ProgressBar） |
| Maximum | double | 100 | 最大值（继承自 ProgressBar） |
| CornerRadius | CornerRadius | 6 | 圆角半径 |
| ShowPercentage | bool | false | 是否显示百分比文本 |
| AnimationDuration | Duration | 0:0:0.4 (400ms) | 阻力感宽度动画持续时间 |

## 视觉样式

- **进度填充**：主题色垂直渐变（`PrimaryVerticalBrush`）
- **光泽层**：顶部半高白色光泽层（`GlossBrush`）
- **轨道**：未填充部分使用 `GlobalTrackBrush`
- **圆角**：两端圆角，视觉柔和
- **阴影**：进度条带柔和阴影
- **百分比文本**：居中显示，格式 `{0:F0}%`

## 动画效果

- **阻力感宽度动画**：进度值变化时进度宽度带轻微过冲回弹（默认 400ms）
- **扫光循环动画**：水平方向循环扫过的光线动画
- **完成态隐藏扫光**：当 `Value >= Maximum` 时，扫光效果自动隐藏

## 使用示例

```xml
<everything:EverythingProgressBar Value="65"/>

<!-- 显示百分比 -->
<everything:EverythingProgressBar Value="80" ShowPercentage="True"/>

<!-- 自定义动画时长 -->
<everything:EverythingProgressBar Value="50" AnimationDuration="0:0:0.5"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
