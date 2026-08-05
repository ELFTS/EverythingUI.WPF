# EverythingComboBox - 组合框控件

带渐变效果的组合框控件，支持下拉列表自定义样式和选中项光泽层。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Placeholder | string | "" | 占位符文本 |

## 视觉样式

- **下拉按钮**：渐变背景 + 统一白色光泽层（GlossBrush），白色箭头图标，带柔和阴影
- **下拉选中项**：渐变高亮 + 顶部光泽层 + 阴影，文字变白
- **悬停项**：浅灰色背景
- **滚动条**：下拉列表自动使用 EverythingVerticalScrollBar 样式

## 动画效果

- 悬停时按钮轻微放大，离开回弹
- 下拉展开时列表自上而下百叶窗式错开展开，关闭时倒序收起
- 下拉时箭头旋转 180 度，关闭回正

## 使用示例

```xml
<!-- 默认蓝色组合框 -->
<everything:EverythingComboBox SelectedIndex="0">
    <ComboBoxItem Content="选项 1"/>
    <ComboBoxItem Content="选项 2"/>
    <ComboBoxItem Content="选项 3"/>
</everything:EverythingComboBox>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
