# EverythingComboBox - 组合框控件

带渐变效果的组合框控件，支持下拉列表自定义样式和选中项光泽层。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Placeholder | string | null | 占位符文本 |

## 视觉样式

- **下拉按钮**：渐变背景 + 统一白色光泽层（GlossBrush），白色箭头图标，带阴影效果
- **下拉选中项**：渐变高亮 + 顶部半高光泽层（GlossBrush, Opacity=0.6）+ 阴影效果
- **悬停项**：浅灰色背景
- **阴影效果**：主按钮带有柔和阴影，悬停时阴影加深

## 动画效果

- **悬停动画**：按钮轻微放大（1.02倍），阴影加深（BlurRadius: 8→12, Opacity: 0.2→0.3）
- **按下动画**：按钮缩小（0.98倍），阴影深度减小（2→1）
- **下拉展开**：百叶窗式展开动画，带弹性缩放效果
- **箭头旋转**：下拉时箭头旋转180度，带缓动效果
- **项悬停动画**：浅灰色背景过渡（0.2秒）
- **项选中动画**：渐变背景 + 光泽层 + 阴影效果

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
