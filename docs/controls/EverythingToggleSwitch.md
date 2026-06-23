# EverythingToggleSwitch - 开关控件

拟物化风格的开关控件，支持渐变效果和流畅动画。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| IsChecked | bool | false | 是否选中 |
| SwitchWidth | double | 50 | 开关宽度 |
| SwitchHeight | double | 26 | 开关高度 |
| ThumbSize | double | 22 | 滑块大小 |
| UncheckedBackground | Brush | #CCCCCC | 关闭状态背景色 |
| ThumbBrush | Brush | 白色 | 滑块颜色 |

## 视觉样式

- **开启状态**：渐变背景 + 统一白色光泽层（GlossBrush），滑块在右侧
- **关闭状态**：灰色背景，滑块在左侧
- **滑块效果**：圆形滑块，带阴影
- **阴影效果**：轨道和滑块都带有柔和阴影，开启状态下轨道阴影保持显示

## 动画效果

- **切换动画**：滑块弹性滑动（0.4秒，ElasticEase，2次振荡）
- **背景渐变**：颜色平滑过渡（0.2秒，CubicEase）
- **轨道阴影**：独立阴影层始终保留，开启状态下不会因背景切换而消失

## 使用示例

```xml
<!-- 默认蓝色开关 -->
<everything:EverythingToggleSwitch IsChecked="True"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
