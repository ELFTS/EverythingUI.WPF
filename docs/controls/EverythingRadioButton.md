# EverythingRadioButton - 单选框控件

带渐变效果的单选框控件，支持自定义颜色、统一白色光泽层（GlossBrush）和分组。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| GroupName | string | null | 分组名称 |
| IsChecked | bool | false | 是否选中 |
| BoxSize | double | 22 | 单选框大小 |
| DotBrush | Brush | 白色 | 圆点标记颜色 |

## 视觉样式

- **选中状态**：渐变背景 + 统一白色光泽层（GlossBrush），白色圆点，带弹性缩放动画
- **未选中状态**：透明背景，圆形边框
- **悬停状态**：边框颜色加深，背景渐变调整
- **按下状态**：边框颜色进一步加深

## 动画效果

- **选中动画**：圆点弹性缩放（0.3秒，ElasticEase，1次振荡）
- **背景过渡**：渐变背景淡入淡出（0.2秒，CubicEase）

## 使用示例

```xml
<!-- 默认蓝色 -->
<everything:EverythingRadioButton Content="选项 A" GroupName="Group1" IsChecked="True"/>
```

### 单选组示例

```xml
<StackPanel>
    <TextBlock Text="请选择：" Margin="0,0,0,10"/>
    <everything:EverythingRadioButton Content="选项 A" GroupName="Group1" IsChecked="True"/>
    <everything:EverythingRadioButton Content="选项 B" GroupName="Group1"/>
    <everything:EverythingRadioButton Content="选项 C" GroupName="Group1"/>
</StackPanel>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
