# EverythingToggleSwitch - 开关控件

拟物化风格的开关控件，支持渐变效果和流畅动画。

## 属性

| 属性                 | 类型    | 描述           |
| ------------------ | ----- | ------------ |
| IsChecked          | bool  | 是否选中         |
| ColorName          | ColorName | 颜色名称（默认 Blue） |
| SwitchWidth        | double | 开关宽度（默认 50） |
| SwitchHeight       | double | 开关高度（默认 26） |
| ThumbSize          | double | 滑块大小（默认 22） |
| UncheckedBackground | Brush | 关闭状态背景色（默认 #CCCCCC） |
| ThumbBrush         | Brush | 滑块颜色（默认白色） |

## 视觉样式

- **开启状态**：渐变背景 + 统一白色光泽层（GlossBrush），滑块在右侧
- **关闭状态**：灰色背景，滑块在左侧
- **滑块效果**：圆形滑块，带阴影
- **阴影效果**：轨道和滑块都带有柔和阴影

## 动画效果

- **切换动画**：滑块弹性滑动（0.4秒，ElasticEase，2次振荡）
- **背景渐变**：颜色平滑过渡（0.2秒，CubicEase）
- **轨道阴影**：轨道带有柔和阴影效果

## 统一光泽层

本控件使用全局统一的白色光泽层资源 `GlossBrush`（定义在 `Styles/GradientColors.xaml`）：

- **渐变规格**：二段式半高，顶部 `#CCFFFFFF`(80%白) → 底部 `#33FFFFFF`(20%白)
- **实现方式**：通过 `{DynamicResource GlossBrush}` 引用，配合 `HalfHeightConverter` 实现高度减半
- **定位**：`VerticalAlignment="Top"` 顶部对齐
- **裁剪**：`ClipToBounds="True"` 防止溢出

## 使用示例

```xml
<!-- 默认蓝色开关 -->
<everything:EverythingToggleSwitch IsChecked="True"/>

<!-- 使用颜色名称 -->
<everything:EverythingToggleSwitch IsChecked="True" ColorName="Green"/>
<everything:EverythingToggleSwitch IsChecked="True" ColorName="Red"/>
<everything:EverythingToggleSwitch IsChecked="True" ColorName="Orange"/>
<everything:EverythingToggleSwitch IsChecked="True" ColorName="Purple"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色。
