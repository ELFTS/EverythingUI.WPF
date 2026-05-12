# EverythingRadioButton - 单选框控件

带渐变效果的单选框控件，支持自定义颜色和分组。

## 属性

| 属性                 | 类型     | 描述           |
| ------------------ | ------ | ------------ |
| GroupName          | string | 分组名称         |
| IsChecked          | bool   | 是否选中         |
| ColorName          | ColorName | 颜色名称（默认 Blue） |
| BoxSize            | double | 单选框大小（默认 22） |
| DotBrush           | Brush  | 圆点标记颜色（默认白色） |

## 视觉样式

- **选中状态**：渐变背景，白色圆点
- **未选中状态**：透明背景，圆形边框

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
