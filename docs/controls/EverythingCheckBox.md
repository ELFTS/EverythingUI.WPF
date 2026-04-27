# EverythingCheckBox - 复选框控件

带渐变效果的复选框控件，支持自定义颜色和三种状态。

## 属性

| 属性                 | 类型          | 描述           |
| ------------------ | ----------- | ------------ |
| GradientStartColor | Color       | 渐变起始颜色（默认蓝色） |
| GradientEndColor   | Color       | 渐变中间颜色（默认深蓝） |
| IsChecked          | bool?       | 是否选中（支持三态）   |
| IsThreeState       | bool        | 是否启用三态       |

## 视觉样式

- **选中状态**：渐变背景，白色勾选图标
- **未选中状态**：透明背景，边框显示
- **不确定状态**：渐变背景，白色横线

## 使用颜色资源

```xml
<!-- 默认蓝色 -->
<everything:EverythingCheckBox Content="默认蓝色" IsChecked="True"/>

<!-- 使用预设颜色资源 -->
<everything:EverythingCheckBox Content="红色主题" IsChecked="True"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}"/>

<everything:EverythingCheckBox Content="绿色主题" IsChecked="True"
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}"/>

<everything:EverythingCheckBox Content="橙色主题" IsChecked="True"
    GradientStartColor="{StaticResource GradientOrangeStart}"
    GradientEndColor="{StaticResource GradientOrangeEnd}"/>

<everything:EverythingCheckBox Content="紫色主题" IsChecked="True"
    GradientStartColor="{StaticResource GradientPurpleStart}"
    GradientEndColor="{StaticResource GradientPurpleEnd}"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色资源。

## 三态复选框

```xml
<everything:EverythingCheckBox 
    Content="三态选项"
    IsThreeState="True"
    IsChecked="{x:Null}"/>
```
