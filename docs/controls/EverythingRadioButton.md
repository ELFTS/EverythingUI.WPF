# EverythingRadioButton - 单选框控件

带渐变效果的单选框控件，支持自定义颜色和分组。

## 属性

| 属性                 | 类型     | 描述           |
| ------------------ | ------ | ------------ |
| GradientStartColor | Color  | 渐变起始颜色（默认蓝色） |
| GradientEndColor   | Color  | 渐变中间颜色（默认深蓝） |
| GroupName          | string | 分组名称         |
| IsChecked          | bool   | 是否选中         |

## 视觉样式

- **选中状态**：渐变背景，白色圆点
- **未选中状态**：透明背景，圆形边框

## 使用颜色资源

```xml
<!-- 默认蓝色 -->
<everything:EverythingRadioButton Content="选项 A" GroupName="Group1" IsChecked="True"/>

<!-- 使用预设颜色资源 -->
<everything:EverythingRadioButton Content="红色主题" GroupName="ColorGroup"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}"/>

<everything:EverythingRadioButton Content="绿色主题" GroupName="ColorGroup"
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}"/>

<everything:EverythingRadioButton Content="橙色主题" GroupName="ColorGroup"
    GradientStartColor="{StaticResource GradientOrangeStart}"
    GradientEndColor="{StaticResource GradientOrangeEnd}"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色资源。

## 单选组示例

```xml
<StackPanel>
    <TextBlock Text="请选择：" Margin="0,0,0,10"/>
    <everything:EverythingRadioButton Content="选项 A" GroupName="Group1" IsChecked="True"/>
    <everything:EverythingRadioButton Content="选项 B" GroupName="Group1"/>
    <everything:EverythingRadioButton Content="选项 C" GroupName="Group1"/>
</StackPanel>
```
