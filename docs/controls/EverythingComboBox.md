# EverythingComboBox - 组合框控件

带渐变效果的组合框控件，支持下拉列表自定义样式。

## 属性

| 属性                 | 类型   | 描述           |
| ------------------ | ---- | ------------ |
| GradientStartColor | Color | 渐变起始颜色（默认蓝色） |
| GradientEndColor   | Color | 渐变中间颜色（默认深蓝） |
| SelectedIndex      | int   | 选中项索引        |
| SelectedItem       | object | 当前选中项       |

## 视觉样式

- **下拉按钮**：渐变背景，白色箭头图标
- **选中项**：渐变高亮效果
- **悬停项**：浅灰色背景

## 使用颜色资源

```xml
<!-- 默认蓝色组合框 -->
<everything:EverythingComboBox SelectedIndex="0">
    <ComboBoxItem Content="选项 1"/>
    <ComboBoxItem Content="选项 2"/>
    <ComboBoxItem Content="选项 3"/>
</everything:EverythingComboBox>

<!-- 使用预设颜色资源 -->
<everything:EverythingComboBox SelectedIndex="0"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}">
    <ComboBoxItem Content="红色主题"/>
    <ComboBoxItem Content="选项 2"/>
    <ComboBoxItem Content="选项 3"/>
</everything:EverythingComboBox>

<everything:EverythingComboBox SelectedIndex="0"
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}">
    <ComboBoxItem Content="绿色主题"/>
    <ComboBoxItem Content="选项 2"/>
    <ComboBoxItem Content="选项 3"/>
</everything:EverythingComboBox>

<everything:EverythingComboBox SelectedIndex="0"
    GradientStartColor="{StaticResource GradientPurpleStart}"
    GradientEndColor="{StaticResource GradientPurpleEnd}">
    <ComboBoxItem Content="紫色主题"/>
    <ComboBoxItem Content="选项 2"/>
    <ComboBoxItem Content="选项 3"/>
</everything:EverythingComboBox>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色资源。
