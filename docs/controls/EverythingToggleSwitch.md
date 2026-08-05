# EverythingToggleSwitch - 开关控件

拟物化风格的开关控件，支持渐变效果和流畅动画。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| IsChecked | bool | false | 是否选中 |
| SwitchWidth | double | 50 | 开关宽度 |
| SwitchHeight | double | 26 | 开关高度 |
| ThumbSize | double | 22 | 滑块大小 |
| UncheckedBackground | Brush | #CCCCCC | 关闭状态背景色 |
| ThumbBrush | Brush | 白色 | 滑块颜色 |

## 视觉样式

- **开启状态**：主题色垂直渐变背景（`PrimaryVerticalBrush`）+ 顶部半高白色光泽层（`GlossBrush`），滑块在右侧
- **关闭状态**：`UncheckedBackground` 背景，滑块在左侧
- **滑块**：圆角方形，颜色由 `ThumbBrush` 控制
- **阴影**：轨道与滑块均带柔和阴影，开启状态下阴影保留

## 动画效果

- **滑块滑动**：开启时滑块移至右侧，关闭时回到左侧，带弹性回弹效果
- **背景切换**：开启/关闭背景交叉淡入淡出

## 使用示例

```xml
<!-- 默认蓝色开关 -->
<everything:EverythingToggleSwitch IsChecked="True"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
