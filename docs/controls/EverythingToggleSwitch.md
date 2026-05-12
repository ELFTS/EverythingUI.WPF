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

- **开启状态**：渐变背景 + 顶部光泽效果，滑块在右侧
- **关闭状态**：灰色背景，滑块在左侧
- **滑块效果**：圆形滑块，带阴影
- **阴影效果**：轨道和滑块都带有柔和阴影

## 动画效果

- **切换动画**：滑块弹性滑动（0.4秒，ElasticEase）
- **背景渐变**：颜色平滑过渡（0.2秒）
- **弹性回弹**：滑块移动时带弹性回弹效果（2次振荡）

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
