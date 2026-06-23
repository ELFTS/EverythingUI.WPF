# EverythingCheckBox - 复选框控件

带渐变效果的复选框控件，支持自定义颜色、统一白色光泽层（GlossBrush）和三种状态。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| IsChecked | bool? | false | 是否选中（支持三态） |
| IsThreeState | bool | false | 是否启用三态 |
| BoxSize | double | 22 | 复选框大小 |
| CornerRadius | CornerRadius | 6 | 圆角半径 |
| CheckMarkBrush | Brush | 白色 | 勾选标记颜色 |

## 视觉样式

- **选中状态**：渐变背景 + 统一白色光泽层（GlossBrush），白色勾选图标，带弹性缩放动画
- **未选中状态**：透明背景，边框显示
- **不确定状态**：渐变背景 + 光泽层，白色横线，带缩放动画
- **外阴影**：复选框外层带柔和 DropShadow 外阴影，增强拟物立体感
- **悬停状态**：边框颜色加深，背景渐变调整
- **按下状态**：边框颜色进一步加深

## 动画效果

- **选中动画**：勾选标记弹性缩放（0.3秒，ElasticEase，1次振荡）
- **不确定动画**：横线缩放展开（0.2秒，CubicEase）
- **背景过渡**：渐变背景淡入淡出（0.2秒，CubicEase）

## 使用示例

```xml
<!-- 默认蓝色 -->
<everything:EverythingCheckBox Content="默认蓝色" IsChecked="True"/>
```

### 三态复选框

```xml
<everything:EverythingCheckBox
    Content="三态选项"
    IsThreeState="True"
    IsChecked="{x:Null}"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
