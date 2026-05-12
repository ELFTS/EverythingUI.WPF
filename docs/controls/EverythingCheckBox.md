# EverythingCheckBox - 复选框控件

带渐变效果的复选框控件，支持自定义颜色和三种状态。

## 属性

| 属性                 | 类型          | 描述           |
| ------------------ | ----------- | ------------ |
| IsChecked          | bool?       | 是否选中（支持三态）   |
| IsThreeState       | bool        | 是否启用三态       |
| ColorName          | ColorName   | 颜色名称（默认 Blue） |
| BoxSize            | double      | 复选框大小（默认 22） |
| CornerRadius       | CornerRadius | 圆角半径（默认 6） |
| CheckMarkBrush     | Brush       | 勾选标记颜色（默认白色） |

## 视觉样式

- **选中状态**：渐变背景，白色勾选图标
- **未选中状态**：透明背景，边框显示
- **不确定状态**：渐变背景，白色横线

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
