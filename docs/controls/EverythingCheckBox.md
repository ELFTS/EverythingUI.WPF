# EverythingCheckBox - 复选框控件

带渐变效果的复选框控件，支持自定义颜色、统一白色光泽层（GlossBrush）和三种状态。

## 属性

| 属性                 | 类型          | 描述           |
| ------------------ | ----------- | ------------ |
| IsChecked          | bool?       | 是否选中（支持三态）   |
| IsThreeState       | bool        | 是否启用三态       |
| ColorName          | ColorName   | 颜色名称（默认 Blue） |
| BoxSize            | double      | 复选框大小（默认 22） |
| CornerRadius       | CornerRadius | 圆角半径（默认 6） |
| CheckMarkBrush     | Brush       | 勾选标记颜色（默认白色） |

> **ColorManager 说明**：`ColorName` 属性由 `ColorManager` 静态类管理，通过附加属性实现颜色同步。

## 视觉样式

- **选中状态**：渐变背景，白色勾选图标，带弹性缩放动画
- **未选中状态**：透明背景，边框显示
- **不确定状态**：渐变背景，白色横线，带缩放动画
- **悬停状态**：边框颜色加深，背景渐变调整
- **按下状态**：边框颜色进一步加深
- **统一白色光泽层（GlossBrush）**：全局统一的白色光泽层资源，增强立体感

## 动画效果

- **选中动画**：勾选标记弹性缩放（0.3秒，ElasticEase，1次振荡）
- **不确定动画**：横线缩放展开（0.2秒，CubicEase）
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
<everything:EverythingCheckBox Content="默认蓝色" IsChecked="True"/>

<!-- 使用颜色名称 -->
<everything:EverythingCheckBox Content="红色主题" IsChecked="True" ColorName="Red"/>
<everything:EverythingCheckBox Content="绿色主题" IsChecked="True" ColorName="Green"/>
<everything:EverythingCheckBox Content="橙色主题" IsChecked="True" ColorName="Orange"/>
<everything:EverythingCheckBox Content="紫色主题" IsChecked="True" ColorName="Purple"/>
<everything:EverythingCheckBox Content="粉色主题" IsChecked="True" ColorName="Pink"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色。

## 三态复选框

```xml
<everything:EverythingCheckBox 
    Content="三态选项"
    IsThreeState="True"
    IsChecked="{x:Null}"/>
```
