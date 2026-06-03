# EverythingRadioButton - 单选框控件

带渐变效果的单选框控件，支持自定义颜色、统一白色光泽层（GlossBrush）和分组。

## 属性

| 属性                 | 类型     | 描述           |
| ------------------ | ------ | ------------ |
| GroupName          | string | 分组名称         |
| IsChecked          | bool   | 是否选中         |
| ColorName          | ColorName | 颜色名称（默认 Blue） |
| BoxSize            | double | 单选框大小（默认 22） |
| DotBrush           | Brush  | 圆点标记颜色（默认白色） |

> **ColorManager 说明**：`ColorName` 属性由 `ColorManager` 静态类管理，通过附加属性实现颜色同步。

## 视觉样式

- **选中状态**：渐变背景，白色圆点，带弹性缩放动画
- **未选中状态**：透明背景，圆形边框
- **悬停状态**：边框颜色加深，背景渐变调整
- **按下状态**：边框颜色进一步加深
- **统一白色光泽层（GlossBrush）**：全局统一的白色光泽层资源，增强立体感

## 动画效果

- **选中动画**：圆点弹性缩放（0.3秒，ElasticEase，1次振荡）
- **背景过渡**：渐变背景淡入淡出（0.2秒，CubicEase）

## 统一光泽层

本控件使用全局统一的白色光泽层资源 `GlossBrush`（定义在 `Styles/GradientColors.xaml`）：

- **渐变规格**：二段式半高，顶部 `#CCFFFFFF`(80%白) → 底部 `#33FFFFFF`(20%白)
- **实现方式**：通过 `{DynamicResource GlossBrush}` 引用，配合 `HalfHeightConverter` 实现高度减半
- **定位**：`VerticalAlignment="Top"` 顶部对齐
- **裁剪**：`ClipToBounds="True"` 防止溢出

## 使用示例

```xml
<!-- 默认蓝色 -->
<everything:EverythingRadioButton Content="选项 A" GroupName="Group1" IsChecked="True"/>

<!-- 使用颜色名称 -->
<everything:EverythingRadioButton Content="红色主题" GroupName="ColorGroup" ColorName="Red"/>
<everything:EverythingRadioButton Content="绿色主题" GroupName="ColorGroup" ColorName="Green"/>
<everything:EverythingRadioButton Content="橙色主题" GroupName="ColorGroup" ColorName="Orange"/>
<everything:EverythingRadioButton Content="紫色主题" GroupName="ColorGroup" ColorName="Purple"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色。

## 单选组示例

```xml
<StackPanel>
    <TextBlock Text="请选择：" Margin="0,0,0,10"/>
    <everything:EverythingRadioButton Content="选项 A" GroupName="Group1" IsChecked="True"/>
    <everything:EverythingRadioButton Content="选项 B" GroupName="Group1"/>
    <everything:EverythingRadioButton Content="选项 C" GroupName="Group1"/>
</StackPanel>
```
