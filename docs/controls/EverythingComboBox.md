# EverythingComboBox - 组合框控件

带渐变效果的组合框控件，支持下拉列表自定义样式。

## 属性

| 属性                 | 类型   | 描述           |
| ------------------ | ---- | ------------ |
| SelectedIndex      | int   | 选中项索引        |
| SelectedItem       | object | 当前选中项       |
| ItemsSource        | IEnumerable | 数据源      |
| ColorName          | ColorName | 颜色名称（默认 Blue） |
| Placeholder        | string | 占位符文本       |
| PlaceholderBrush   | Brush | 占位符颜色（默认白色） |
| CornerRadius       | CornerRadius | 圆角半径（默认 6） |

## 视觉样式

- **下拉按钮**：渐变背景 + 统一白色光泽层（GlossBrush），白色箭头图标，带阴影效果
- **选中项**：渐变高亮效果
- **悬停项**：浅灰色背景
- **阴影效果**：主按钮带有柔和阴影，悬停时阴影加深

## 动画效果

- **悬停动画**：按钮轻微放大（1.02倍），阴影加深（BlurRadius: 8→12, Opacity: 0.2→0.3）
- **按下动画**：按钮缩小（0.98倍），阴影深度减小（2→1）
- **下拉展开**：百叶窗式展开动画，带弹性缩放效果
- **箭头旋转**：下拉时箭头旋转180度，带缓动效果
- **项悬停动画**：浅灰色背景过渡（0.2秒）
- **项选中动画**：渐变背景 + 阴影效果

## 统一光泽层

本控件使用全局统一的白色光泽层资源 `GlossBrush`（定义在 `Styles/GradientColors.xaml`）：

- **渐变规格**：二段式半高，顶部 `#CCFFFFFF`(80%白) → 底部 `#33FFFFFF`(20%白)
- **实现方式**：通过 `{DynamicResource GlossBrush}` 引用，配合 `HalfHeightConverter` 实现高度减半
- **定位**：`VerticalAlignment="Top"` 顶部对齐
- **裁剪**：`ClipToBounds="True"` 防止溢出

## 使用示例

```xml
<!-- 默认蓝色组合框 -->
<everything:EverythingComboBox SelectedIndex="0">
    <ComboBoxItem Content="选项 1"/>
    <ComboBoxItem Content="选项 2"/>
    <ComboBoxItem Content="选项 3"/>
</everything:EverythingComboBox>

<!-- 使用其他颜色 -->
<everything:EverythingComboBox SelectedIndex="0" ColorName="Red">
    <ComboBoxItem Content="红色主题"/>
</everything:EverythingComboBox>

<everything:EverythingComboBox SelectedIndex="0" ColorName="Red">
    <ComboBoxItem Content="红色主题"/>
</everything:EverythingComboBox>

<everything:EverythingComboBox SelectedIndex="0" ColorName="Green">
    <ComboBoxItem Content="绿色主题"/>
</everything:EverythingComboBox>

<everything:EverythingComboBox SelectedIndex="0" ColorName="Purple">
    <ComboBoxItem Content="紫色主题"/>
</everything:EverythingComboBox>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色。
